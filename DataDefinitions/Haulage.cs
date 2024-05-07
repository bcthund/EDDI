using Newtonsoft.Json;
using System;
using System.Linq;
using Utilities;

namespace EddiDataDefinitions
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Haulage
    {
        [PublicAPI]
        public long missionid { get; set; }

        [PublicAPI]
        public string name { get; set; }

        public string typeEDName { get; set; }

        [PublicAPI, JsonIgnore, Obsolete("Please use localizedName or invariantName")]
        public string type => MissionType.FromEDName(typeEDName)?.localizedName;

        [PublicAPI]
        public string status { get; set; }

        [PublicAPI]
        public string originsystem { get; set; }

        [PublicAPI]
        public string sourcesystem { get; set; }

        [PublicAPI]
        public string sourcebody { get; set; }

        [PublicAPI, JsonIgnore] // False if illegal mission items are required
        public bool legal => !name.ToLowerInvariant().Contains("illegal");

        [JsonIgnore]
        public bool wing => name.ToLowerInvariant().Contains("wing");

        [PublicAPI] // The total quantity of mission items which need to be delivered for the mission
        public int amount { get; set; }

        public int remaining { get; set; }

        [PublicAPI] // The quantity of mission items which still need to be delivered
        public int need { get; set; }

        public long startmarketid { get; set; }

        public long endmarketid { get; set; }

        [PublicAPI] // The quantity of mission items which has been collected (at a cargo depot)
        public int collected { get; set; }

        [PublicAPI] // The quantity of mission items which has been delivered (at a cargo depot)
        public int delivered { get; set; }

        [PublicAPI]
        public bool shared { get; set; }


        public Haulage (long MissionId, string Name, string OriginSystem, int Amount, bool Shared = false)
        {
            missionid = MissionId;
            name = Name;
            originsystem = OriginSystem;
            status = "Active";
            amount = Amount;
            remaining = Amount;
            need = Amount;
            shared = Shared;

            // Mechanism for identifying chained delivery and 'welcome' missions
            typeEDName = Name.Split('_').ElementAtOrDefault(1)?.ToLowerInvariant();
            if ( !string.IsNullOrEmpty(typeEDName) )
            {
                switch ( typeEDName )
                {
                    case "clearingthepath":
                    case "helpfinishtheorder":
                        {
                            typeEDName = "delivery";
                            break;
                        }
                    case "rescuefromthetwins":
                    case "rescuethewares":
                        {
                            typeEDName = "salvage";
                            break;
                        }
                    case "ds":
                    case "rs":
                    case "welcome":
                        {
                            typeEDName = Name.Split( '_' ).ElementAt( 2 ).ToLowerInvariant();
                            break;
                        }
                }
            }
        }
    }
}
