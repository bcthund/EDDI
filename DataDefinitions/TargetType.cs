namespace EddiDataDefinitions
{
    /// <summary>
    /// Target types
    /// </summary>
    public class TargetType : ResourceBasedLocalizedEDName<TargetType>
    {
        static TargetType()
        {
            resourceManager = Properties.TargetType.ResourceManager;
            resourceManager.IgnoreCase = true;
            missingEDNameHandler = (edname) => new TargetType(edname);
        }

        public static readonly TargetType AIHumanoid = new TargetType("AIHumanoid");
        public static readonly TargetType BountyHunter = new TargetType ("BountyHunter");
        public static readonly TargetType Civilian = new TargetType("Civilian");
        public static readonly TargetType CitizenHumanoid = new TargetType("CitizenHumanoid");
        public static readonly TargetType Deserter = new TargetType("Deserter");
        public static readonly TargetType DeserterASS = new TargetType("DeserterASS");
        public static readonly TargetType GuardHumanoid = new TargetType("GuardHumanoid");
        public static readonly TargetType Hostage = new TargetType("Hostage");
        public static readonly TargetType Miner = new TargetType("Miner");
        public static readonly TargetType Pirate = new TargetType("Pirate");
        public static readonly TargetType PirateLord = new TargetType("PirateLord");
        public static readonly TargetType Politician = new TargetType("Politician");
        public static readonly TargetType Security = new TargetType("Security");
        public static readonly TargetType Scout = new TargetType("Scout");
        public static readonly TargetType Smuggler = new TargetType("Smuggler");
        public static readonly TargetType Terrorist = new TargetType("Terrorist");
        public static readonly TargetType TerroristLeader = new TargetType("TerroristLeader");
        public static readonly TargetType Trader = new TargetType("Trader");
        public static readonly TargetType VenerableGeneral = new TargetType("VenerableGeneral");

        // dummy used to ensure that the static constructor has run
        public TargetType () : this("")
        { }

        private TargetType(string edname) : base(edname, edname)
        { }
    }
}
