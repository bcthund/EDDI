using JetBrains.Annotations;
using Newtonsoft.Json;

namespace EddiDataDefinitions
{
    public class MicroResourceAmount
    {
        [Utilities.PublicAPI( "The localized name of the micro resource" )]
        public string name => microResource.localizedName;

        [Utilities.PublicAPI( "The invariant name of the micro resource" )]
        public string invariantName => microResource.invariantName;

        [Utilities.PublicAPI( "The localized category of the micro resource" )]
        public string category => Category?.localizedName;

        [Utilities.PublicAPI( "The invariant category of the micro resource" )]
        public string invariantCategory => Category?.invariantName;

        [Utilities.PublicAPI( "The amount of the micro resource" )]
        public int amount { get; }

        public int? price { get; }

        // Not intended to be user facing
        public string edname => microResource.edname;
        public MicroResource microResource { get; }
        public MicroResourceCategory Category => microResource?.Category;
        public long? ownerId { get; }
        public long? missionId { get; }

        [JsonConstructor]
        public MicroResourceAmount([JsonProperty] string Name, [JsonProperty] long? OwnerID, [JsonProperty] int Count, [JsonProperty] string Type = null, [JsonProperty] string Name_Localised = null, [JsonProperty] long? MissionID = null)
        {
            this.microResource = MicroResource.FromEDName(Name, Name_Localised, Type);
            this.ownerId = OwnerID;
            this.amount = Count;
            this.missionId = MissionID;
        }

        public MicroResourceAmount ( [NotNull] MicroResource microResource, int amount, int? price = null, long? ownerId = null, long? missionId = null )
        {
            this.microResource = microResource;
            this.amount = amount;
            this.price = price;
            this.ownerId = ownerId;
            this.missionId = missionId;
        }
    }
}
