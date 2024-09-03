using Newtonsoft.Json;
using Utilities;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Diagnostics.Tracing;

namespace EddiDataDefinitions
{
    /// <summary> Atmosphere Thickness </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class AtmosphereThickness : ResourceBasedLocalizedEDName<AtmosphereThickness>
    {
        static AtmosphereThickness()
        {
            resourceManager = Properties.AtmosphereThickness.ResourceManager;
            resourceManager.IgnoreCase = false;
            missingEDNameHandler = (edname) => new AtmosphereThickness(edname);

            None = new AtmosphereThickness("None");
            Normal = new AtmosphereThickness("Normal");
            Thin = new AtmosphereThickness("Thin");
            Thick = new AtmosphereThickness("Thick");
            HotThick = new AtmosphereThickness("HotThick");
            GasGiant = new AtmosphereThickness("GasGiant");
        }

        public static readonly AtmosphereThickness None;
        public static readonly AtmosphereThickness Normal;
        public static readonly AtmosphereThickness Thin;
        public static readonly AtmosphereThickness Thick;
        public static readonly AtmosphereThickness HotThick;
        public static readonly AtmosphereThickness GasGiant;

        // dummy used to ensure that the static constructor has run
        public AtmosphereThickness () : this("")
        { }

        private AtmosphereThickness(string edname) : base(edname, NormalizeAtmosphereThickness(edname))
        { }

        new public static AtmosphereThickness FromName( string name )
        {
            string normalizedName;
            if ( name == null || name == "" )
            {
                normalizedName = "None";
            }
            else {
                normalizedName = NormalizeAtmosphereThickness(name);
            }

            //Logging.Debug($"===============> [Atmosphere Thickness:FromName] name='{name}', normalized='{normalizedName}', resource='{ResourceBasedLocalizedEDName<AtmosphereThickness>.FromName( normalizedName ).edname}'");

            return ResourceBasedLocalizedEDName<AtmosphereThickness>.FromName( normalizedName );
        }

        new public static AtmosphereThickness FromEDName( string edname )
        {
            string normalizedEDName;
            if ( edname == null || edname == "" )
            {
                normalizedEDName = "None";
            }
            else {
                normalizedEDName = NormalizeAtmosphereThickness(edname);
            }

            //Logging.Debug($"===============> [Atmosphere Thickness:FromEDName] name='{edname}', normalized='{normalizedEDName}', resource='{ResourceBasedLocalizedEDName<AtmosphereThickness>.FromName( normalizedEDName ).edname}'");

            return ResourceBasedLocalizedEDName<AtmosphereThickness>.FromEDName( normalizedEDName );
        }

        public static string NormalizeAtmosphereThickness( string edname )
        {
            string returnVal;
            string normalizedThickness = edname.ToLowerInvariant()
            .Replace(" ", "_")
            .Replace("-", "");

            if (normalizedThickness.Contains("no_atmosphere") || normalizedThickness=="" || normalizedThickness=="none" ) {
                returnVal = "None";
            }
            else if (normalizedThickness.Contains("hot_thick") || normalizedThickness.Contains("hotthick")) {
                returnVal = "HotThick";
            }
            else if (normalizedThickness.Contains("thick")) {
                returnVal = "Thick";
            }
            else if (normalizedThickness.Contains("thin")) {
                returnVal = "Thin";
            }
            else if (normalizedThickness.Contains("gas_giant") || normalizedThickness.Contains("gasgiant")) {
                returnVal = "GasGiant";
            }
            else returnVal = "Normal";

            //Logging.Debug($"====================> [Atmosphere Thickness:Normalize] edname='{edname}' normalized='{normalizedThickness}', return='{returnVal}'");

            return returnVal;
        }
    }
}
