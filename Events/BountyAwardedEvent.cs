using EddiDataDefinitions;
using System;
using System.Collections.Generic;
using Utilities;

namespace EddiEvents
{
    [PublicAPI]
    public class BountyAwardedEvent : Event
    {
        public const string NAME = "Bounty awarded";
        public const string DESCRIPTION = "Triggered when you are awarded a bounty";
        public const string SAMPLE = @"{ ""timestamp"":""2023-10-20T22:46:00Z"", ""event"":""Bounty"", ""Rewards"":[ { ""Faction"":""Amarishvaru Advanced Holdings"", ""Reward"":527518 } ], ""PilotName"":""$npc_name_decorate:#name=Christopher;"", ""PilotName_Localised"":""Christopher"", ""Target"":""anaconda"", ""TotalReward"":527518, ""VictimFaction"":""Orom Blue Council"" }";

        [PublicAPI("The name of the asset you destroyed (if applicable)")]
        public string target { get; private set; }

        [PublicAPI( "The pilot of the asset you destroyed (if applicable)" )]
        public string pilot { get; private set; }

        [PublicAPI("The name of the faction whose asset you destroyed")]
        public string faction { get; private set; }

        [PublicAPI("The total number of credits obtained for destroying the asset")]
        public long reward { get; private set; }

        [PublicAPI("The rewards obtained for destroying the asset")]
        public List<Reward> rewards { get; private set; }

        [PublicAPI("True if the rewards have been shared with wing-mates")]
        public bool shared { get; private set; }

        public BountyAwardedEvent(DateTime timestamp, string target, string target_localised, string victimName, string victimFaction, long reward, List<Reward> rewards, bool shared) : base(timestamp, NAME)
        {
            if ( target != null )
            {
                // Might be a ship
                var targetShip = ShipDefinitions.FromEDModel(target, false);

                // Might be a SRV or Fighter
                var targetVehicle = VehicleDefinition.EDNameExists(target) ? VehicleDefinition.FromEDName(target) : null;

                // Might be an on foot commander
                var targetCmdrSuit = Suit.EDNameExists(target) ? Suit.FromEDName(target) : null;

                // Might be an on foot NPC
                var targetNpcSuitLoadout = NpcSuitLoadout.EDNameExists(target) ? NpcSuitLoadout.FromEDName(target) : null;

                target = targetShip?.model
                         ?? targetCmdrSuit?.localizedName
                         ?? targetVehicle?.localizedName
                         ?? targetNpcSuitLoadout?.localizedName
                         ?? target_localised;
            }

            this.target = target;
            this.pilot = victimName;
            this.faction = victimFaction;
            this.reward = reward;
            this.rewards = rewards;
            this.shared = shared;
        }
    }
}
