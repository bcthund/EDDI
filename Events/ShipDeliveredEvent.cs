using EddiDataDefinitions;
using System;
using Utilities;

namespace EddiEvents
{
    [PublicAPI]
    public class ShipDeliveredEvent : Event
    {
        public const string NAME = "Ship delivered";
        public const string DESCRIPTION = "Triggered when your newly-purchased ship is delivered to you";
        public const string SAMPLE = "{ \"timestamp\":\"2018-02-04T00:06:45Z\", \"event\":\"ShipyardNew\", \"ShipType\":\"typex\", \"ShipType_Localised\":\"Alliance Chieftain\", \"NewShipID\":70 }";

        [PublicAPI("The ID of the ship that was delivered")]
        public int? shipid { get; private set; }

        [PublicAPI("The ship that was delivered")]
        public string ship => shipDefinition?.model;

        // Not intended to be user facing

        public Ship shipDefinition => ShipDefinitions.FromEDModel(edModel);

        public string edModel { get; private set; }

        public ShipDeliveredEvent(DateTime timestamp, string ship, int? shipId) : base(timestamp, NAME)
        {
            this.edModel = ship;
            this.shipid = shipId;
        }
    }
}
