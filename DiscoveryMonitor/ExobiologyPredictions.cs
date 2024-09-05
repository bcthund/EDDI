﻿using EddiConfigService.Configurations;
using EddiDataDefinitions;
using JetBrains.Annotations;
using System.Collections.Generic;
using System;
using System.Linq;
using Utilities;

namespace EddiDiscoveryMonitor
{
    public class ExobiologyPredictions
    {
        private readonly StarSystem _currentSystem;
        private readonly Body body;
        private readonly Body parentStar;
        private readonly DiscoveryMonitorConfiguration configuration;

        public ExobiologyPredictions ( [NotNull] StarSystem starSystem, [NotNull] Body body, [NotNull] Body parentStar, [NotNull] DiscoveryMonitorConfiguration configuration )
        {
            this._currentSystem = starSystem;
            this.body = body;
            this.parentStar = parentStar;
            this.configuration = configuration;
        }

        // This is a list so that we can have duplicate Genus, specifically for adding multiple 'Unknown' predictions
        //public HashSet<OrganicGenus> PredictByVariant ()
        public List<OrganicGenus> PredictByVariant ()
        {
            Logging.Debug( $"Generating predictions by variant for {body.bodyname} in {_currentSystem.systemname}.");

            var log = "";

            // Create temporary list of ALL variant possible
            var predictedVariants = new List<OrganicVariant>();

            // Iterate though variant
            foreach ( var variant in OrganicVariant.AllOfThem )
            {
                log = $"Checking variant {variant.edname} (genus: {variant.genus}): ";

                if ( !variant.isPredictable )
                {
                    log += "SKIP. No known criteria.";
                    Logging.Debug( log );
                    continue;
                }

                if ( !TryCheckConfiguration( variant.genus, ref log ) )
                {
                    Logging.Debug( log );
                    continue;
                }

                if ( TryCheckGravity( variant.minG, variant.maxG, ref log ) && 
                     TryCheckTemperature( variant.minK, variant.maxK, ref log ) && 
                     TryCheckPressure( variant.minP, variant.maxP, ref log ) && 
                     TryCheckPlanetClass( variant.planetClass, ref log ) && 
                     TryCheckAtmosphere( variant.atmosphereClass, ref log ) && 
                     TryCheckAtmosphereComposition( variant.atmosphereComposition, ref log ) &&
                     TryCheckVolcanism( variant.volcanism, ref log ) && 
                     //TryCheckMainStar( variant.primaryStar, ref log ) &&
                     TryCheckLocalStar( variant.localStar, ref log ) &&
                     TryCheckMaterials( variant.materials, ref log ) &&
                     TryCheckBodyTypePresent( variant.systemBodies, ref log ) &&
                     TryCheckNebulaDistance( variant.nebulaDistance, ref log ) &&
                     TryCheckDistanceFromArrival( variant.distanceFromArrival, ref log ) &&
                     TryCheckGeologyNum( variant.geologicalsPresent, ref log ) &&
                     TryCheckGuardianSector( variant.genus, ref log ) &&
                     TryCheckRegion( variant.regions, ref log ) )
                {
                    log += "OK";
                    predictedVariants.Add( variant );
                }

                Logging.Debug( log );
            }

            // Create a distinct genus list
            List<OrganicGenus> listGenus = predictedVariants.Select(s => s.genus).Distinct().ToList();

            if ( listGenus.Count() > 0 )
            {
                log = $"Setting Min/Max values:\r\n";

                // Iterate over all predicted variants, set the min/max values for the genus list
                for ( int i = 0; i < listGenus.Count(); i++ )
                {
                    log += $"\t[{listGenus[ i ].edname}]\r\n";
                    foreach ( var variant in predictedVariants )
                    {
                        if ( listGenus[ i ].edname == variant.genus.edname )
                        {
                            log += $"\t\t{variant.edname} ";
                            var species = OrganicSpecies.FromEDName( variant.species.edname );
                            if(species != null) {
                                if(listGenus[ i ].predictedMinimumValue == 0 || species.value < listGenus[ i ].predictedMinimumValue) {
                                    listGenus[ i ].predictedMinimumValue = species.value;
                                }

                                if(listGenus[ i ].predictedMaximumValue == 0 || species.value > listGenus[ i ].predictedMaximumValue) {
                                    listGenus[ i ].predictedMaximumValue = species.value;
                                }

                                log += $": value={species.value}, predictedMinimum={listGenus[ i ].predictedMinimumValue}, predictedMaximum={listGenus[ i ].predictedMaximumValue}\r\n";
                            }
                        }
                    }
                }
            }

            Logging.Debug( log );

            // Return an ordered list of only the unique genus' found
            return listGenus.OrderBy(o => o.invariantName).ToList();
        }

        private bool TryCheckConfiguration ( OrganicGenus genus, ref string log )
        {
            // Check if species should be ignored per configuration settings
            try
            {
                if ( ( configuration.exobiology.predictions.skipGroundStructIce && genus == OrganicGenus.Ground_Struct_Ice ) ||
                     ( configuration.exobiology.predictions.skipBrancae && genus == OrganicGenus.Brancae ) ||
                     ( configuration.exobiology.predictions.skipCone && genus == OrganicGenus.Cone ) ||
                     ( configuration.exobiology.predictions.skipTubers && genus == OrganicGenus.Tubers ) ||
                     ( configuration.exobiology.predictions.skipAleoids && genus == OrganicGenus.Aleoids ) ||
                     ( configuration.exobiology.predictions.skipVents && genus == OrganicGenus.Vents ) ||
                     ( configuration.exobiology.predictions.skipSphere && genus == OrganicGenus.Sphere ) ||
                     ( configuration.exobiology.predictions.skipBacterial && genus == OrganicGenus.Bacterial ) ||
                     ( configuration.exobiology.predictions.skipCactoid && genus == OrganicGenus.Cactoid ) ||
                     ( configuration.exobiology.predictions.skipClypeus && genus == OrganicGenus.Clypeus ) ||
                     ( configuration.exobiology.predictions.skipConchas && genus == OrganicGenus.Conchas ) ||
                     ( configuration.exobiology.predictions.skipElectricae && genus == OrganicGenus.Electricae ) ||
                     ( configuration.exobiology.predictions.skipFonticulus && genus == OrganicGenus.Fonticulus ) ||
                     ( configuration.exobiology.predictions.skipShrubs && genus == OrganicGenus.Shrubs ) ||
                     ( configuration.exobiology.predictions.skipFumerolas && genus == OrganicGenus.Fumerolas ) ||
                     ( configuration.exobiology.predictions.skipFungoids && genus == OrganicGenus.Fungoids ) ||
                     ( configuration.exobiology.predictions.skipOsseus && genus == OrganicGenus.Osseus ) ||
                     ( configuration.exobiology.predictions.skipRecepta && genus == OrganicGenus.Recepta ) ||
                     ( configuration.exobiology.predictions.skipStratum && genus == OrganicGenus.Stratum ) ||
                     ( configuration.exobiology.predictions.skipTubus && genus == OrganicGenus.Tubus ) ||
                     ( configuration.exobiology.predictions.skipTussocks && genus == OrganicGenus.Tussocks ) )
                {
                    log += "SKIP. Per configuration preferences.";
                    return false;
                }
            }
            catch ( Exception e )
            {
                Logging.Error("Failed to read configuration", e );
            }
            return true;
        }

        private bool TryCheckRegion(ICollection<string> checkRegions, ref string log )
        {
            if (checkRegions.Count() > 0)
            {
                var currentRegion = Utilities.RegionMap.RegionMap.FindRegion((double)_currentSystem.x, (double)_currentSystem.y, (double)_currentSystem.z);
                if (currentRegion != null) {
                    if (checkRegions.Any( a => a == currentRegion.name ) )
                    {
                        //log += $"ACCEPT. '{currentRegion.name}' is in '{string.Join(",", checkRegions)}'. ";
                        return true;
                    }
                    log += $"REJECT. Region: '{currentRegion.name}' not in '{string.Join(",", checkRegions)}'";
                }
                return false;
            }
            return true;
        }

        private bool TryCheckGravity ( decimal? minG, decimal? maxG, ref string log )
        {
            //log += $"[Gravity: body={body.gravity} min={minG} max={maxG}] ";

            if ( minG > 0 )
            {
                if ( body.gravity < minG )
                {
                    log += $"REJECT. Gravity: {body.gravity} < {minG}";
                    return false;
                }
            }

            if ( maxG > 0 )
            {
                if ( body.gravity > maxG )
                {
                    log += $"REJECT. Gravity: {body.gravity} > {maxG}";
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Evaluate whether a candidate organic's temperature range matches a given body.
        /// </summary>
        /// <param name="minK">Minimum temperature in Kelvin</param>
        /// <param name="maxK">Maximum temperature in Kelvin</param>
        /// <param name="log"></param>
        /// <returns></returns>
        private bool TryCheckTemperature(decimal? minK, decimal? maxK, ref string log )
        {
            if ( body.temperature < minK )
            {
                log += $"REJECT. Temp: {body.temperature} K < {minK} K.";
                return false;
            }

            if ( body.temperature > maxK )
            {
                log += $"REJECT. Temp: {body.temperature} K > {maxK} K.";
                return false;
            }

            return true;
        }

        private bool TryCheckPressure(decimal? minP, decimal? maxP, ref string log )
        {
            if ( body.pressure < minP )
            {
                log += $"REJECT. Pressure: {body.pressure} atm. < {minP} atm.";
                return false;
            }

            if ( body.pressure > maxP )
            {
                log += $"REJECT. Pressure: {body.pressure} atm. > {maxP} atm.";
                return false;
            }

            return true;
        }

        private bool TryCheckPlanetClass(ICollection<string> checkPlanetClasses, ref string log )
        {
            // Check if body has appropriate planet class
            if ( checkPlanetClasses.Count > 0 )
            {
                if ( checkPlanetClasses.Any( c =>
                        ( ( c == "None" || c == string.Empty ) && ( body.planetClass == null || body.planetClass == PlanetClass.None ) ) ||
                            c == "Any" ||
                            c == body.planetClass.edname ) )
                {
                    return true;
                }
                log += $"REJECT. Planet class: {( body.planetClass ?? PlanetClass.None )?.edname} not in {string.Join( ",", checkPlanetClasses )}.";
                return false;
            }

            return true;
        }

        private bool TryCheckAtmosphere(ICollection<string> checkAtmosphereClasses, ref string log )
        {
            // Check if body has appropriate astmosphere
            if ( checkAtmosphereClasses.Count > 0 )
            {
                foreach(var checkAtmosphereGroup in checkAtmosphereClasses)
                {

                    var checkParts = checkAtmosphereGroup.Split( ',' );

                    if( checkParts.Count() == 1 )
                    {
                        // Check Class only
                        if ( 
                            ( ( checkParts[0] == "None" || checkParts[0] == string.Empty ) && ( body.atmosphereclass == null || body.atmosphereclass == AtmosphereClass.None ) ) ||
                            checkParts[0] == "Any" ||
                            checkParts[0] == body.atmosphereclass.edname )
                        {
                            return true;
                        }
                    }
                    else if(checkParts.Count() >= 2 ) {

                        // Check Thickness
                        if ( 
                            ( ( checkParts[0] == "none" || checkParts[0] == string.Empty ) && ( body.atmospherethickness == null || body.atmospherethickness == AtmosphereThickness.None ) ) ||
                            checkParts[0] == "any" ||
                            checkParts[0] == body.atmospherethickness.edname )
                        {
                            // Check Class
                            if ( 
                                ( ( checkParts[1] == "None" || checkParts[1] == string.Empty ) && ( body.atmosphereclass == null || body.atmosphereclass == AtmosphereClass.None ) ) ||
                                checkParts[1] == "Any" ||
                                checkParts[1] == body.atmosphereclass.edname )
                            {
                                return true;
                            }
                        }
                    }
                    
                }
                log += $"REJECT. Atmosphere thickness,class: {( body.atmospherethickness ?? AtmosphereThickness.None )?.edname},{( body.atmosphereclass ?? AtmosphereClass.None )?.edname} not in {string.Join( ";", checkAtmosphereClasses )}.";
                return false;
            }

            return true;
        }

        private bool TryCheckAtmosphereComposition(ICollection<string> checkAtmosphereCompositions, ref string log )
        {
            // Check if body has appropriate astmosphere
            if ( checkAtmosphereCompositions.Count > 0 )
            {
                foreach(var checkAtmosphereGroup in checkAtmosphereCompositions)
                {
                    var checkParts = checkAtmosphereGroup.Split( ',' );

                    if( checkParts.Count() == 1 )
                    {
                        // Check composition
                        if( body.atmospherecompositions.Any( x => x.edname == checkParts[0] ) )
                        { 
                            return true;
                        }
                    }
                    else if(checkParts.Count() >= 2 ) {
                        // Check composition and amount
                        if (Decimal.TryParse( checkParts[1], out decimal checkPercent ))
                        {
                            if( body.atmospherecompositions.Any( x=> x.edname == checkParts[0] && x.percent >= checkPercent ) )
                            { 
                                return true;
                            }
                        }
                    }
                }
                log += $"REJECT. Atmosphere composition: {string.Join(";", body.atmospherecompositions.Select( x => string.Join(",", (new { x.edname, x.percent })) ).ToList()) } not in {string.Join( ";", checkAtmosphereCompositions )}.";
                return false;
            }
            return true;
        }

        private bool TryCheckVolcanism(ICollection<string> checkVolcanismCompositions, ref string log )
        {
            // Check if body has appropriate volcanism
            if ( checkVolcanismCompositions.Count > 0 )
            {
                if ( checkVolcanismCompositions.Any( c => 
                        ( ( c == "None" || c == string.Empty ) && body.volcanism == null ) ||
                            c == "Any" ||
                            c == body.volcanism?.edComposition ) )
                {
                    return true;
                }
                log += $"REJECT. Volcanism composition: {body.volcanism?.edComposition} not in {string.Join( ";", checkVolcanismCompositions )}.";
                return false;
            }

            return true;
        }

        private bool TryCheckVolcanismAdvanced(IList<string> checkVolcanismCompositions, ref string log )
        {
            // Check if body has appropriate volcanism
            if ( checkVolcanismCompositions.Count > 0 )
            {
                foreach(var composition in checkVolcanismCompositions) {

                    if( (composition=="None") || ( composition=="Any" && body.volcanism != null) || ( composition == body.volcanism?.ToString() ) ) {
                        return true;
                    }
                }

                log += $"REJECT. Volcanism composition: '{(body.volcanism is null ? "None" : body.volcanism?.ToString())}' not in [{String.Join(";", checkVolcanismCompositions)}].";
                return false;
            }

            return true;
        }

        private bool TryCheckMainStar ( ICollection<string> checkStar, ref string log )
        {
            if(checkStar.Count() > 0 ) {

                var result = _currentSystem.TryGetMainStar( out Body mainStar );

                if(mainStar!=null) {
                    foreach( var starGroup in checkStar) {
                        IList<string> starParts = starGroup.Split( ',' ).ToList();

                        if ( starParts[ 0 ] == mainStar.starClass.edname )
                        {
                            if ( starParts.Count >= 2 )
                            {
                                if ( mainStar.luminosityclass.Contains( starParts[ 1 ] ) )
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    // Failed to get parent stars, return True as this check isn't valid anymore
                    log += $"FAILED. Did not get any main star, pass by default. ";
                    return true;
                }

                log += $"REJECT. Main star/luminosity [{mainStar.starClass.edname}/{mainStar.luminosityclass}] not in {string.Join( ";", checkStar )}.";
                return false;
            }

                    return true;
        }

        private bool TryCheckLocalStar ( ICollection<string> checkStar, ref string log )
        {
            if(checkStar.Count() > 0 ) {

                HashSet<Body> parentStars = new HashSet<Body>();
                var result = _currentSystem.TryGetParentStars( body.bodyId, out parentStars );

                if(parentStars.Count()>0) {
                    foreach( var starGroup in checkStar) {
                        IList<string> starParts = starGroup.Split( ',' ).ToList();

                        foreach ( var parentStar in parentStars ) {
                            if ( starParts[0] == parentStar.starClass.edname )
                            {
                                if(starParts.Count >= 2) {
                                    if ( parentStar.luminosityclass.Contains(starParts[1]) ) {
                                        return true;
                                    }
                                }
                                else {
                                    return true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    // Failed to get parent stars, return True as this check isn't valid anymore
                    log += $"FAILED. Did not get any parent stars, pass by default. ";
                    return true;
                }

                log += $"REJECT. Parent star/luminosity [{string.Join(",", parentStars.Select( x => x.starClass.edname ) ) }/{string.Join(",", parentStars.Select( x => x.luminosityclass ) ) }] not in {string.Join(";", checkStar)}.";
                return false;
            }
            return true;
        }

        private bool TryCheckPrimaryStarClass ( string checkStarClass, ref string log )
        {
            // Check if body has appropriate parent star
            if ( checkStarClass != null && checkStarClass != "" )
            {
                HashSet<Body> parentStars = new HashSet<Body>();
                var result = _currentSystem.TryGetParentStars( body.bodyId, out parentStars );

                foreach ( var parentStar in parentStars ) {
                    if ( checkStarClass == parentStar.starClass.edname )
                    {
                        return true;
                    }
                }
                log += $"REJECT. Parent star [{string.Join(",", parentStars.Select( x => x.starClass.edname ) ) }] not in {checkStarClass}.";

                return false;
            }

            return true;
        }

        private bool TryCheckPrimaryStarLuminosity ( string checkStarLuminosity, ref string log )
        {
            // Check if body has appropriate parent star
            if ( checkStarLuminosity != null && checkStarLuminosity != "" )
            {
                HashSet<Body> parentStars = new HashSet<Body>();
                var result = _currentSystem.TryGetParentStars( body.bodyId, out parentStars );

                foreach ( var parentStar in parentStars ) {
                    if ( parentStar.luminosityclass.Contains(checkStarLuminosity) )
                    {
                        return true;
                    }
                }
                log += $"REJECT. Parent star luminosity [{string.Join(",", parentStars.Select(x => x.luminosity))}] not in {checkStarLuminosity}.";

                return false;
            }

            return true;
        }

        private bool TryCheckBodyTypePresent ( ICollection<string> checkBodyTypes, ref string log )
        {
            if ( checkBodyTypes.Count() > 0 )
            {
                foreach( var body in _currentSystem.bodies ) {
                    if(body != null && checkBodyTypes.Any( s => s == body.planetClass.edname ) ) {
                        return true;
                    }
                }
                log += $"REJECT. Body with type present [{string.Join(",", _currentSystem.bodies.Select( x => x.planetClass.edname ) ) }] not in {string.Join( ",", checkBodyTypes) }.";

                return false;
            }

            return true;
        }

        private bool TryCheckMaterials ( ICollection<string> checkMaterials, ref string log )
        {
            // Check if body has appropriate rare materials
            if ( checkMaterials.Count > 0 )
            {
                var bodyMaterials = body.materials.Select(x => x.name ).ToList();
                foreach(var mat in bodyMaterials) {
                    if(checkMaterials.Any( s => s == mat)) {
                        return true;
                    }
                }
                log += $"REJECT. Material [{string.Join( ",", body.materials.Select(x => x.name).ToList())}] not in {string.Join( ",", checkMaterials )}.";
                return false;
            }

            return true;
        }

        private bool TryCheckMaterial ( string checkMaterial, ref string log )
        {
            // Check if body has appropriate rare materials
            if ( checkMaterial != null && checkMaterial != "" )
            {
                if(body.materials.Any(x => x.name == checkMaterial && x.percentage>0)) {
                    return true;
                }
                log += $"REJECT. Material [{string.Join( ",", body.materials.Select(x => x.name).ToList())}] not in {string.Join( ",", checkMaterial )}.";
                return false;
            }
            return true;
        }

        private bool TryCheckGeologyNum ( decimal? checkGeologyNum, ref string log )
        {
            // Check if body has appropriate rare materials
            if ( checkGeologyNum != null && checkGeologyNum != 0 )
            {               
                if( body.surfaceSignals.reportedGeologicalCount >= checkGeologyNum )
                {
                    return true;
                }

                log += $"REJECT. Geology number present {body.surfaceSignals.reportedGeologicalCount} < {checkGeologyNum}.";
                return false;
            }
            return true;
        }

        private bool TryCheckNebulaDistance ( decimal? checkNebulaDistance, ref string log )
        {
            if( checkNebulaDistance != null && checkNebulaDistance != 0 ) {
                var nearestNebula = Nebula.TryGetNearestNebula( _currentSystem );
                if (nearestNebula != null) {
                    if ( nearestNebula.distance < checkNebulaDistance ) {
                        return true;
                    }
                }
                log += $"REJECT. Nebula distance [{(nearestNebula is null ? "Null" : nearestNebula.name)} @ {(nearestNebula is null ? "Null" : nearestNebula.distance.ToString())} Ly] > {checkNebulaDistance}.";
                return false;
            }

            return true;
        }

        private bool TryCheckDistanceFromArrival ( decimal? checkDistanceFromArrival, ref string log )
        {
            if( checkDistanceFromArrival != null && checkDistanceFromArrival != 0 ) {
                if( body.distance >= checkDistanceFromArrival ) {
                    return true;
                }
                log += $"REJECT. Distance from arrival [{body.distance}] < {checkDistanceFromArrival}.";
                return false;
            }
            return true;
        }

        private bool TryCheckGuardianSector ( OrganicGenus genus, ref string log )
        {
            if ( genus == OrganicGenus.Brancae )
            {
                var region = Utilities.RegionMap.RegionMap.FindRegion( (double)_currentSystem.x, (double)_currentSystem.y, (double)_currentSystem.z );

                if ( region != null )
                {
                    if( GuardianSector.TryGetGuardianSector(_currentSystem.systemname, region.name ) )
                    {
                        return true;
                    }
                }
                else
                {
                    if( GuardianSector.TryGetGuardianSector(_currentSystem.systemname ) )
                    {
                        return true;
                    }
                }

                log += $"REJECT. Not in known Guardian sector {_currentSystem.systemname}.";
                return false;
            }
            return true;
        }
    }
}