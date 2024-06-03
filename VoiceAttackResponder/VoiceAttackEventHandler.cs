using Eddi;
using EddiCore;
using EddiEvents;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utilities;

namespace EddiVoiceAttackResponder
{
    internal static class VoiceAttackEventHandler
    {
        private static readonly ConcurrentDictionary<string, TaskQueue<Event>> eventQueues = new ConcurrentDictionary<string, TaskQueue<Event>>();
        private static readonly CancellationTokenSource consumerCancellationTS = new CancellationTokenSource(); // This must be static so that it is visible to child threads and tasks

        // We'll maintain a referenceable list of variables that we've set from events
        private static List<VoiceAttackVariable> currentVariables = new List<VoiceAttackVariable>();

        public static void Handle ( Event theEvent )
        {
            // Check for any completed, cancelled, or faulted consumer tasks and clean up consumer tasks which are no longer needed
            foreach ( var eventQueue in eventQueues.Where( q => !q.Value.isRunning ) )
            {
                if ( eventQueues.TryRemove( eventQueue.Key, out var removed ) )
                {
                    removed.Dispose();
                }
            }

            if ( theEvent is null || consumerCancellationTS.IsCancellationRequested ) { return; }

            if ( eventQueues.TryGetValue( theEvent.type, out var taskQueue ) )
            {
                // Add our event to an existing blocking collection for that event type.
                taskQueue.Add( theEvent );
            }
            else
            {
                // Add our event to a new blocking collection for that event type and start a consumer task for that collection
                eventQueues[ theEvent.type ] = new TaskQueue<Event>( () =>
                {
                    // ReSharper disable once AccessToModifiedClosure - OK to use vaProxy in this context.
                    dequeueEvents( eventQueues[ theEvent.type ], ref App.vaProxy );
                }, consumerCancellationTS.Token, TaskCreationOptions.PreferFairness ) { theEvent };
            }
        }

        private static void dequeueEvents ( BlockingCollection<Event> eventQueue, ref dynamic vaProxy )
        {
            try
            {
                foreach ( var @event in eventQueue.GetConsumingEnumerable( consumerCancellationTS.Token ) )
                {
                    try
                    {
                        if ( @event.type != null )
                        {
                            Logging.Debug( $"Passing event {@event.type} to VoiceAttack", @event );
                            lock ( VoiceAttackPlugin.vaProxyLock )
                            {
                                updateValuesOnEvent( @event, ref vaProxy );
                                triggerVACommands( @event, ref vaProxy );
                            }
                            // We need to wait until each event is no longer active before moving to the next from the same
                            // queue / event type so that variables aren't overwritten before VoiceAttack can respond.
                            // Other queues / event types will be able to continue processing events while we wait.
                            var isCommandExecuting = true;
                            while ( isCommandExecuting )
                            {
                                Thread.Sleep( 50 );
                                if ( EDDI.Instance.vaVersion?.CompareTo( new System.Version( 1, 7, 4 ) ) > 0 ) // If running VoiceAttack version 1.7.4 or later
                                {
                                    isCommandExecuting = vaProxy.Command.Active( "((EDDI " + @event.type.ToLowerInvariant() + "))" );
                                }
                                else // Legacy command invocation for versions of VoiceAttack prior to 1.7.4
                                {
                                    isCommandExecuting = vaProxy.CommandActive( "((EDDI " + @event.type.ToLowerInvariant() + "))" );
                                }
                            }
                        }
                    }
                    catch ( Exception ex )
                    {
                        Logging.Error( $"VoiceAttack failed to handle {@event.type} event.", ex );
                    }

                    if ( eventQueue.Count == 0 )
                    {
                        // If the event queue is empty then we can complete and clean up the associated consumer task.
                        return;
                    }
                }
            }
            catch ( OperationCanceledException )
            {
                // Task canceled. Nothing to do here.
            }
        }

        private static void updateValuesOnEvent ( Event @event, ref dynamic vaProxy )
        {
            try
            {
                Logging.Debug( $"Processing EDDI event {@event.type}:", @event );
                var startTime = DateTime.UtcNow;
                vaProxy.SetText( "EDDI event", @event.type );

                // Retrieve and clear variables from prior iterations of the same event
                clearPriorEventValues( ref vaProxy, @event.type, currentVariables );
                currentVariables = currentVariables.Where( v => v.eventType != @event.type ).ToList();

                // Prepare and update this event's variable values
                var eventVariables = new MetaVariables(@event.GetType(), @event)
                .Results
                .AsVoiceAttackVariables("EDDI", @event.type);
                foreach ( var var in eventVariables )
                { var.Set( vaProxy ); }
                Logging.Debug( $"Set VoiceAttack variables for EDDI event {@event.type}", eventVariables );

                // Save the updated state of our event variables
                currentVariables.AddRange( eventVariables );

                Logging.Debug( $"Processed EDDI event {@event.type} in {( DateTime.UtcNow - startTime ).Milliseconds} milliseconds:", @event );
            }
            catch ( Exception ex )
            {
                Logging.Error( "Failed to set event variables in VoiceAttack", ex );
            }
        }

        private static void clearPriorEventValues ( ref dynamic vaProxy, string eventType, List<VoiceAttackVariable> eventVariables )
        {
            try
            {
                // We set all values in our list from a prior version of the same event to null
                foreach ( var variable in eventVariables
                             .Where( v => v.eventType == eventType && v.value != null ) )
                {
                    variable.value = null;
                }
                // We clear variable values by swapping the values to null and then instructing VA to set them again
                foreach ( var var in eventVariables )
                { var.Set( vaProxy ); }
            }
            catch ( Exception ex )
            {
                Logging.Error( "Failed to clear event variables in VoiceAttack", ex );
            }
        }

        private static void triggerVACommands ( Event @event, ref dynamic vaProxy )
        {
            string commandName = "((EDDI " + @event.type.ToLowerInvariant() + "))";
            try
            {
                // Fire local command if present  
                Logging.Debug( "Searching for command " + commandName );
                if ( EDDI.Instance.vaVersion?.CompareTo( new System.Version( 1, 7, 4 ) ) > 0 ) // If running VoiceAttack version 1.7.4 or later
                {
                    if ( vaProxy.Command.Exists( commandName ) )
                    {
                        Logging.Debug( "Found command " + commandName );
                        vaProxy.Command.Execute( commandName );
                        Logging.Info( "Executed command " + commandName );
                    }
                }
                else // Legacy command invocation for versions of VoiceAttack prior to 1.7.4
                {
                    if ( vaProxy.CommandExists( commandName ) )
                    {
                        Logging.Debug( "Found command " + commandName );
                        vaProxy.ExecuteCommand( commandName );
                        Logging.Info( "Executed command " + commandName );
                    }
                }
            }
            catch ( Exception ex )
            {
                Logging.Error( "Failed to trigger local VoiceAttack command " + commandName, ex );
            }
        }

        public static void StopEventHandling ()
        {
            // Cancel event queue threads and wait for them to complete
            consumerCancellationTS?.Cancel();
            Task.WhenAny(
                Task.Run( () =>
                {
                    while ( eventQueues.Values.Any( q => q.isRunning ) )
                    {
                        Thread.Sleep( TimeSpan.FromMilliseconds( 50 ) );
                    }
                } ),
                Task.Delay( 2000 )
            );
            foreach ( var q in eventQueues.Values )
            { q.Dispose(); }
        }
    }

    internal class TaskQueue<T> : BlockingCollection<T>
    {
        public bool isRunning => consumerTask != null &&
                                 ( !consumerTask.IsCanceled || consumerTask.IsCompleted || consumerTask.IsFaulted );

        private Task consumerTask { get; }

        public TaskQueue ( Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions )
        {
            consumerTask = new Task( action, cancellationToken, creationOptions );
            consumerTask.Start();
        }
    }
}
