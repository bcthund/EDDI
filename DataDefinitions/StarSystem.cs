﻿using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using Utilities;

namespace EddiDataDefinitions
{
    /// <summary>Details for a star system</summary>
    public class StarSystem
    {
        // General information
        [Utilities.PublicAPI, JsonProperty("name"), JsonRequired]
        public string systemname { get; set; }

        public long? EDSMID { get; set; } // The ID in EDSM

        /// <summary>List of BaryCentre IDs with list of child BodyIds</summary>
        public BaryCentre baryCentre { get; set; }

        /// <summary>X co-ordinate for this system</summary>
        [Utilities.PublicAPI]
        public decimal? x { get; set; }

        /// <summary>Y co-ordinate for this system</summary>
        [Utilities.PublicAPI]
        public decimal? y { get; set; }

        /// <summary>Z co-ordinate for this system</summary>
        [Utilities.PublicAPI]
        public decimal? z { get; set; }

        /// <summary>Unique 64 bit id value for system</summary>
        [Utilities.PublicAPI, JsonProperty, JsonRequired]
        public ulong systemAddress { get; set; }

        /// <summary>Details of bodies (stars/planets/moons), kept sorted by ID</summary>
        [Utilities.PublicAPI, JsonProperty] // Required to deserialize to the private setter
        public ImmutableList<Body> bodies
        {
            get => _bodies;
            private set { _bodies = value; OnPropertyChanged();}
        }
        private ImmutableList<Body> _bodies;

        public Body BodyWithID(long? bodyID)
        {
            if (bodyID is null) { return null; }
            Body result = bodies.Find(b => b.bodyId == bodyID);
            return result;
        }

        public void AddOrUpdateBody(Body body)
        {
            var builder = bodies.ToBuilder();
            internalAddOrUpdateBody(body, builder);
            builder.Sort(Body.CompareById);
            bodies = builder.ToImmutable();
        }

        public void AddOrUpdateBodies(IEnumerable<Body> newBodies)
        {
            var builder = bodies.ToBuilder();
            foreach (Body body in newBodies)
            {
                internalAddOrUpdateBody(body, builder);
            }
            builder.Sort(Body.CompareById);
            bodies = builder.ToImmutable();
        }

        private void internalAddOrUpdateBody(Body newOrUpdatedBody, ImmutableList<Body>.Builder builder)
        {
            if ( newOrUpdatedBody is null ) { return; }

            int index = builder.FindIndex(b =>
                (b.bodyId != null && newOrUpdatedBody.bodyId != null && b.bodyId == newOrUpdatedBody.bodyId) || // Matching bodyId
                (!string.IsNullOrEmpty(b.bodyname) && !string.IsNullOrEmpty(newOrUpdatedBody.bodyname) && b.bodyname == newOrUpdatedBody.bodyname) || // Matching bodyName
                (b.distance == 0M && b.distance == newOrUpdatedBody.distance)); // Matching distance (for the main entry star only)
            if (index >= 0)
            {
                builder[index] = PreserveBodyData(builder[index], newOrUpdatedBody);
            }
            else
            {
                builder.Add(newOrUpdatedBody);
            }

            // Update the system reserve level, when appropriate
            if (newOrUpdatedBody.reserveLevel != ReserveLevel.None)
            {
                Reserve = newOrUpdatedBody.reserveLevel;
            }
        }

        public void ClearTemporaryStars()
        {
            var builder = bodies.ToBuilder();
            var bodiesToRemove = builder
                .Where( b => b.bodyId is null || string.IsNullOrEmpty( b.bodyname ) )
                .ToList();
            builder.RemoveRange(  bodiesToRemove );
            builder.Sort( Body.CompareById );
            bodies = builder.ToImmutable();
        }

        public void PreserveBodyData(List<Body> oldBodies, ImmutableList<Body> newBodies)
        {
            // Update `bodies` with new data, except preserve properties not available via the server
            var newBodyBuilder = newBodies.ToBuilder();
            foreach (Body oldBody in oldBodies)
            {
                if (newBodyBuilder.Any(b => b.bodyname == oldBody.bodyname))
                {
                    int index = newBodyBuilder.FindIndex(b => b.bodyname == oldBody.bodyname);
                    newBodyBuilder[index] = PreserveBodyData(oldBody, newBodyBuilder[index]);
                }
                else
                {
                    // `newBodies` did not contain the `oldBody` so we add it here, provided we've
                    // scanned the body ourselves so that we're confident that our old data is accurate. 
                    if (oldBody.scannedDateTime != null)
                    {
                        newBodyBuilder.Add(oldBody);
                    }
                }
            }
            newBodyBuilder.Sort(Body.CompareById);
            bodies = newBodyBuilder.ToImmutable();
        }

        private static Body PreserveBodyData(Body oldBody, Body updatedBody)
        {
            // TODO:#2212........[Remove]
            //Logging.Info( $"[PreserveBodyData] Body {oldBody.shortname}:  Reported old:({oldBody.surfaceSignals.bio.reportedTotal}), new:({updatedBody.surfaceSignals.bio.reportedTotal})" );
            //Thread.Sleep( 10 );
            //Logging.Info( $"[PreserveBodyData] Body {oldBody.shortname}:     Total old:({oldBody.surfaceSignals.bio.numTotal}), new:({updatedBody.surfaceSignals.bio.numTotal})" );
            //Thread.Sleep( 10 );
            //Logging.Info( $"[PreserveBodyData] Body {oldBody.shortname}:  Complete old:({oldBody.surfaceSignals.bio.numComplete}), new:({updatedBody.surfaceSignals.bio.numComplete})" );
            //Thread.Sleep( 10 );
            //Logging.Info( $"[PreserveBodyData] Body {oldBody.shortname}: Remaining old:({oldBody.surfaceSignals.bio.numRemaining}), new:({updatedBody.surfaceSignals.bio.numRemaining})" );
            //Thread.Sleep( 10 );

            // TODO:#2212........[Remove]
            //foreach ( Exobiology item in oldBody.surfaceSignals.bio.list.Values )
            //{
            //    Logging.Info( $"[PreserveBodyData] Body {oldBody.shortname}: bios old: {item.genus.name} ({item.samples} samples) ({item.complete}) ({item.coords[ 0 ].latitude},{item.coords[ 0 ].longitude}) ({item.coords[ 1 ].latitude},{item.coords[ 1 ].longitude})" );
            //    Thread.Sleep( 10 );
            //}

            // TODO:#2212........[Remove]
            //foreach ( Exobiology item in updatedBody.surfaceSignals.bio.list.Values )
            //{
            //    Logging.Info( $"[PreserveBodyData] Body {updatedBody.shortname}: bios new: {item.genus.name} ({item.samples} samples) ({item.complete}) ({item.coords[ 0 ].latitude},{item.coords[ 0 ].longitude}) ({item.coords[ 1 ].latitude},{item.coords[ 1 ].longitude})" );
            //    Thread.Sleep( 10 );
            //}


            if ( ( oldBody.scannedDateTime ?? DateTime.MinValue) > ( updatedBody.scannedDateTime ?? DateTime.MinValue ) )
            {
                updatedBody.scannedDateTime = oldBody.scannedDateTime;
            }

            if (oldBody.alreadydiscovered is true && 
                oldBody.alreadydiscovered != updatedBody.alreadydiscovered)
            {
                updatedBody.alreadydiscovered = oldBody.alreadydiscovered;
            }

            if ( ( oldBody.mappedDateTime ?? DateTime.MinValue ) > ( updatedBody.mappedDateTime ?? DateTime.MinValue ) )
            {
                updatedBody.mappedDateTime = oldBody.mappedDateTime;
            }

            if (oldBody.alreadymapped is true &&
                oldBody.alreadymapped != updatedBody.alreadymapped)
            {
                updatedBody.alreadymapped = oldBody.alreadymapped;
            }

            if (oldBody.mappedEfficiently &&
                oldBody.mappedEfficiently != updatedBody.mappedEfficiently)
            {
                updatedBody.mappedEfficiently = oldBody.mappedEfficiently;
            }

            if (oldBody.rings?.Any() ?? false)
            {
                if (updatedBody.rings is null)
                {
                    updatedBody.rings = new List<Ring>();
                }

                foreach (var oldRing in oldBody.rings)
                {
                    var newRing = updatedBody.rings.FirstOrDefault(r => r.name == oldRing.name);
                    if (oldRing.mapped != null)
                    {
                        if (newRing != null)
                        {
                            newRing.mapped = oldRing.mapped;
                            newRing.hotspots = oldRing.hotspots;
                        }
                        else
                        {
                            // Our data source didn't contain any data about a ring we've scanned.
                            // We add it here because we scanned the ring ourselves and are confident that the data is accurate
                            updatedBody.rings.Add(oldRing);
                        }
                    }
                }
            }

            // Third party sites do not have biological data (currently)
            // Assume that any bio or geo object with a higher numTotal is accurate
            if ( updatedBody.surfaceSignals != null && oldBody.surfaceSignals != null )
            {
                if ( updatedBody.surfaceSignals.bio.reportedTotal < oldBody.surfaceSignals.bio.reportedTotal )
                {
                    updatedBody.surfaceSignals.bio = oldBody.surfaceSignals.bio;
                    log += $"[PreserveBodyData] Using oldBody for bios\r\n";
                }
                else
                {
                    log += $"[PreserveBodyData] Using updatedBody for bios\r\n";
                }

                if ( updatedBody.surfaceSignals.geo.reportedTotal < oldBody.surfaceSignals.geo.reportedTotal )
                {
                    updatedBody.surfaceSignals.geo = oldBody.surfaceSignals.geo;
                    log += $"[PreserveBodyData] Using oldBody for geos\r\n";
                }
                else
                {
                    log += $"[PreserveBodyData] Using updatedBody for geos\r\n";
                }
            }
            else
            {
                if ( updatedBody.surfaceSignals == null )
                {
                    updatedBody.surfaceSignals = new SurfaceSignals();
                    log += $"[PreserveBodyData] updatedBody is null, aborting\r\n";
                }

                if ( oldBody.surfaceSignals == null )
                {
                    log += $"[PreserveBodyData] oldBody is null, aborting\r\n";
                }
            }

            log += $"[PreserveBodyData] ---> END <---";
            Logging.Info( log );
            Thread.Sleep( 10 );

            return updatedBody;
        }

        /// <summary>True if any star in the system is scoopable</summary>
        [Utilities.PublicAPI]
        public bool scoopable => bodies.Any(b => b.scoopable);

        /// <summary>The reserve level applicable to the system's rings</summary>
        public ReserveLevel Reserve
        {
            get => _reserve;
            set { _reserve = value; OnPropertyChanged();}
        }
        private ReserveLevel _reserve = ReserveLevel.None;

        [Utilities.PublicAPI, JsonIgnore]
        public string reserves => (Reserve ?? ReserveLevel.None).localizedName;

        // Populated system data

        [Utilities.PublicAPI]
        public long? population { get; set; } = 0;

        [Utilities.PublicAPI, JsonIgnore]
        public string primaryeconomy => (Economies.FirstOrDefault() ?? Economy.None).localizedName;

        public List<Economy> Economies
        {
            get => _economies;
            set { _economies = value; OnPropertyChanged();}
        }
        private List<Economy> _economies = new List<Economy>();

        /// <summary>The system's security level</summary>
        public SecurityLevel securityLevel { get; set; } = SecurityLevel.None;

        /// <summary>The system's security level (localized name)</summary>
        [Utilities.PublicAPI, JsonIgnore]
        public string security => (securityLevel ?? SecurityLevel.None).localizedName;

        /// <summary> The powerplay power exerting influence within the system (null if contested)</summary>
        [JsonIgnore]
        public Power Power => Powers.Count > 1 ? null : Powers.FirstOrDefault();

        /// <summary> The powerplay powers exerting influence within the system (may include multiple when the system is contested) </summary>
        public List<Power> Powers { get; set; } = new List<Power>();

        [Utilities.PublicAPI, JsonIgnore]
        public string power => (Power ?? Power.None).localizedName;

        /// <summary> The state of powerplay within the system </summary>
        public PowerplayState powerState { get; set; }

        [Utilities.PublicAPI, JsonIgnore]
        public string powerstate => (powerState ?? PowerplayState.None).localizedName;

        [Utilities.PublicAPI, JsonIgnore]
        public string state => (Faction?.presences.FirstOrDefault(p => p.systemName == systemname)?.FactionState ?? FactionState.None).localizedName;

        // Faction details
        [CanBeNull	]
        public Faction Faction
        {
            get => _faction;
            set { _faction = value; OnPropertyChanged();}
        }
        private Faction _faction = new Faction();

        [Utilities.PublicAPI]
        public List<Faction> factions
        {
            get => _factions;
            set { _factions = value; OnPropertyChanged();}
        }
        private List<Faction> _factions;

        [Utilities.PublicAPI, JsonIgnore, Obsolete("Please use Faction instead")]
        public string faction => Faction?.name;

        [Utilities.PublicAPI, JsonIgnore, Obsolete("Please use Faction.Allegiance instead")]
        public string allegiance => (Faction?.Allegiance ?? Superpower.None).localizedName;

        [Utilities.PublicAPI, JsonIgnore, Obsolete("Please use Faction.Government instead")]
        public string government => (Faction?.Government ?? Government.None).localizedName;

        [Utilities.PublicAPI( "Faction conflicts data. Currently only available for recently visited star systems." ), JsonIgnore]
        public List<Conflict> conflicts
        {
            get => _conflicts;
            set { _conflicts = value; OnPropertyChanged(); }
        }
        private List<Conflict> _conflicts;

        [ Utilities.PublicAPI("Thargoid war data. Currently only available for recently visited star systems." ), JsonIgnore ]
        public ThargoidWar ThargoidWar
        {
            get => _thargoidWar;
            set { _thargoidWar = value; OnPropertyChanged(); }
        }
        private ThargoidWar _thargoidWar;

        /// <summary>Details of stations</summary>
        [Utilities.PublicAPI]
        public List<Station> stations
        {
            get => _stations;
            set { _stations = value; OnPropertyChanged();}
        }
        private List<Station> _stations;

        /// <summary>Summary info for stations</summary>
        [Utilities.PublicAPI, JsonIgnore]
        public List<Station> planetarystations => stations.FindAll(s => s.IsPlanetary());

        [Utilities.PublicAPI, JsonIgnore]
        public List<Station> orbitalstations => stations.FindAll(s => (s.hasdocking ?? false) 
            && !s.IsPlanetary() 
            && !s.IsCarrier() 
            && !s.IsMegaShip());

        /// <summary> Whether this system requires a permit for visiting </summary>
        [Utilities.PublicAPI]
        public bool requirespermit { get; set; }

        /// <summary> The name of the permit required for visiting this system, if any </summary>
        [Utilities.PublicAPI]
        public string permitname { get; set; }

        // Other data

        /// <summary>Types of signals detected within the system</summary>
        [JsonIgnore]
        public ImmutableList<SignalSource> signalSources 
        { 
            get 
            {
                _signalSources = _signalSources.RemoveAll(s => s.expiry != null && s.expiry < DateTime.UtcNow);
                return _signalSources; 
            } 
            set 
            {
                _signalSources = value; 
                OnPropertyChanged();
            } 
        }

        [JsonIgnore]
        private ImmutableList<SignalSource> _signalSources = ImmutableList<SignalSource>.Empty;

        [Utilities.PublicAPI, JsonIgnore]
        public List<string> signalsources => signalSources.Select(s => s.localizedName).Distinct().ToList();

        public void AddOrUpdateSignalSource(SignalSource signalSource)
        {
            var builder = signalSources.ToBuilder();
            builder.Add(signalSource);
            signalSources = builder.ToImmutable();
        }

        /// <summary> Signals filtered to only return results with a carrier callsign </summary>
        [Utilities.PublicAPI, JsonIgnore]
        public List<string> carriersignalsources => signalSources
            .Where(s => new Regex("[[a-zA-Z0-9]{3}-[[a-zA-Z0-9]{3}$").IsMatch(s.invariantName) 
                && (s.isStation ?? false))
            .Select(s => s.localizedName)
            .ToList();

        /// <summary> Whether the system is a "green" system for exploration (containing all FSD synthesis elements) </summary>
        [Utilities.PublicAPI, JsonIgnore]
        public bool isgreen => materialsAvailable.IsSupersetOf(Material.jumponiumElements);

        /// <summary> Whether the system is a "gold" system for exploration (containing all elements available from planetary surfaces) </summary>
        [Utilities.PublicAPI, JsonIgnore]
        public bool isgold => materialsAvailable.IsSupersetOf(Material.surfaceElements);

        /// <summary>Number of visits</summary>
        [Utilities.PublicAPI, JsonIgnore]
        public long estimatedvalue => estimateSystemValue();

        /// <summary>Number of visits</summary>
        [Utilities.PublicAPI]
        public int visits => visitLog.Count();

        /// <summary>Time of last visit</summary>
        public DateTime? lastvisit => visitLog.LastOrDefault();

        /// <summary>Visit log</summary>
        public readonly SortedSet<DateTime> visitLog = new SortedSet<DateTime>();

        /// <summary>Time of last visit, expressed as a Unix timestamp in seconds</summary>
        [Utilities.PublicAPI, JsonIgnore]
        public long? lastVisitSeconds => lastvisit > DateTime.MinValue ? (long?)Dates.fromDateTimeToSeconds((DateTime)lastvisit) : null;

        /// <summary>comment on this starsystem</summary>
        [Utilities.PublicAPI]
        public string comment;

        /// <summary>distance from home</summary>
        [Utilities.PublicAPI, JsonIgnore]
        public decimal? distancefromhome;

        /// <summary>Whether a system scan has already been completed for this system in the current play session</summary>
        [JsonIgnore]
        public bool systemScanCompleted;

        // Not intended to be user facing - materials available within the system
        [JsonIgnore]
        private HashSet<Material> materialsAvailable => bodies?
            .SelectMany(b => b.materials)
            .Select(m => m.definition)
            .Where(m => m != null)
            .Distinct()
            .OrderByDescending(m => m.Rarity.level)
            .ToHashSet() ?? new HashSet<Material>();

        // Not intended to be user facing - materials available from system bodies
        [Utilities.PublicAPI, JsonIgnore]
        public HashSet<string> surfaceelements => materialsAvailable
            .Select(m => m.localizedName).ToHashSet();

        // Discoverable bodies as reported by a discovery scan "honk"
        [Utilities.PublicAPI, JsonProperty("discoverableBodies")]
        public int totalbodies
        {
            get => _totalbodies;
            set { _totalbodies = value; OnPropertyChanged();}
        }
        private int _totalbodies;

        [Utilities.PublicAPI, JsonIgnore]
        public int scannedbodies => bodies.Count(b => b.scannedDateTime != null);

        [Utilities.PublicAPI, JsonIgnore]
        public int mappedbodies => bodies.Count(b => b.mappedDateTime != null);

        // Not intended to be user facing - the last time the information present changed
        [Utilities.PublicAPI]
        public long? updatedat;

        // Not intended to be user facing - the last time the data about this system was obtained from remote repository
        public DateTime lastupdated;

        // Deprecated properties (preserved for backwards compatibility with Cottle and database stored values)

        // This is a key for legacy json files that cannot be changed without breaking backwards compatibility. 
        [JsonIgnore, Obsolete("Please use systemname instead.")]
        public string name => systemname;

        [JsonExtensionData]
        private IDictionary<string, JToken> additionalJsonData;

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            OnFactionDeserialized();
            additionalJsonData = null;
        }

        private void OnFactionDeserialized()
        {
            if (Faction == null) { Faction = new Faction(); }
            FactionPresence factionPresence = Faction.presences.FirstOrDefault(p => p.systemName == systemname) ?? new FactionPresence();
            if (factionPresence.FactionState == null)
            {
                // Convert legacy data
                if (additionalJsonData.TryGetValue("state", out var fState))
                {
                    string factionState = (string)fState;
                    if (factionState != null)
                    {
                        factionPresence.FactionState = FactionState.FromEDName(factionState) ?? FactionState.None;
                    }
                }
            }
            else
            {
                // get the canonical FactionState object for the given EDName
                factionPresence.FactionState =
                    FactionState.FromEDName(Faction.presences.FirstOrDefault(p => p.systemName == systemname)?.FactionState.edname) ?? FactionState.None;
            }
        }

        public StarSystem()
        {
            bodies = ImmutableList.Create<Body>();
            factions = new List<Faction>();
            stations = new List<Station>();
            baryCentre = new BaryCentre();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private long estimateSystemValue()
        {
            // Credit to MattG's thread at https://forums.frontier.co.uk/showthread.php/232000-Exploration-value-formulae for scan value formulas

            if (bodies == null || bodies.Count == 0)
            {
                return 0;
            }

            long value = 0;

            // Add the estimated value for each body
            foreach (Body body in bodies)
            {
                value += body.estimatedvalue;
            }

            // Bonus for fully discovering a system
            if (totalbodies == bodies.Count(b => b.scannedDateTime != null))
            {
                value += totalbodies * 1000;

                // Bonus for fully mapping a system
                int mappableBodies = bodies.Count(b => b.bodyType.invariantName != "Star");
                if (mappableBodies == bodies.Count(b => b.mappedDateTime != null))
                {
                    value += mappableBodies * 10000;
                }
            }

            return value;
        }

        public decimal? DistanceFromStarSystem(StarSystem other)
        {
            if (other is null) { return null; }
            return Functions.StellarDistanceLy(x, y, z, other.x, other.y, other.z);
        }
    }
}