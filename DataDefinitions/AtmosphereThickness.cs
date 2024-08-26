using Newtonsoft.Json;
using Utilities;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Diagnostics.Tracing;

namespace EddiDataDefinitions
{
    /// <summary> Atmosphere Class </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class AtmosphereThickness : ResourceBasedLocalizedEDName<AtmosphereThickness>
    {
        static AtmosphereThickness()
        {
            resourceManager = Properties.AtmosphereThickness.ResourceManager;
            resourceManager.IgnoreCase = true;
            missingEDNameHandler = (edname) => new AtmosphereThickness(edname);

            None = new AtmosphereThickness("none");
            Normal = new AtmosphereThickness("normal");
            Thin = new AtmosphereThickness("thin");
            Thick = new AtmosphereThickness("thick");
            HotThick = new AtmosphereThickness("hot_thick");
        }

        public static readonly AtmosphereThickness None;
        public static readonly AtmosphereThickness Normal;
        public static readonly AtmosphereThickness Thin;
        public static readonly AtmosphereThickness Thick;
        public static readonly AtmosphereThickness HotThick;

        // dummy used to ensure that the static constructor has run
        public AtmosphereThickness () : this("")
        { }

        private AtmosphereThickness(string edname) : base(edname, edname
            .ToLowerInvariant()
            .Replace(" ", "_")
            .Replace("-", ""))
        { }

        new public static AtmosphereThickness FromName(string name)
        {
            if (name == null)
            {
                return FromName("none");
            }

            string normalizedName = name
            .ToLowerInvariant();
            return ResourceBasedLocalizedEDName<AtmosphereThickness>.FromName(normalizedName);
        }

        new public static AtmosphereThickness FromEDName(string edname)
        {
            if (edname == null)
            {
                return FromEDName("none");
            }

            string normalizedEDName = NormalizeAtmosphereThickness(edname);
            return ResourceBasedLocalizedEDName<AtmosphereThickness>.FromEDName(normalizedEDName);
        }

        public static string NormalizeAtmosphereThickness ( string edname )
        {
            string normalizedThickness = edname.ToLowerInvariant()
            .Replace(" ", "_")
            .Replace("-", "");

            // TODO: 2212: Gas Giants?

            int pos;
            if (normalizedThickness=="" || normalizedThickness=="none" || normalizedThickness=="None") {
                return "none";
            }
            else if (normalizedThickness.Contains("hot_thick")) {
                pos = normalizedThickness.IndexOf("hot_thick")+9;
            }
            else if (normalizedThickness.Contains("thick")) {
                pos = normalizedThickness.IndexOf("thick")+5;
            }
            else if (normalizedThickness.Contains("thin")) {
                pos = normalizedThickness.IndexOf("thin")+4;
            }
            else return "normal";

            //Logging.Debug($"{edname}, {normalizedThickness}, {pos}, {normalizedThickness.Length}");

            if (pos < normalizedThickness.Length) {
                return normalizedThickness?.Remove(pos) ?? "None";
            }

            return normalizedThickness ?? "None";
        }
    }
}
