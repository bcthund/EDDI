using EddiDataDefinitions;

namespace EddiSpeechService
{
    public static partial class Translations
    {
        public static string getPhoneticShipModel(string val)
        {
            var ship = ShipDefinitions.FromModel( val );
            if ( ship != null && !string.IsNullOrEmpty( ship.model ) )
            {
                return ship.SpokenModel().Trim();
            }

            return val;
        }
    }
}
