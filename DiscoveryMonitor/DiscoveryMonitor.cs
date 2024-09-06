using EddiConfigService;
using EddiConfigService.Configurations;
using EddiCore;
using EddiDataDefinitions;
using EddiDataProviderService;
using EddiEvents;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using Utilities;
using Utilities.RegionMap;
using System.ComponentModel;

[assembly: InternalsVisibleTo( "Tests" )]
namespace EddiDiscoveryMonitor
{
    public class DiscoveryMonitor : IEddiMonitor, INotifyPropertyChanged
    {
        internal class FssSignal
        {
            public ulong systemAddress;         // For reference to double check
            public long bodyId;                 // For reference to double check
            public int geoCount;                // The number of geological signals detected
            public int bioCount;                // The number of biological signals detected
        }
        internal readonly HashSet<FssSignal> fssSignalsLibrary = new HashSet<FssSignal>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }

        internal DiscoveryMonitorConfiguration configuration;
        internal Exobiology _currentOrganic { get; set; }
        internal StarSystem _currentSystem => EDDI.Instance?.CurrentStarSystem;
        internal long? _currentBodyId;
        
        public long? CurrentBodyId
        {
            get { return _currentBodyId; }
            set {
                _currentBodyId = value;
                OnPropertyChanged("CurrentBodyId");
            }
        }

        internal Status _currentStatus { get; set; }

        internal Region _currentRegion;
        internal Nebula _nearestNebula;
        internal Nebula _enteredNebula;

        public DiscoveryMonitor ()
        {
            configuration = ConfigService.Instance.discoveryMonitorConfiguration;

            // Get the visited status of all Nebulae
            NebulaSqLiteRepository.Instance.GetNebulaeVisited(ref NebulaDefinitions.AllOfThem);
        }

        public string MonitorName ()
        {
            return "Discovery Monitor";
        }

        public string LocalizedMonitorName ()
        {
            return Properties.DiscoveryMonitor.monitorName;
        }

        public string MonitorDescription ()
        {
            return Properties.DiscoveryMonitor.monitorDescription;
        }

        public bool IsRequired ()
        {
            return true;
        }

        public bool NeedsStart ()
        {
            return false;
        }

        public void Start ()
        { }

        public void Stop ()
        { }

        public void Reload ()
        {
            configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        }

        public UserControl ConfigurationTabItem ()
        {
            return new ConfigurationWindow();
        }

        public void HandleStatus ( Status status )
        {
            _currentStatus = status;
            try
            {
                if ( TryCheckScanDistance( status, out var bio ) )
                {
                    EDDI.Instance.enqueueEvent( new ScanOrganicDistanceEvent( DateTime.UtcNow, bio ) );
                }
            }
            catch ( Exception exception )
            {
                Logging.Error( "Failed to handle status update: TryCheckScanDistance/ScanOrganicDistanceEvent", exception );
                throw;
            }
        }

        /// <summary>
        /// Check the currently active bio scan distance (if any). Return true if it's time to post a `ScanOrganicDistance` event.
        /// </summary>
        internal bool TryCheckScanDistance ( Status status, out Exobiology bioResult )
        {
            bioResult = null;
            if ( !CheckSafe() || status.latitude is null || status.longitude is null ) { return false; }

            //var body = _currentBody(_currentBodyId);
            var body = EDDI.Instance?.CurrentStarSystem.BodyWithID( CurrentBodyId );

            if ( body.surfaceSignals.TryGetBio( _currentOrganic, out var bio ) && bio.samples > 0 )
            {
                // If the bio has been fully sampled, ignore it.
                if( bio.ScanState == Exobiology.State.SampleAnalysed) {
                    return false;
                }

                var distanceFromSamplesKm = new SortedSet<decimal>();
                foreach ( var coords in bio.sampleCoords )
                {
                    var distance = Functions.SurfaceDistanceKm( body.radius * 1000, status.latitude, status.longitude, coords.Item1, coords.Item2 );
                    if ( distance != null )
                    {
                        distanceFromSamplesKm.Add( (decimal)distance );
                    }
                }

                var maxDistanceKm = distanceFromSamplesKm.LastOrDefault();
                var minDistanceKm = distanceFromSamplesKm.FirstOrDefault();

                //var distanceM = maxDistanceKm * 1000;
                var distanceM = minDistanceKm * 1000;

                if ( distanceM <= bio.genus.minimumDistanceMeters )
                {
                    // Was previously outside sample range, alert that we have violated the radius
                    if ( !bio.nearPriorSample )
                    {
                        bio.nearPriorSample = true;
                        bioResult = bio;
                        return true;
                    }
                }
                else if ( distanceM > bio.genus.minimumDistanceMeters )
                {
                    // Was previously inside sample range, alert that we have traveled past the sample radius
                    if ( bio.nearPriorSample )
                    {
                        bio.nearPriorSample = false;
                        bioResult = bio;
                        return true;
                    }
                }
            }
            return false;
        }

        public void PreHandle ( Event @event )
        {
            if ( @event is CodexEntryEvent entryEvent )
            {
                handleCodexEntryEvent( entryEvent );
            }
            else if ( @event is SurfaceSignalsEvent signalsEvent )
            {
                handleSurfaceSignalsEvent( signalsEvent );
                if (!@event.fromLoad) OnPropertyChanged("RefreshData");
            }
            else if ( @event is ScanOrganicEvent organicEvent )
            {
                handleScanOrganicEvent( organicEvent );
                if (!@event.fromLoad) OnPropertyChanged("RefreshData");
            }
            else if ( @event is BodyScannedEvent bodyScannedEvent )
            {
                handleBodyScannedEvent( bodyScannedEvent );
                if (!@event.fromLoad) OnPropertyChanged("RefreshData");
            }
            else if ( @event is StarScannedEvent starScannedEvent )
            {
                handleStarScannedEvent( starScannedEvent );
                if (!@event.fromLoad) OnPropertyChanged("RefreshData");
            }
            else if ( @event is JumpedEvent jumpedEvent )
            {
                handleJumpedEvent( jumpedEvent );
                if (!@event.fromLoad) OnPropertyChanged("RefreshData");
            }
            else if ( @event is LocationEvent locationEvent )
            {
                handleLocationEvent( locationEvent );
                OnPropertyChanged("RefreshData");
            }
            else if ( @event is NextDestinationEvent nextDestinationEvent) {
                handleNextDestinationEvent( nextDestinationEvent );
                if (!@event.fromLoad) OnPropertyChanged("RefreshData");
            }
        }

        internal void handleJumpedEvent ( JumpedEvent @event )
        {
            var log = "\r\n";
            bool error = false;

            // Check if the current region has changed
            log += $"\tGetting Region for ({@event.x},{@event.y},{@event.z}): ";
            if( @event.region != null )
            {
                CheckRegion(@event.region, @event.fromLoad);
            }
            else 
            {
                error = true;
                log += $"Region = NULL.\r\n";
            }

            // Check if the nearest nebula has changed
            log += $"\tGetting Nebula for {@event.system} @ ({@event.x},{@event.y},{@event.z}): ";
            if( @event.nebula != null )
            {
                CheckNebula(@event.nebula, @event.fromLoad);
            }
            else 
            {
                error = true;
                log += $"Nebula = NULL.\r\n";
            }

            if (error)
            {
                Logging.Error( log );
            }
            else
            {
                Logging.Debug( log );
            }
            
        }

        // When the location is recieved at startup or if the player respawns at a station update the region and nebula
        internal void handleLocationEvent ( LocationEvent @event )
        {
            var log = "\r\n";
            bool error = false;

            // Set the current body context
            CurrentBodyId = @event.bodyId;

            // Check if the current region has changed
            var checkRegion = RegionMap.FindRegion( (double)@event.x, (double)@event.y, (double)@event.z );

            log += $"\tGetting Region for ({@event.x},{@event.y},{@event.z}): ";
            if( checkRegion != null )
            {
                CheckRegion(checkRegion, @event.fromLoad);
            }
            else 
            {
                error = true;
                log += $"Region = NULL.\r\n";
            }

            var checkNebula = Nebula.TryGetNearestNebula( @event.systemname, (decimal)@event.x, (decimal)@event.y, (decimal) @event.z );
            log += $"\tGetting Nebula for {@event.systemname} @ ({@event.x},{@event.y},{@event.z}): ";
            if ( checkNebula != null )
            {
                CheckNebula( checkNebula, @event.fromLoad );
            }
            else
            {
                error = true;
                log += $"Nebula = NULL.\r\n";
            }

            if (error)
            {
                Logging.Error( log );
            }
            else
            {
                Logging.Debug( log );
            }
            
        }

        // When the location is recieved at startup or if the player respawns at a station update the region and nebula
        internal void handleNextDestinationEvent(NextDestinationEvent @event)
        {
            CurrentBodyId = @event.bodyId;
        }

        internal void CheckRegion(Region checkRegion, bool fromLoad) {
            if( _currentRegion is null || checkRegion.id != _currentRegion.id )
            {
                _currentRegion = checkRegion;

                EDDI.Instance.enqueueEvent(
                    new RegionEvent(
                        DateTime.UtcNow,
                        checkRegion )
                    {
                        fromLoad = fromLoad
                    } );
            }
        }

        internal void CheckNebula(Nebula checkNebula, bool fromLoad) {

            if ( _nearestNebula is null || _nearestNebula.id != checkNebula.id )
            {
                _nearestNebula = checkNebula;

                EDDI.Instance.enqueueEvent(
                    new NebulaEvent(
                        DateTime.UtcNow,
                        checkNebula )
                    {
                        fromLoad = fromLoad
                    } );
            }
            else if (_nearestNebula != null)
            {
                // Are we inside the radius of the nearest nebula?
                if ( _nearestNebula.distance <= ( _nearestNebula.diameter / 2 ) && ( _enteredNebula is null || _enteredNebula.id != checkNebula.id ) )
                {

                    _nearestNebula.visited = true;
                    _enteredNebula = checkNebula;

                    NebulaSqLiteRepository.Instance.SaveNebulaVisited(checkNebula.id, true);

                    EDDI.Instance.enqueueEvent(
                    new NebulaEnteredEvent(
                        DateTime.UtcNow,
                        _nearestNebula )
                    {
                        fromLoad = fromLoad
                    } );

                }
                else if ( _nearestNebula.distance > ( _nearestNebula.diameter / 2 ) && ( _enteredNebula != null ) )
                {
                    // We can use this to report if we have left the Nebula
                    EDDI.Instance.enqueueEvent(
                    new NebulaEnteredEvent(
                        DateTime.UtcNow,
                        _nearestNebula )
                    {
                        fromLoad = fromLoad
                    } );

                    _enteredNebula = null;
                }
            }
        }

        internal void handleCodexEntryEvent ( CodexEntryEvent @event )
        {
            // Not sure if we have anything to do here with this yet
        }

        /// <summary>
        /// Triggered when a planet is scanned (FSS) and mapped (SAA).
        /// For FSS, store information so that we can predict the genus that will be present
        /// </summary>
        internal void handleSurfaceSignalsEvent ( SurfaceSignalsEvent @event )
        {
            var log = "";

            CurrentBodyId = @event.bodyId;

            if ( @event.detectionType == "FSS" )
            {
                if ( !fssSignalsLibrary.Any( s => s.systemAddress == @event.systemAddress && s.bodyId == @event.bodyId ) &&
                     TryGetFssSurfaceSignals( @event, ref log, out var signals ) )
                {
                    fssSignalsLibrary.Add( signals );
                }
            }
            else if ( @event.detectionType == "SAA" )
            {
                if ( TrySetSaaSurfaceSignals( @event, ref log, out var body ) )
                {
                    // Save/Update Body data
                    body.surfaceSignals.lastUpdated = @event.timestamp;
                    EDDI.Instance?.CurrentStarSystem.AddOrUpdateBody( body );
                    StarSystemSqLiteRepository.Instance.SaveStarSystem(EDDI.Instance.CurrentStarSystem);
                }
            }

            Logging.Debug( log );
        }

        private bool TryGetFssSurfaceSignals ( SurfaceSignalsEvent @event, ref string log, out FssSignal signal )
        {
            if ( @event.systemAddress is null )
            { signal = null; return false; }

            log += "[FSSBodySignals]:\r\n";
            signal = new FssSignal { systemAddress = (ulong)@event.systemAddress, bodyId = @event.bodyId };
            var addSignal = false;

            foreach ( var sig in @event.surfaceSignals )
            {
                if ( sig.signalSource.edname == "SAA_SignalType_Biological" )
                {
                    log += $"\tDetect bios: {sig.amount}\r\n";
                    signal.bioCount = sig.amount;
                    addSignal = true;
                }
                else if ( sig.signalSource.edname == "SAA_SignalType_Geological" )
                {
                    log += $"\tDetect geos: {sig.amount}\r\n";
                    signal.geoCount = sig.amount;
                    addSignal = true;
                }
            }

            Logging.Debug( log );

            signal = addSignal ? signal : null;
            return addSignal;
        }

        private bool TrySetSaaSurfaceSignals ( SurfaceSignalsEvent @event, ref string log, out Body body )
        {
            //  The SAA signal is generated when logging in to the game, therefore set and check a bit so we
            //  know if SAA has already occurred on this body and prevents erasing the existing bio data
            body = EDDI.Instance?.CurrentStarSystem?.BodyWithID( @event.bodyId );
            if ( body == null ) { return false; }

            if(!body.surfaceSignals.hasCompletedSAA) {
                log += "[SAASignalsFound]: ";
                // Set the number of detected surface signals for each signal type
                foreach ( var signal in @event.surfaceSignals )
                {
                    if ( signal.signalSource == SignalSource.Biological )
                    {
                        log += $"Bios: {signal.amount}. ";
                        body.surfaceSignals.reportedBiologicalCount = signal.amount;
                    }
                    else if ( signal.signalSource == SignalSource.Geological )
                    {
                        log += $"Geos: {signal.amount}. ";
                        body.surfaceSignals.reportedGeologicalCount = signal.amount;
                    }
                    else if ( signal.signalSource == SignalSource.Guardian )
                    {
                        log += $"Guardian: {signal.amount}. ";
                        body.surfaceSignals.reportedGuardianCount = signal.amount;
                    }
                    else if ( signal.signalSource == SignalSource.Human )
                    {
                        log += $"Human: {signal.amount}. ";
                        body.surfaceSignals.reportedHumanCount = signal.amount;
                    }
                    else if ( signal.signalSource == SignalSource.Thargoid )
                    {
                        log += $"Thargoid: {signal.amount}. ";
                        body.surfaceSignals.reportedThargoidCount = signal.amount;
                    }
                    else
                    {
                        log += $"Other ({signal.signalSource.invariantName}): {signal.amount}. ";
                        body.surfaceSignals.reportedOtherCount += signal.amount;
                    }
                }

                Logging.Debug( log );

                if ( @event.bioSignals != null )
                {
                    // Compare our predicted and actual bio signals.
                    if ( body.surfaceSignals.hasPredictedBios )
                    {
                        var confirmedBiologicals = @event.bioSignals.Select(b => b.species).ToList();
                        var predictedBiologicals = body.surfaceSignals.bioSignals
                            .Where( b => b.ScanState == Exobiology.State.Predicted ).Select( b => b.species ).ToList();
                        var unpredictedBiologicals = confirmedBiologicals.Except( predictedBiologicals ).ToList();
                        var missingBiologicals = predictedBiologicals.Except( confirmedBiologicals ).ToList();

                        if ( unpredictedBiologicals.Any() )
                        {
                            log = "Unpredicted biologicals found";
                            log += $"\tStar System:  {body.systemname}\r\n";
                            log += $"\tBody Name:    {body.bodyname}\r\n";
                            log += $"\tGravity:      {body.gravity}\r\n";
                            log += $"\tTemperature:  {body.temperature} K\r\n";
                            log += $"\tPlanet Class: {(body.planetClass ?? PlanetClass.None).edname}\r\n";
                            log += $"\tAtmosphere:   {(body.atmosphereclass ?? AtmosphereClass.None).edname}\r\n";
                            log += $"\tVolcanism:    {body.volcanism?.edComposition ?? "None"}\r\n";
                            if ( EDDI.Instance?.CurrentStarSystem?.TryGetMainStar( out var parentStar ) ?? false )
                            {
                                log += $"\tParent star class: {parentStar.stellarclass}\r\n";
                            }
                            Logging.Error( log, unpredictedBiologicals);
                        }

                        if ( missingBiologicals.Any() )
                        {
                            log = "Predicted biologicals not found";
                            log += $"\tStar System:  {body.systemname}\r\n";
                            log += $"\tBody Name:    {body.bodyname}\r\n";
                            log += $"\tGravity:      {body.gravity}\r\n";
                            log += $"\tTemperature:  {body.temperature} K\r\n";
                            log += $"\tPlanet Class: {( body.planetClass ?? PlanetClass.None ).edname}\r\n";
                            log += $"\tAtmosphere:   {( body.atmosphereclass ?? AtmosphereClass.None ).edname}\r\n";
                            log += $"\tVolcanism:    {body.volcanism?.edComposition ?? "None"}\r\n";
                            if ( EDDI.Instance?.CurrentStarSystem?.TryGetMainStar( out var parentStar ) ?? false )
                            {
                                log += $"\tParent star class: {parentStar.stellarclass}\r\n";
                            }
                            Logging.Debug( log, missingBiologicals );
                        }
                    }

                    // Update from predicted to actual bio signals
                    body.surfaceSignals.bioSignals = @event.bioSignals;
                }

                body.surfaceSignals.hasCompletedSAA = true;
            }
            else {
                log = $"\r\n***** SAASignalsFound already logged for this body (body.surfaceSignals.hasCompletedSAA=true), ignoring event and using existing 'body.surfaceSignals' data *****\r\n\r\n";
                Logging.Debug( log );
            }

            return true;
        }

        private void handleScanOrganicEvent ( ScanOrganicEvent @event )
        {
            try
            {
                // Always get current bio data, even during fromLoad so we have correct context on startup
                //  - We have no other way to know the last bio context unless the ScanOrganic event is in the logs at startup
                //  - Could we somehow save the last bio context in config files?
                if ( CheckSafe( @event.bodyId ) )
                {
                    CurrentBodyId = @event.bodyId;

                    //var body = _currentBody(_currentBodyId);
                    var body = EDDI.Instance?.CurrentStarSystem.BodyWithID( CurrentBodyId );
                    var log = "";

                    // Retrieve and/or add the organic
                    if ( body.surfaceSignals.TryGetBio( @event.variant, @event.species, @event.genus, out var bio ) )
                    {
                        log += "Fetched biological\r\n";
                    }
                    else
                    {
                        log += "Adding biological\r\n";
                        bio = bio ?? body.surfaceSignals.AddBio( @event.variant, @event.species, @event.genus );
                    }

                    if ( bio == null )
                    {
                        Logging.Debug( log );
                        return;
                    }

                    _currentOrganic = bio;

                    // If event if fromLoad then do not update the bio data
                    if (!@event.fromLoad) {

                        if ( bio.ScanState == Exobiology.State.Predicted )
                        {
                            log += $"Presence of predicted organic {bio.species} is confirmed\r\n";
                            bio.ScanState = Exobiology.State.Confirmed;
                        }

                        if ( bio.variant is null )
                        {
                            log += "Setting additional data from variant details\r\n";
                            bio.SetVariantData( @event.variant );
                        }

                        bio.Sample( @event.scanType,
                            @event.variant,
                            _currentStatus.latitude,
                            _currentStatus.longitude );

                        // These are updated when the above Sample() function is called, se we send them back to the event
                        // Otherwise we would probably have to enqueue a new event (maybe not a bad idea?)
                        @event.bio = bio;
                        @event.remainingBios = body.surfaceSignals.bioSignalsRemaining.Except( new[] { bio } ).ToList();

                        Logging.Debug( log, @event );

                        if ( bio.ScanState == Exobiology.State.SampleComplete )
                        {
                            // The `Analyse` journal event normally takes place about 5 seconds after completing the sample
                            // but can be delayed if the commander holsters their scanner before the analysis cycle is completed.
                            Task.Run( async () =>
                            {
                                int timeMs = 15000; // If after 15 seconds the event hasn't generated then
                                                    // we'll generate our own event and update our own internal tracking
                                                    // (regardless of whether the scanner is holstered).
                                await Task.Delay( timeMs );
                                if ( bio.ScanState < Exobiology.State.SampleAnalysed )
                                {
                                    Logging.Debug( "Generating synthetic 'Analyse' event (to update internal tracking when scanner is holstered before `Analyse` completes)" );
                                    EDDI.Instance.enqueueEvent(
                                        new ScanOrganicEvent(
                                            @event.timestamp.AddMilliseconds( timeMs ),
                                            @event.systemAddress,
                                            @event.bodyId, "Analyse",
                                            @event.genus,
                                            @event.species,
                                            @event.variant )
                                        {
                                            fromLoad = @event.fromLoad
                                        } );
                                }
                            } ).ConfigureAwait( false );
                        }
                        else if ( bio.ScanState == Exobiology.State.SampleAnalysed )
                        { 
                            // Clear our tracked organic once analysis is complete.
                            _currentOrganic = null; 
                        }

                        // Save/Update Body data
                        body.surfaceSignals.lastUpdated = @event.timestamp;
                        EDDI.Instance?.CurrentStarSystem.AddOrUpdateBody( body );
                        StarSystemSqLiteRepository.Instance.SaveStarSystem(EDDI.Instance.CurrentStarSystem);
                        
                    }

                    // Let DiscoveryMonitor window know there was a context update
                    OnPropertyChanged("handleScanOrganicEvent");
                }
            }
            catch ( Exception e )
            {
                Logging.Debug( "Failed to handle ScanOrganicEvent", e );
            }
        }

        private void handleBodyScannedEvent ( BodyScannedEvent @event )
        {
            if ( @event.bodyId is null || !CheckSafe( @event.bodyId ) ) { return; }

            if ( @event.systemAddress == EDDI.Instance?.CurrentStarSystem.systemAddress )
            {
                // Predict biologicals for a scanned body
                var body = EDDI.Instance?.CurrentStarSystem.BodyWithID( (long)@event.bodyId );
                var signal = fssSignalsLibrary.FirstOrDefault( s =>
                    s.systemAddress == body.systemAddress && s.bodyId == body.bodyId );

                if ( signal != null && 
                     !body.surfaceSignals.bioSignals.Any() && 
                     TryPredictBios( signal, ref body ) )
                {
                    EDDI.Instance.enqueueEvent( new OrganicPredictionEvent( DateTime.UtcNow, body, body.surfaceSignals.bioSignals ) );

                    // Save/Update Body data
                    body.surfaceSignals.lastUpdated = @event.timestamp;
                    EDDI.Instance?.CurrentStarSystem.AddOrUpdateBody( body );
                    StarSystemSqLiteRepository.Instance.SaveStarSystem(EDDI.Instance.CurrentStarSystem);
                }
            }
        }

        private void handleStarScannedEvent ( StarScannedEvent @event )
        {
            if ( @event.bodyId is null || !CheckSafe( @event.bodyId ) ) { return; }

            if ( @event.systemAddress == EDDI.Instance?.CurrentStarSystem.systemAddress )
            {
                // Predict biologicals for previously scanned bodies when a star is scanned
                var childBodyIDs = EDDI.Instance?.CurrentStarSystem.GetChildBodyIDs( (long)@event.bodyId );
                foreach ( var childBodyID in EDDI.Instance?.CurrentStarSystem.bodies
                             .Where( b=> b.bodyId != null && childBodyIDs.Contains((long)b.bodyId) )
                             .Select(b => b.bodyId) )
                {
                    //var body = _currentBody( (long)childBodyID );
                    var body = EDDI.Instance?.CurrentStarSystem.BodyWithID( (long)childBodyID );
                    var signal = fssSignalsLibrary.FirstOrDefault( s =>
                        s.systemAddress == body.systemAddress && s.bodyId == body.bodyId );

                    if ( signal != null && 
                         !body.surfaceSignals.bioSignals.Any() && 
                         TryPredictBios( signal, ref body ) )
                    {
                        EDDI.Instance.enqueueEvent( new OrganicPredictionEvent( DateTime.UtcNow, body, body.surfaceSignals.bioSignals ) );

                        // Save/Update Body data
                        body.surfaceSignals.lastUpdated = @event.timestamp;
                        EDDI.Instance?.CurrentStarSystem.AddOrUpdateBody( body );
                        StarSystemSqLiteRepository.Instance.SaveStarSystem(EDDI.Instance.CurrentStarSystem);
                    }
                }
            }
        }

        private bool TryPredictBios(FssSignal signal, ref Body body)
        {
            var log = "";
            var hasPredictedBios = false;

            // TODO: This probably shouldn't be here, has nothing to do with bio predictions. Should consider a TryPredictGeos method in the future?
            if ( signal?.geoCount > 0 && body != null)
            {
                body.surfaceSignals.reportedGeologicalCount = signal.geoCount;
            }

            if ( signal?.bioCount > 0 && 
                 body != null && 
                 !body.surfaceSignals.bioSignals.Any() && 
                 EDDI.Instance.CurrentStarSystem.TryGetMainStar(out var parentStar))
            {
                // Always update the reported totals
                body.surfaceSignals.reportedBiologicalCount = signal.bioCount;
                log += $"[FSS backlog <{body.systemAddress},{body.bodyId}>\r\n" +
                       $"\tBio Count is {signal.bioCount} ({body.surfaceSignals.reportedBiologicalCount})\r\n" +
                       $"\tGeo Count is {signal.geoCount} ({body.surfaceSignals.reportedGeologicalCount})\r\n";
                
                // Predict possible biological genuses
                List<OrganicGenus> bios;
                log += "Predicting organics (by variant):\r\n";
                bios = new ExobiologyPredictions( EDDI.Instance?.CurrentStarSystem, body, parentStar, configuration ).PredictByVariant();

                // Account for predicting less than actual signals, lets player know that we don't know what one or more bios will be
                if( bios?.Count()<signal.bioCount )
                {
                    for(int i=bios.Count(); i<signal.bioCount; i++)
                    {
                        log += $"\t[Adding Unknown Genus: ";
                        OrganicGenus newGenus = OrganicGenus.Unknown;
                        newGenus.predictedMinimumValue = 1000000;
                        newGenus.predictedMaximumValue = 1000000;
                        bios.Add( newGenus );
                        log += $"count={bios.Count()}]\r\n";
                    }
                }

                foreach ( var genus in bios )
                {
                    log += $"\tAdding predicted bio {genus.invariantName}\r\n";
                    body.surfaceSignals.AddBioFromGenus( genus, true );
                }
                hasPredictedBios = true;
            }

            Logging.Debug( log );

            return hasPredictedBios;
        }

        // This was originally added to allow forced prediction updates from the GUI as a debugging option
        public void UpdatePredictedBios(ulong systemAddress, long? bodyId) {

            if( _currentSystem.systemAddress==systemAddress ) {

                var body = _currentSystem.BodyWithID( (long)bodyId );

                if ( TryUpdatePredictBios( ref body ) )
                {
                    EDDI.Instance.enqueueEvent( new OrganicPredictionEvent( DateTime.UtcNow, body, body.surfaceSignals.bioSignals ) );

                    // Save/Update Body data
                    body.surfaceSignals.lastUpdated = DateTime.UtcNow;
                    EDDI.Instance?.CurrentStarSystem.AddOrUpdateBody( body );
                    StarSystemSqLiteRepository.Instance.SaveStarSystem(EDDI.Instance.CurrentStarSystem);
                }
            }
        }

        // Forced refresh of predicted bios
        //  - Existing counts will remain
        //  - List of bios will be overwritten, including current status of any samples
        private bool TryUpdatePredictBios(ref Body body)
        {
            var log = "";
            var hasPredictedBios = false;

            // Erase existing bio signals
            body.surfaceSignals.bioSignals = new HashSet<Exobiology>();

            if ( body != null && 
                 !body.surfaceSignals.bioSignals.Any() && 
                 EDDI.Instance.CurrentStarSystem.TryGetMainStar(out var parentStar))
            {                
                List<OrganicGenus> bios;
                bios = new ExobiologyPredictions( EDDI.Instance?.CurrentStarSystem, body, parentStar, configuration ).PredictByVariant();

                // Account for predicting less than actual signals, lets player know that we don't know what one or more bios will be
                if( bios?.Count()<body.surfaceSignals.reportedBiologicalCount )
                {
                    for(int i=bios.Count(); i<body.surfaceSignals.reportedBiologicalCount; i++)
                    {
                        log += $"\t[Adding Unknown Genus: ";
                        OrganicGenus newGenus = OrganicGenus.Unknown;
                        newGenus.predictedMinimumValue = 1000000;
                        newGenus.predictedMaximumValue = 1000000;
                        bios.Add( newGenus );
                        log += $"count={bios.Count()}]\r\n";
                    }
                }

                foreach ( var genus in bios )
                {
                    log += $"\tAdding predicted bio {genus.invariantName}\r\n";
                    body.surfaceSignals.AddBioFromGenus( genus, true );
                }
                hasPredictedBios = true;
            }

            Logging.Debug( log );

            return hasPredictedBios;
        }

        /// <summary>
        /// Check if the current system and body exist
        /// </summary>
        private bool CheckSafe ()
        {
            if ( _currentOrganic != null )
            {
                if ( EDDI.Instance?.CurrentStarSystem != null )
                {
                    if ( EDDI.Instance?.CurrentStarSystem.BodyWithID( CurrentBodyId ) != null )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckSafe ( long? bodyId )
        {
            if ( EDDI.Instance?.CurrentStarSystem != null )
            {
                if ( EDDI.Instance?.CurrentStarSystem.BodyWithID( bodyId ) != null )
                {
                    CurrentBodyId = bodyId;
                    return true;
                }
            }

            return false;
        }

        public void PostHandle ( Event @event )
        { }

        public void HandleProfile ( JObject profile )
        { }

        public IDictionary<string, Tuple<Type, object>> GetVariables ()
        {
            return new Dictionary<string, Tuple<Type, object>>
            {
                [ "nearestnebula" ] = new Tuple<Type, object>( typeof( Nebula ), _nearestNebula ),
                [ "currentregion" ] = new Tuple<Type, object>( typeof( Region ), _currentRegion )
            };
        }
    }
}