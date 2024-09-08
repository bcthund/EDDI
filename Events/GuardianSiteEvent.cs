using System;
using Utilities;
using Utilities.RegionMap;
using EddiDataDefinitions;
using System.Collections.Generic;

namespace EddiEvents
{
    [PublicAPI]
    public class GuardianSiteEvent : Event
    {
        public const string NAME = "Guardian Site Present";
        public const string DESCRIPTION = "Triggered when you enter a system with a known guardian site.";
        public const string SAMPLE = "{ \"timestamp\":\"2024-09-08T04:01:06Z\", \"event\":\"FSDJump\", \"Taxi\":false, \"Multicrew\":false, \"StarSystem\":\"Col 173 Sector JS-J d9-76\", \"SystemAddress\":2622153001331, \"StarPos\":[1543.28125,-181.62500,86.31250], \"SystemAllegiance\":\"\", \"SystemEconomy\":\"$economy_None;\", \"SystemEconomy_Localised\":\"None\", \"SystemSecondEconomy\":\"$economy_None;\", \"SystemSecondEconomy_Localised\":\"None\", \"SystemGovernment\":\"$government_None;\", \"SystemGovernment_Localised\":\"None\", \"SystemSecurity\":\"$GAlAXY_MAP_INFO_state_anarchy;\", \"SystemSecurity_Localised\":\"Anarchy\", \"Population\":0, \"Body\":\"Col 173 Sector JS-J d9-76\", \"BodyID\":0, \"BodyType\":\"Star\", \"JumpDist\":5.948, \"FuelUsed\":0.019239, \"FuelLevel\":31.961519 }";

        [PublicAPI("The list of guardian sites in the system.")]
        public List<GuardianSite> sites { get; private set; }

        //[PublicAPI("How many sites are in this system.")]
        //public int? count => guardianSites.Count;

        public GuardianSiteEvent (DateTime timestamp, List<GuardianSite> guardianSites) : base(timestamp, NAME)
        {
            this.sites = guardianSites;
        }
    }
}