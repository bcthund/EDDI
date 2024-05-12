using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Utilities;

namespace EddiDataDefinitions
{
    /// <summary>A ship</summary>
    public class Ship : INotifyPropertyChanged
    {
        /// <summary>the ID of this ship for this commander</summary>
        public int LocalId { get; set; }

        /// <summary>the manufacturer of the ship (Lakon, CoreDynamics etc.)</summary>
        [PublicAPI, JsonIgnore]
        public string manufacturer { get; set; }

        /// <summary>the spoken manufacturer of the ship (Lakon, CoreDynamics etc.) (rendered using ssml and IPA)</summary>
        [PublicAPI, JsonIgnore]
        public string phoneticmanufacturer => SpokenManufacturer();

        /// <summary>the spoken model of the ship (Python, Anaconda, etc.) (rendered using ssml and IPA)</summary>
        [PublicAPI, JsonIgnore]
        public string phoneticmodel => SpokenModel();

        [JsonIgnore]
        public List<Translation> phoneticModel { get; set; }

        /// <summary>the size of this ship</summary>
        [JsonIgnore]
        public LandingPadSize Size { get; set; } = LandingPadSize.Small;

        /// <summary>the spoken size of this ship</summary>
        [PublicAPI, JsonIgnore]
        public string size => (Size ?? LandingPadSize.Small).localizedName;

        /// <summary>the size of the military compartment slots</summary>
        [JsonIgnore]
        public int? militarysize { get; set; }

        /// <summary>the total tonnage cargo capacity</summary>
        [PublicAPI]
        public int cargocapacity { get; set; }

        /// <summary>the value of the ship without cargo, in credits</summary>

        [PublicAPI]
        public long value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        private long _value;

        /// <summary>the value of the ship's hull, in credits</summary>

        [PublicAPI]
        public long? hullvalue
        {
            get
            {
                return _hullvalue;
            }
            set
            {
                if (_hullvalue != value)
                {
                    _hullvalue = value;
                    OnPropertyChanged();
                }
            }
        }

        private long? _hullvalue;

        /// <summary>the value of the ship's hull, in credits</summary>

        [PublicAPI]
        public long? modulesvalue
        {
            get
            {
                return _modulesvalue;
            }
            set
            {
                if (_modulesvalue != value)
                {
                    _modulesvalue = value;
                    OnPropertyChanged();
                }
            }
        }

        private long? _modulesvalue;

        /// <summary>the value of the ship's rebuy, in credits</summary>
        [PublicAPI]
        public long rebuy
        {
            get => _rebuy;
            set { _rebuy = value; OnPropertyChanged(); }
        }

        private long _rebuy;

        /// <summary>the name of this ship</summary>
        [PublicAPI]
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _name;

        /// <summary>the model of the ship (Python, Anaconda, Cobra Mk. III, etc.)</summary>
        [PublicAPI]
        public string model
        {
            get
            {
                return _model;
            }
            set
            {
                if (_model != value)
                {
                    _model = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _model;

        /// <summary>the identifier of this ship</summary>

        [PublicAPI]
        public string ident
        {
            get
            {
                return _ident;
            }
            set
            {
                if (_ident != value)
                {
                    _ident = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _ident;

        /// <summary>the phonetic name of this ship</summary>

        [JsonProperty("phoneticname")]
        public string phoneticName
        {
            get => _phoneticName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _phoneticName = null;
                }
                else if (IPA.IsValid(value))
                {
                    OnPropertyChanged();
                    _phoneticName = value;
                }
                OnPropertyChanged();
            }
        }

        /// <summary>The ship's spoken name (rendered using ssml and IPA)</summary>

        [PublicAPI, JsonIgnore]
        public string phoneticname => SpokenName();

        [JsonIgnore]
        private string _phoneticName;

        /// <summary>the role of this ship</summary>

        [JsonProperty]
        public string roleEDName
        {
            get => Role.edname;
            set
            {
                var rDef = Role.FromEDName(value);
                Role = rDef;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public Role Role
        {
            get => _Role;
            set
            {
                if (_Role != value)
                {
                    _Role = value;
                    OnPropertyChanged();
                }
            }
        }
        private Role _Role = Role.MultiPurpose;

        [PublicAPI, JsonIgnore, Obsolete("Please use localizedName or invariantName")]
        public string role => Role?.localizedName; // This string is made available for Cottle scripts that vary depending on the ship's role. 

        [JsonExtensionData]
        private IDictionary<string, JToken> additionalJsonData;

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (Role == null) // legacy shipmonitor JSON
            {
                string roleName = (string)additionalJsonData["role"];
                Role = Role.FromEDName(roleName) ?? Role.FromName(roleName);
            }
            else
            {
                // get the canonical role object for the given EDName
                Role = Role.FromEDName(Role.edname);
            }

            if (EDName is null && !(model is null)) // legacy shipmonitor JSON may not include EDName or EDID
            {
                Ship template = ShipDefinitions.FromModel(model);
                EDName = EDName ?? template?.EDName;
            }
            additionalJsonData = null;
        }

        /// <summary>
        /// The raw JSON from the companion API for this ship
        /// </summary>
        public string raw
        {
            get
            {
                return _raw;
            }
            set
            {
                if (_raw != value)
                {
                    _raw = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _raw;

        public bool RawIsNotNull => !string.IsNullOrEmpty(_raw);

        /// <summary>
        /// The wanted/hot status of this ship
        /// </summary>
        [PublicAPI, JsonIgnore]
        public bool hot
        {
            get
            {
                return _hot;
            }
            set
            {
                if (_hot != value)
                {
                    _hot = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _hot = false;

        /// <summary>the name of the system in which this ship is stored; null if the commander is in this ship</summary>
        [PublicAPI]
        public string starsystem
        {
            get
            {
                return _starsystem;
            }
            set
            {
                if (_starsystem != value)
                {
                    _starsystem = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _starsystem;

        [Obsolete("Please use 'starsystem' instead")]
        public string system => starsystem; // Legacy Cottle scripts may use `system` rather than `starsystem`. 

        /// <summary>the name of the station in which this ship is stored; null if the commander is in this ship</summary>

        [PublicAPI]
        public string station
        {
            get => _station;
            set { _station = value; OnPropertyChanged(); }
        }
        private string _station;

        [PublicAPI]
        public long? marketid
        {
            get => _marketid;
            set { _marketid = value; OnPropertyChanged(); }
        }
        private long? _marketid;

        public decimal? x { get; set; }

        public decimal? y { get; set; }

        public decimal? z { get; set; }

        public bool intransit { get; set; }

        public long? transferprice { get; set; }

        public long? transfertime { get; set; }

        [PublicAPI]
        public decimal? distance
        {
            get => _distance;
            set { _distance = value; OnPropertyChanged(); }
        }

        private decimal? _distance;

        [PublicAPI]
        public decimal health
        {
            get => _health;
            set { _health = value; OnPropertyChanged(); }
        }
        private decimal _health = 100M;

        [PublicAPI]
        public Module cargohatch
        {
            get => _cargohatch;
            set { _cargohatch = value; OnPropertyChanged(); }
        }
        private Module _cargohatch = new Module();

        [PublicAPI]
        public Module bulkheads
        {
            get => _bulkheads;
            set { _bulkheads = value; OnPropertyChanged(); }
        }
        private Module _bulkheads = new Module();

        [PublicAPI]
        public Module canopy
        {
            get => _canopy;
            set { _canopy = value; OnPropertyChanged(); }
        }
        private Module _canopy = new Module();

        [PublicAPI]
        public Module powerplant
        {
            get => _powerplant;
            set { _powerplant = value; OnPropertyChanged(); }
        }
        private Module _powerplant = new Module();

        [PublicAPI]
        public Module thrusters
        {
            get => _thrusters;
            set { _thrusters = value; OnPropertyChanged(); }
        }
        private Module _thrusters = new Module();

        [PublicAPI]
        public Module frameshiftdrive
        {
            get => _frameshiftdrive;
            set
            {
                _frameshiftdrive = value;
                maxfuelperjump = value?.GetFsdMaxFuelPerJump() ?? 0;
                optimalmass = value?.GetFsdOptimalMass() ?? 0;
                OnPropertyChanged();
            }
        }
        private Module _frameshiftdrive = new Module();

        [PublicAPI]
        public Module lifesupport
        {
            get => _lifesupport;
            set { _lifesupport = value; OnPropertyChanged(); }
        }
        private Module _lifesupport = new Module();

        [PublicAPI]
        public Module powerdistributor
        {
            get => _powerdistributor;
            set { _powerdistributor = value; OnPropertyChanged(); }
        }
        private Module _powerdistributor = new Module();

        [PublicAPI]
        public Module sensors
        {
            get => _sensors;
            set { _sensors = value; OnPropertyChanged(); }
        }
        private Module _sensors = new Module();

        [PublicAPI]
        public Module fueltank
        {
            get => _fueltank;
            set { _fueltank = value; OnPropertyChanged(); }
        }
        private Module _fueltank = new Module();

        [PublicAPI, JetBrains.Annotations.NotNull, JetBrains.Annotations.ItemNotNull]
        public List<Hardpoint> hardpoints
        {
            get => _hardpoints;
            set { _hardpoints = value; OnPropertyChanged(); }
        }
        private List<Hardpoint> _hardpoints = new List<Hardpoint>();

        [PublicAPI, JetBrains.Annotations.NotNull, JetBrains.Annotations.ItemNotNull]
        public List<Compartment> compartments
        {
            get => _compartments;
            set { _compartments = value; OnPropertyChanged(); }
        }
        private List<Compartment> _compartments = new List<Compartment>();

        [PublicAPI, JetBrains.Annotations.NotNull, JetBrains.Annotations.ItemNotNull]
        public List<LaunchBay> launchbays
        {
            get => _launchbays;
            set { _launchbays = value; OnPropertyChanged(); }
        }
        private List<LaunchBay> _launchbays = new List<LaunchBay>();

        public string paintjob { get; set; }

        [PublicAPI]
        public double? fueltankcapacity // Core capacity
        {
            get => _fueltankcapacity;
            set { _fueltankcapacity = value ?? 0; OnPropertyChanged(); }
        }
        private double _fueltankcapacity;

        [PublicAPI]
        public double? fueltanktotalcapacity // Capacity including additional tanks
        {
            get => _fueltanktotalcapacity;
            set { _fueltanktotalcapacity = value ?? 0; OnPropertyChanged(); }
        }
        private double _fueltanktotalcapacity;

        public double activeFuelReservoirCapacity { get; set; }

        // Ship jump and mass properties

        [PublicAPI]
        public double maxjumprange 
        {
            get => _maxjumprange;
            set
            {
                if (value > 0)
                {
                    _maxjumprange = value;
                }
                else
                {
                    _maxjumprange = JumpRange(fuelInTanks ?? 0, 0);
                }

                OnPropertyChanged(nameof(maxjumprange));
            }
        }
        private double _maxjumprange;

        [JsonIgnore, Obsolete("Please use maxjumprange instead")]
        public double maxjump => maxjumprange;

        [PublicAPI]
        public double maxfuelperjump
        {
            get => _maxfuelperjump;
            set
            {
                _maxfuelperjump = value > 0 
                    ? value 
                    : frameshiftdrive?.GetFsdMaxFuelPerJump() ?? 0;
                OnPropertyChanged(nameof(maxfuelperjump));
            }
        }
        private double _maxfuelperjump;

        [JsonIgnore, Obsolete("Please use maxfuelperjump instead")]
        public double maxfuel => maxfuelperjump;

        public double optimalmass 
        {
            get => _optimalmass;
            set
            {
                _optimalmass = value > 0
                    ? value
                    : frameshiftdrive?.GetFsdOptimalMass() ?? 0;
                OnPropertyChanged(nameof(optimalmass));
            }
        }
        private double _optimalmass;

        public double unladenmass { get; set; }

        public double? fuelInTanks
        {
            get => _fuelInTanks;
            set
            {
                _fuelInTanks = value ?? 0;
                maxjumprange = JumpRange( _fuelInTanks, 0 );
            }
        }
        private double _fuelInTanks;

        public int cargoCarried { get; set; }

        // Admin

        // The name in Elite: Dangerous' database
        public string EDName { get; set; }

        // The context for the possessive "your" used to describe your ship
        [JsonIgnore] 
        internal string possessiveYour { get; set; } = nameof(Properties.Ship.yourSidewinder); // Default context is a Sidewinder

        public Ship()
        { }

        public Ship( string EDName, ShipManufacturer Manufacturer, string Model, string possessiveYour, List<Translation> PhoneticModel, LandingPadSize Size, int? MilitarySize, double reservoirFuelTankSize )
        {
            this.EDName = EDName;
            manufacturer = Manufacturer.name;
            model = Model;
            this.possessiveYour = possessiveYour;
            phoneticModel = PhoneticModel;
            this.Size = Size;
            militarysize = MilitarySize;
            activeFuelReservoirCapacity = reservoirFuelTankSize;
        }

        public override string ToString()
        {
            // This is mostly to help with debugging
            return name ?? $"{Role.localizedName} {model}";
        }

        public string SpokenName(string defaultname = null)
        {
            string result;
            if (!string.IsNullOrWhiteSpace(phoneticName))
            {
                result = "<phoneme alphabet=\"ipa\" ph=\"" + phoneticName + "\">" + name + "</phoneme>";
            }
            else if (!string.IsNullOrWhiteSpace(name))
            {
                result = name;
            }
            else
            {
                result = $"{Properties.Ship.ResourceManager.GetString(possessiveYour) ?? Properties.Ship.your} {(defaultname ?? phoneticmodel) ?? Properties.Ship._ship}";
            }
            return result;
        }

        public string SpokenModel()
        {
            string result;
            if (phoneticModel == null)
            {
                result = model;
            }
            else
            {
                result = "";
                foreach (Translation item in phoneticModel)
                {
                    result += "<phoneme alphabet=\"ipa\" ph=\"" + item.to + "\">" + item.from + "</phoneme> ";
                }
            }
            return result;
        }

        public string SpokenManufacturer() => ShipManufacturer.SpokenManufacturer(manufacturer) ?? manufacturer;

        /// <summary> Calculates the distance from the specified coordinates to the ship's recorded x, y, and z coordinates </summary>
        public decimal? Distance(decimal? fromX, decimal? fromY, decimal? fromZ)
        {
            // Work out the distance to the system where the ship is stored if we can
            return Functions.StellarDistanceLy(x, y, z, fromX, fromY, fromZ);
        }

        public string CoriolisUri()
        {
            if (raw != null)
            {
                // Generate a Coriolis import URI to retain as much information as possible
                string uri = "https://coriolis.io/import?";

                // Take the ship's JSON, gzip it, then turn it in to base64 and attach it to the base uri
                var bytes = Encoding.UTF8.GetBytes(raw);
                using (var streamIn = new MemoryStream(bytes))
                using (var streamOut = new MemoryStream())
                {
                    using (var gzipStream = new GZipStream(streamOut, CompressionLevel.Optimal, true))
                    {
                        streamIn.CopyTo(gzipStream);
                    }
                    uri += "data=" + Uri.EscapeDataString(Convert.ToBase64String(streamOut.ToArray()));
                }

                // Add the ship's name
                string bn = name ?? $"{Role.localizedName} {model}";
                uri += "&bn=" + Uri.EscapeDataString(bn);

                return uri;
            }
            return null;
        }

        public string EDShipyardUri()
        {
            // Once Coriolis supports POSTing, we can switch to POSTing to https://edsy.org/import

            if (raw != null)
            {
                // Generate an EDShipyard import URI to retain as much information as possible
                string uri = "https://edsy.org/";

                // Take the ship's JSON, gzip it, then turn it in to base64 and attach it to the base uri
                string unescapedraw = raw.Replace(@"\""", @"""");
                var bytes = Encoding.UTF8.GetBytes(unescapedraw);
                using (var streamIn = new MemoryStream(bytes))
                using (var streamOut = new MemoryStream())
                {
                    using (var gzipStream = new GZipStream(streamOut, CompressionLevel.Optimal, true))
                    {
                        streamIn.CopyTo(gzipStream);
                    }
                    uri += "#/I=" + Uri.EscapeDataString(Convert.ToBase64String(streamOut.ToArray()));
                }

                return uri;
            }
            return null;
        }

        /// <summary>
        /// Augment the ship's information from the model
        /// </summary>
        public void Augment()
        {
            Ship template = ShipDefinitions.FromModel(model);
            if (template != null)
            {
                EDName = template.EDName;
                manufacturer = template.manufacturer;
                possessiveYour = template.possessiveYour;
                phoneticModel = template.phoneticModel;
                Size = template.Size;
                militarysize = template.militarysize;
                activeFuelReservoirCapacity = template.activeFuelReservoirCapacity;
                if (Role == null)
                {
                    Role = Role.MultiPurpose;
                }
            }
        }

        public JumpDetail JumpDetails(string type, double? fuelInTanksOverride = null, int? cargoCarriedOverride = null)
        {
            var currentFuel = fuelInTanksOverride ?? fuelInTanks ?? 0;
            var cargoTonnage = cargoCarriedOverride ?? cargoCarried;

            if (string.IsNullOrEmpty(type)) { return null; }


            if (!string.IsNullOrEmpty(type))
            {
                switch (type)
                {
                    case "next":
                        {
                            var jumpRange = JumpRange( Math.Min( currentFuel, maxfuelperjump ), cargoTonnage );
                            return new JumpDetail(jumpRange, 1);
                        }
                    case "max":
                        {
                            var jumpRange = JumpRange( maxfuelperjump, cargoTonnage );
                            return new JumpDetail(jumpRange, 1);
                        }
                    case "total":
                        {
                            double total = 0;
                            int jumps = 0;
                            while (currentFuel > 0)
                            {
                                total += JumpRange( Math.Min( currentFuel, maxfuelperjump ), cargoTonnage );
                                jumps++;
                                currentFuel -= Math.Min( currentFuel, maxfuelperjump );
                            }
                            return new JumpDetail(total, jumps);
                        }
                    case "full":
                        {
                            currentFuel = fueltanktotalcapacity ?? 0;
                            double total = 0;
                            int jumps = 0;
                            while ( currentFuel > 0)
                            {
                                total += JumpRange( Math.Min( currentFuel, maxfuelperjump ), cargoTonnage );
                                jumps++;
                                currentFuel -= Math.Min( currentFuel, maxfuelperjump );
                            }
                            return new JumpDetail(total, jumps);
                        }
                }
            }
            return null;
        }

        private double JumpRange (double currentFuel, int carriedCargo, double boostModifier = 1)
        {
            if ( frameshiftdrive is null || unladenmass == 0 || optimalmass == 0 ) { return 0; }
            var mass = unladenmass + currentFuel + activeFuelReservoirCapacity + carriedCargo;
            var fuel = Math.Min(currentFuel, maxfuelperjump);
            
            // Calculate our base max range
            var baseMaxRange = (optimalmass / mass) * Math.Pow( fuel * 1000 / frameshiftdrive.GetFsdRatingConstant(), ( 1 / frameshiftdrive.GetFsdPowerConstant() ) );
            if ( baseMaxRange == 0 ) { return 0; }

            // Return the maximum range with the specified fuel and cargo levels, with a boost modifier if using synthesis or a jet cone boost
            var guardianFsdBoosterRange = compartments.FirstOrDefault(c => c.module.edname.Contains("Int_GuardianFSDBooster"))?.module?.GetGuardianFSDBoost() ?? 0;
            var boostFactor = Math.Pow( baseMaxRange / ( baseMaxRange + guardianFsdBoosterRange), frameshiftdrive.GetFsdPowerConstant());

            return Math.Pow( ( fuel / (boostFactor * frameshiftdrive.GetFsdRatingConstant() / 1000 ) ), (1 / frameshiftdrive.GetFsdPowerConstant() ) ) * boostFactor * optimalmass / mass;
            //return maxRange * boostFactor / boostModifier;
        }

        public static Ship FromShipyardInfo(ShipyardInfoItem item)
        {
            try
            {
                Logging.Debug($"Converting ShipyardInfoItem to Ship: ", item);
                var ship = ShipDefinitions.FromEDModel(item.edModel, false);
                if (ship == null)
                {
                    // Unknown ship; report the full object so that we can update the definitions 
                    Logging.Info("Ship definition error: " + item.edModel);

                    // Create a basic ship definition & supplement from the info available 
                    ship = new Ship
                    {
                        EDName = item.edModel
                    };
                }
                ship.value = item.shipPrice;
                return ship;
            }
            catch (Exception ex)
            {
                Logging.Error($"Failed to parse ShipyardInfoItem.", ex);
                return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
