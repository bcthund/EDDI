using EddiCore;
using EddiDataDefinitions;
using EddiEvents;
using EddiStatusService;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Utilities;

[assembly: InternalsVisibleTo( "Tests" )]
namespace EddiStatusMonitor
{
    [UsedImplicitly]
    public class StatusMonitor : IEddiMonitor
    {
        // Miscellaneous tracking
        private bool jumping;
        private string lastDestinationPOI;
        private string lastMusicTrack;
        internal Status currentStatus;
        internal Status lastStatus;
        private static readonly object statusLock = new object();

        [ExcludeFromCodeCoverage]
        public StatusMonitor ()
        {
            Logging.Info($"Initialized {MonitorName()}");
        }

        [ExcludeFromCodeCoverage]
        public string MonitorName()
        {
            return "Status monitor";
        }

        [ExcludeFromCodeCoverage]
        public string LocalizedMonitorName()
        {
            return "Status monitor";
        }

        [ExcludeFromCodeCoverage]
        public string MonitorDescription()
        {
            return "Monitor Elite: Dangerous' Status.json for current status.  This should not be disabled unless you are sure you know what you are doing, as it will result in many functions inside EDDI no longer working";
        }

        public bool IsRequired()
        {
            return true;
        }

        public bool NeedsStart()
        {
            return true;
        }

        [ExcludeFromCodeCoverage]
        public void Start()
        {
            StatusService.Instance.Start();
        }

        public void HandleStatus(Status status)
        {
            if ( status != null )
            {
                lock ( statusLock )
                {
                    lastStatus = currentStatus;
                    currentStatus = status;
                }

                // Update the commander's credit balance
                if ( currentStatus.credit_balance != null && EDDI.Instance.Cmdr != null )
                {
                    EDDI.Instance.Cmdr.credits = Convert.ToUInt64( currentStatus.credit_balance );
                }

                // Update vehicle information
                if ( !string.IsNullOrEmpty( currentStatus.vehicle ) && currentStatus.vehicle != lastStatus?.vehicle )
                {
                    if ( EDDI.Instance.Vehicle != currentStatus.vehicle )
                    {
                        var statusSummary = new Dictionary<string, Status> { { "isStatus", currentStatus }, { "wasStatus", lastStatus } };
                        Logging.Debug( $"Status changed vehicle from {lastStatus?.vehicle ?? "<NULL>"} to {currentStatus.vehicle}", statusSummary );
                        EDDI.Instance.Vehicle = currentStatus.vehicle;
                    }
                }
                if ( currentStatus.vehicle == Constants.VEHICLE_SHIP && EDDI.Instance.CurrentShip != null )
                {
                    EDDI.Instance.CurrentShip.cargoCarried = currentStatus.cargo_carried ?? 0;
                    EDDI.Instance.CurrentShip.fuelInTanks = currentStatus.fuelInTanks ?? 0;
                    EDDI.Instance.CurrentShip.fuelInReservoir = currentStatus.fuelInReservoir ?? 0;
                }

                if ( lastStatus is null ) { return; }

                // Trigger events for changed status, as applicable
                if ( currentStatus.shields_up != lastStatus.shields_up && currentStatus.vehicle == lastStatus.vehicle )
                {
                    // React to changes in shield state.
                    // We check the vehicle to make sure that events aren't generated when we switch vehicles, start the game, or stop the game.
                    if ( currentStatus.shields_up )
                    {
                        EDDI.Instance.enqueueEvent( new ShieldsUpEvent( currentStatus.timestamp ) );
                    }
                    else
                    {
                        EDDI.Instance.enqueueEvent( new ShieldsDownEvent( currentStatus.timestamp ) );
                    }
                }
                if ( currentStatus.srv_turret_deployed != lastStatus.srv_turret_deployed )
                {
                    EDDI.Instance.enqueueEvent( new SRVTurretEvent( currentStatus.timestamp, currentStatus.srv_turret_deployed ) );
                }
                if ( currentStatus.silent_running != lastStatus.silent_running )
                {
                    EDDI.Instance.enqueueEvent( new SilentRunningEvent( currentStatus.timestamp, currentStatus.silent_running ) );
                }
                if ( currentStatus.srv_under_ship != lastStatus.srv_under_ship && lastStatus.vehicle == Constants.VEHICLE_SRV )
                {
                    // If the turret is deployable then we are not under our ship. And vice versa. 
                    bool deployable = !currentStatus.srv_under_ship;
                    EDDI.Instance.enqueueEvent( new SRVTurretDeployableEvent( currentStatus.timestamp, deployable ) );
                }
                if ( currentStatus.fsd_status != lastStatus.fsd_status
                    && currentStatus.vehicle == Constants.VEHICLE_SHIP
                    && !currentStatus.docked )
                {
                    if ( currentStatus.fsd_status == "ready" )
                    {
                        switch ( lastStatus.fsd_status )
                        {
                            case "charging":
                                if ( !jumping && currentStatus.supercruise == lastStatus.supercruise )
                                {
                                    EDDI.Instance.enqueueEvent( new ShipFsdEvent( currentStatus.timestamp, "charging cancelled" ) );
                                }
                                jumping = false;
                                break;
                            case "cooldown":
                                EDDI.Instance.enqueueEvent( new ShipFsdEvent( currentStatus.timestamp, "cooldown complete" ) );
                                break;
                            case "masslock":
                                EDDI.Instance.enqueueEvent( new ShipFsdEvent( currentStatus.timestamp, "masslock cleared" ) );
                                break;
                        }
                    }
                    else
                    {
                        EDDI.Instance.enqueueEvent( new ShipFsdEvent( currentStatus.timestamp, currentStatus.fsd_status, currentStatus.fsd_hyperdrive_charging ) );
                    }
                }
                if ( currentStatus.vehicle == lastStatus.vehicle ) // 'low fuel' is 25% or less
                {
                    // Trigger `Low fuel` events for each 5% fuel increment at 25% fuel or less (where our vehicle remains constant)
                    if ( ( currentStatus.low_fuel && !lastStatus.low_fuel ) || // 25%
                        ( currentStatus.fuel_percentile != null && // less than 20%, 15%, 10%, or 5%
                         lastStatus.fuel_percentile != null &&
                         currentStatus.fuel_percentile <= 4 &&
                         currentStatus.fuel_percentile < lastStatus.fuel_percentile ) )
                    {
                        EDDI.Instance.enqueueEvent( new LowFuelEvent( currentStatus.timestamp ) );
                    }
                }
                if ( currentStatus.landing_gear_down != lastStatus.landing_gear_down
                    && currentStatus.vehicle == Constants.VEHICLE_SHIP && lastStatus.vehicle == Constants.VEHICLE_SHIP )
                {
                    EDDI.Instance.enqueueEvent( new ShipLandingGearEvent( currentStatus.timestamp, currentStatus.landing_gear_down ) );
                }
                if ( currentStatus.cargo_scoop_deployed != lastStatus.cargo_scoop_deployed )
                {
                    EDDI.Instance.enqueueEvent( new ShipCargoScoopEvent( currentStatus.timestamp, currentStatus.cargo_scoop_deployed ) );
                }
                if ( currentStatus.lights_on != lastStatus.lights_on )
                {
                    EDDI.Instance.enqueueEvent( new ShipLightsEvent( currentStatus.timestamp, currentStatus.lights_on ) );
                }
                if ( currentStatus.hardpoints_deployed != lastStatus.hardpoints_deployed )
                {
                    EDDI.Instance.enqueueEvent( new ShipHardpointsEvent( currentStatus.timestamp, currentStatus.hardpoints_deployed ) );
                }
                if ( currentStatus.flight_assist_off != lastStatus.flight_assist_off )
                {
                    EDDI.Instance.enqueueEvent( new FlightAssistEvent( currentStatus.timestamp, currentStatus.flight_assist_off ) );
                }
                if ( !string.IsNullOrEmpty( currentStatus.destination_name ) && currentStatus.destination_name != lastStatus.destination_name
                    && currentStatus.vehicle == lastStatus.vehicle )
                {
                    if ( EDDI.Instance.CurrentStarSystem != null && EDDI.Instance.CurrentStarSystem.systemAddress ==
                        currentStatus.destinationSystemAddress && currentStatus.destination_name != lastDestinationPOI )
                    {
                        var body = EDDI.Instance.CurrentStarSystem.bodies.FirstOrDefault(b =>
                                b.bodyId == currentStatus.destinationBodyId
                                && b.bodyname == currentStatus.destination_name);
                        var station = EDDI.Instance.CurrentStarSystem.stations.FirstOrDefault(s =>
                                s.name == currentStatus.destination_name);

                        // There is an FDev bug where both Encoded Emissions and High Grade Emissions use the `USS_HighGradeEmissions` edName.
                        // When this occurs, we need to fall back to our generic signal source name.
                        var signalSource = currentStatus.destination_name == "$USS_HighGradeEmissions;"
                                ? SignalSource.GenericSignalSource
                                : EDDI.Instance.CurrentStarSystem.signalSources.FirstOrDefault(s =>
                                      s.edname == currentStatus.destination_name) ?? SignalSource.FromEDName(currentStatus.destination_name);

                        // Might be a body (including the primary star of a different system if selecting a star system)
                        if ( body != null && currentStatus.destination_name == body.bodyname )
                        {
                            EDDI.Instance.enqueueEvent( new NextDestinationEvent(
                                currentStatus.timestamp,
                                currentStatus.destinationSystemAddress,
                                currentStatus.destinationBodyId,
                                currentStatus.destination_name,
                                currentStatus.destination_localized_name,
                                body ) );
                        }
                        // Might be a station (including megaship or fleet carrier)
                        else if ( station != null )
                        {
                            EDDI.Instance.enqueueEvent( new NextDestinationEvent(
                                currentStatus.timestamp,
                                currentStatus.destinationSystemAddress,
                                currentStatus.destinationBodyId,
                                currentStatus.destination_name,
                                currentStatus.destination_localized_name,
                                body,
                                station ) );
                        }
                        // Might be a non-station signal source
                        else if ( signalSource != null )
                        {
                            if ( !currentStatus.destination_localized_name?.StartsWith( "$" ) ?? false )
                            {
                                signalSource.fallbackLocalizedName = currentStatus.destination_localized_name;
                            }
                            EDDI.Instance.enqueueEvent( new NextDestinationEvent(
                                currentStatus.timestamp,
                                currentStatus.destinationSystemAddress,
                                currentStatus.destinationBodyId,
                                signalSource.invariantName,
                                signalSource.localizedName,
                                null,
                                null,
                                signalSource ) );
                        }
                        else if ( currentStatus.destination_name != lastDestinationPOI )
                        {
                            EDDI.Instance.enqueueEvent( new NextDestinationEvent(
                                currentStatus.timestamp,
                                currentStatus.destinationSystemAddress,
                                currentStatus.destinationBodyId,
                                currentStatus.destination_name,
                                currentStatus.destination_localized_name ?? currentStatus.destination_name,
                                body ) );
                        }
                        lastDestinationPOI = currentStatus.destination_name;
                    }
                }
                if ( !currentStatus.gliding && lastStatus.gliding )
                {
                    EDDI.Instance.enqueueEvent( new GlideEvent( currentStatus.timestamp, currentStatus.gliding, EDDI.Instance.CurrentStellarBody?.systemname, EDDI.Instance.CurrentStellarBody?.systemAddress, EDDI.Instance.CurrentStellarBody?.bodyname, EDDI.Instance.CurrentStellarBody?.bodyType ) );
                }
                else if ( currentStatus.gliding && !lastStatus.gliding && StatusService.Instance.lastEnteredNormalSpaceEvent != null )
                {
                    var theEvent = StatusService.Instance.lastEnteredNormalSpaceEvent;
                    EDDI.Instance.enqueueEvent( new GlideEvent( DateTime.UtcNow, currentStatus.gliding, theEvent.systemname, theEvent.systemAddress, theEvent.bodyname, theEvent.bodyType ) { fromLoad = theEvent.fromLoad } );
                }
                // Reset our fuel log if we change vehicles or refuel
                if ( currentStatus.vehicle != lastStatus.vehicle || currentStatus.fuel > lastStatus.fuel )
                {
                    StatusService.Instance.fuelLog.Clear();
                }
                // Detect whether we're in combat
                if ( lastStatus.in_danger && !currentStatus.in_danger )
                {
                    EDDI.Instance.enqueueEvent( new SafeEvent( DateTime.UtcNow ) { fromLoad = false } );
                }
            }
        }

        [ExcludeFromCodeCoverage]
        public void Stop()
        {
            StatusService.Instance.Stop();
        }

        [ExcludeFromCodeCoverage]
        public void Reload()
        { }

        [ExcludeFromCodeCoverage]
        public UserControl ConfigurationTabItem()
        {
            return null;
        }

        public void PreHandle(Event @event)
        {
            // Some events can be derived from our status during a given event
            if ( @event is EnteredNormalSpaceEvent enteredNormalSpaceEvent )
            {
                handleEnteredNormalSpaceEvent( enteredNormalSpaceEvent );
            }
            else if ( @event is FSDEngagedEvent fsdEngagedEvent )
            {
                handleFSDEngagedEvent( fsdEngagedEvent );
            }
            else if ( @event is MusicEvent musicEvent )
            {
                handleMusicEvent( musicEvent );
            }
            else if ( @event is SettlementApproachedEvent settlementApproachedEvent )
            {
                handleSettlementApproachedEvent( settlementApproachedEvent );
            }
        }

        internal void handleSettlementApproachedEvent ( SettlementApproachedEvent @event )
        {
            // Synthesize a `Destination arrived` event when approaching a settlement / location we've been tracking,
            // if the journal hasn't already generated a `SupercruiseDestinationDrop` event
            if ( !@event.fromLoad &&
                 currentStatus?.destinationSystemAddress != null &&
                 currentStatus.destinationSystemAddress == @event.systemAddress &&
                 currentStatus.destinationBodyId == @event.bodyId &&
                 ( currentStatus.destination_name == @event.name ||
                   currentStatus.destination_localized_name == @event.name ) )
            {
                // Retrieve the last `SupercruiseDestinationDrop` event and verify that, if it exists, it does not match the settlement we may be approaching.
                if ( !EDDI.Instance.lastEventOfType.TryGetValue( "SupercruiseDestinationDrop",
                         out var supercruiseDestinationDrop ) ||
                     !( supercruiseDestinationDrop is DestinationArrivedEvent destinationArrivedEvent ) ||
                     destinationArrivedEvent.name != @event.name )
                {
                    destinationArrivedEvent = new DestinationArrivedEvent( currentStatus.timestamp, @event.name );
                    EDDI.Instance.enqueueEvent( destinationArrivedEvent );
                }
            }
        }

        internal void handleEnteredNormalSpaceEvent( EnteredNormalSpaceEvent @event )
        {
            // We can derive a "Glide" event from the context in our status
            StatusService.Instance.lastEnteredNormalSpaceEvent = @event;
        }

        internal void handleFSDEngagedEvent( FSDEngagedEvent @event )
        {
            if (@event.target == "Hyperspace")
            {
                jumping = true;
            }
            EDDI.Instance.enqueueEvent(new ShipFsdEvent( @event.timestamp, "charging complete" ) { fromLoad = @event.fromLoad });
        }

        internal void handleMusicEvent ( MusicEvent @event )
        {
            // Derive a "Station mailslot" event from changes to music tracks
            Status status = null;
            LockManager.GetLock(nameof(currentStatus), () => { status = currentStatus; } );

            if ( status?.vehicle == Constants.VEHICLE_SHIP )
            {
                if ( @event.musictrack == "Starport" && 
                     ( lastMusicTrack == "NoTrack" || lastMusicTrack == "Exploration" ) &&
                     !status.docked )
                {
                    EDDI.Instance.enqueueEvent( new StationMailslotEvent( @event.timestamp ) { fromLoad = @event.fromLoad } );
                }
            }

            lastMusicTrack = @event.musictrack;
        }

        [ExcludeFromCodeCoverage]
        public void PostHandle(Event @event)
        { }

        [ExcludeFromCodeCoverage]
        public void HandleProfile(JObject profile)
        { }

        public IDictionary<string, Tuple<Type, object>> GetVariables()
        {
            lock ( statusLock )
            {
                return new Dictionary<string, Tuple<Type, object>>
                {
                    { "status", new Tuple<Type, object>(typeof(Status), currentStatus ) },
                    { "lastStatus", new Tuple < Type, object >(typeof(Status), lastStatus ) }
                };
            }
        }
    }
}
