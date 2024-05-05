using EddiDataDefinitions;

namespace EddiSpeechService
{
    public static partial class Translations
    {
        public static string getPhoneticShipModel(string val)
        {
            Ship ship = ShipDefinitions.FromModel(val);
            if (ship != null && ship.EDID > 0)
            {
                return ship.SpokenModel().Trim();
            }
            return val;
        }
    }
}
