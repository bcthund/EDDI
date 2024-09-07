using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Utilities;


namespace EddiDataDefinitions
{
    public class GuardianSite /*: ResourceBasedLocalizedEDName<Nebula>*/
    {
        static GuardianSite () { }

        public enum GuardianSiteType {
            None = 0,
            Beacon = 1,
            Ruin = 2,
            Structure = 3
        }

        public enum BlueprintType {
            None = 0,
            Weapon = 1,
            Vessel = 2,
            Module = 3
        }

        [PublicAPI]
        public string localizedType => Properties.GuardianSiteType.ResourceManager.GetString(type.ToString());

        [PublicAPI]
        public string localizedBlueprint => Properties.GuardianSiteBlueprint.ResourceManager.GetString(blueprintType.ToString());

        public ulong? systemAddress { get; set; }
        public GuardianSiteType type { get; set; }
        public string systemName { get; set; }
        public string body { get; set; }
        public decimal? x;                   // x coordinate of system
        public decimal? y;                   // y coordinate of system
        public decimal? z;                   // z coordinate of system
        public BlueprintType blueprintType { get; set; }

        // Calucated distance from target system ( Gets set and returned with TryGetNearestNebula )
        [PublicAPI("The calculated distance to the site from the current system.")]
        public decimal? distance { get; set; }

        // dummy used to ensure that the static constructor has run
        public GuardianSite ()
        { }

        internal GuardianSite ( ulong? systemAddress,
                               GuardianSiteType type,
                               string systemName,
                               string body,
                               decimal? x,
                               decimal? y,
                               decimal? z,
                               BlueprintType blueprintType )
        {
            this.systemAddress = systemAddress;
            this.type = type;
            this.systemName = systemName;
            this.body = body;
            this.x = x;
            this.y = y;
            this.z = z;
            this.blueprintType = blueprintType;
        }

        public static GuardianSite TryGetNearestGuardianSite ( decimal? systemX, decimal? systemY, decimal? systemZ )
        {
            List<GuardianSite> listGuardianSites = new List<GuardianSite>();

            // Get the distance (squared) of all Nebula
            foreach ( var guardianSite in GuardianSiteDefinitions.AllOfThem )
            {
                if ( guardianSite.x != null && guardianSite.y != null && guardianSite.z != null )
                {
                    // We don't need the exact distance, use the faster method for sorting purposes
                    guardianSite.distance = Functions.StellarDistanceSquare( systemX, systemY, systemZ, guardianSite.x, guardianSite.y, guardianSite.z );
                    listGuardianSites.Add( guardianSite );
                }
            }

            GuardianSite closest = listGuardianSites.OrderBy( s => s.distance).First();
            closest.distance = Functions.StellarDistanceLy( closest.distance );

            return closest;
        }

        public static List<GuardianSite> TryGetNearestGuardianSites ( decimal? systemX, decimal? systemY, decimal? systemZ, int maxCount=50, int maxDistance=10000 )
        {
            List<GuardianSite> listGuardianSites = new List<GuardianSite>();

            // Get the distance (squared) of all Nebula
            foreach ( var guardianSite in GuardianSiteDefinitions.AllOfThem )
            {
                if ( guardianSite.x != null && guardianSite.y != null && guardianSite.z != null )
                {
                    // We don't need the exact distance, use the faster method for sorting purposes
                    guardianSite.distance = Functions.StellarDistanceSquare( systemX, systemY, systemZ, guardianSite.x, guardianSite.y, guardianSite.z );
                    listGuardianSites.Add( guardianSite );
                }
            }

            var maxDistanceSquared = maxDistance*maxDistance;
            List<GuardianSite> closestList = listGuardianSites.Where( s => s.distance <= maxDistanceSquared ).OrderBy( s => s.distance).Take(maxCount).ToList();
            for(int i = 0; i< closestList.Count; i++) {
                closestList[i].distance = Functions.StellarDistanceLy( closestList[i].distance );
            }

            return closestList;
        }

        public static GuardianSite TryGetNearestGuardianSite ( GuardianSiteType typeFilter, decimal? systemX, decimal? systemY, decimal? systemZ )
        {
            List<GuardianSite> listGuardianSites = new List<GuardianSite>();

            // Get the distance (squared) of all Nebula
            foreach ( var guardianSite in GuardianSiteDefinitions.AllOfThem.Where( x=> x.type == typeFilter ) )
            {
                if ( guardianSite.x != null && guardianSite.y != null && guardianSite.z != null )
                {
                    // We don't need the exact distance, use the faster method for sorting purposes
                    guardianSite.distance = Functions.StellarDistanceSquare( systemX, systemY, systemZ, guardianSite.x, guardianSite.y, guardianSite.z );
                    listGuardianSites.Add( guardianSite );
                }
            }

            GuardianSite closest = listGuardianSites.OrderBy( s => s.distance).First();
            closest.distance = Functions.StellarDistanceLy( closest.distance );

            return closest;
        }

        public static List<GuardianSite> TryGetNearestGuardianSites ( GuardianSiteType typeFilter, decimal? systemX, decimal? systemY, decimal? systemZ, int maxCount=50, int maxDistance=10000 )
        {
            List<GuardianSite> listGuardianSites = new List<GuardianSite>();

            // Get the distance (squared) of all Nebula
            foreach ( var guardianSite in GuardianSiteDefinitions.AllOfThem.Where( x=> x.type == typeFilter ) )
            {
                if ( guardianSite.x != null && guardianSite.y != null && guardianSite.z != null )
                {
                    // We don't need the exact distance, use the faster method for sorting purposes
                    guardianSite.distance = Functions.StellarDistanceSquare( systemX, systemY, systemZ, guardianSite.x, guardianSite.y, guardianSite.z );
                    listGuardianSites.Add( guardianSite );
                }
            }

            var maxDistanceSquared = maxDistance*maxDistance;
            List<GuardianSite> closestList = listGuardianSites.Where( s => s.distance <= maxDistanceSquared ).OrderBy( s => s.distance).Take(maxCount).ToList();
            //foreach( var guardianSite in closestList ) {
            //    guardianSite.distance = Functions.StellarDistanceLy( guardianSite.distance );
            //}
            for(int i = 0; i< closestList.Count; i++) {
                closestList[i].distance = Functions.StellarDistanceLy( closestList[i].distance );
            }

            return closestList;
        }

        public static GuardianSite TryGetNearestGuardianSite ( BlueprintType typeFilter, decimal? systemX, decimal? systemY, decimal? systemZ )
        {
            List<GuardianSite> listGuardianSites = new List<GuardianSite>();

            // Get the distance (squared) of all Nebula
            foreach ( var guardianSite in GuardianSiteDefinitions.AllOfThem.Where( x=> x.blueprintType == typeFilter ) )
            {
                if ( guardianSite.x != null && guardianSite.y != null && guardianSite.z != null )
                {
                    // We don't need the exact distance, use the faster method for sorting purposes
                    guardianSite.distance = Functions.StellarDistanceSquare( systemX, systemY, systemZ, guardianSite.x, guardianSite.y, guardianSite.z );
                    listGuardianSites.Add( guardianSite );
                }
            }

            GuardianSite closest = listGuardianSites.OrderBy( s => s.distance).First();
            closest.distance = Functions.StellarDistanceLy( closest.distance );

            return closest;
        }

        public static List<GuardianSite> TryGetNearestGuardianSites ( BlueprintType typeFilter, decimal? systemX, decimal? systemY, decimal? systemZ, int maxCount=50, int maxDistance=10000 )
        {
            List<GuardianSite> listGuardianSites = new List<GuardianSite>();

            // Get the distance (squared) of all Nebula
            foreach ( var guardianSite in GuardianSiteDefinitions.AllOfThem.Where( x=> x.blueprintType == typeFilter ) )
            {
                if ( guardianSite.x != null && guardianSite.y != null && guardianSite.z != null )
                {
                    // We don't need the exact distance, use the faster method for sorting purposes
                    guardianSite.distance = Functions.StellarDistanceSquare( systemX, systemY, systemZ, guardianSite.x, guardianSite.y, guardianSite.z );
                    listGuardianSites.Add( guardianSite );
                }
            }

            var maxDistanceSquared = maxDistance*maxDistance;
            List<GuardianSite> closestList = listGuardianSites.Where( s => s.distance <= maxDistanceSquared ).OrderBy( s => s.distance).Take(maxCount).ToList();
            for(int i = 0; i< closestList.Count; i++) {
                closestList[i].distance = Functions.StellarDistanceLy( closestList[i].distance );
            }

            return closestList;
        }

        public static GuardianSite TryGetNearestNebula ( StarSystem starsystem )
        {
            return TryGetNearestGuardianSite( starsystem.x, starsystem.y, starsystem.z );
        }
    }
}
