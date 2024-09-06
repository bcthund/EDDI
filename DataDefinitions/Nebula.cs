using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace EddiDataDefinitions
{
    public class Nebula
    {
        static Nebula ()
        { }

        public enum FilterVisited {
            NotVisited = 0,
            Visited = 1,
            All = 2
        }

        public enum NebulaType {
            Standard = 0,
            Real = 1,
            Planetary = 2
        }

        public int id { get; set; }
        public NebulaType type { get; set; }
        public string designation;

        [PublicAPI("The name of the nebula")]
        public string name { get; set; }
        public int region;
        public bool hasCentralBody;
        public string referenceBody { get; set; }
        //public string referenceSector;    // TODO:Future - Add nebula sectors for non-central body nebulae
        public decimal? x;                   // x coordinate of system
        public decimal? y;                   // y coordinate of system
        public decimal? z;                   // z coordinate of system
        public int? diameter { get; set; }   // Approximate diameter of nebula in Ly

        // Calucated distance from target system ( Gets set and returned with TryGetNearestNebula )
        [PublicAPI("The calculated distance to the central point of the nebula from the current system.")]
        public decimal? distance { get; set; }
        public bool visited { get; set; }

        // dummy used to ensure that the static constructor has run
        public Nebula ()
        { }

        internal Nebula ( int id,
                         //string designation,
                         NebulaType type,
                         string name,
                         int region,
                         bool hasCentralBody,
                         string referenceBody,
                         //string referenceSector,      // TODO:Future - Add nebula sectors for non-central body nebulas
                         decimal? x,
                         decimal? y,
                         decimal? z,
                         int? diameter,
                         bool visited=false)
        {
            this.id = id;
            this.type = type;
            //this.designation = designation;
            this.name = name;
            this.region = region;
            this.hasCentralBody = hasCentralBody;
            this.referenceBody = referenceBody;
            //this.referenceSector = referenceSector;   // TODO:Future - Add nebula sectors for non-central body nebulas
            this.x = x;
            this.y = y;
            this.z = z;
            this.diameter = diameter;
            this.distance = 0;
            this.visited = visited;
        }

        public static Nebula TryGetNearestNebula ( string systemname, decimal? systemX, decimal? systemY, decimal? systemZ )
        {
            List<Nebula> listNebula = new List<Nebula>();

            // TODO: 2212 - Future - Add nebula sectors for non-central body nebulas
            //   - Check if system name is in the nebula sector list
            //   - systemname.Contains(nebula.referenceSector)
            //   - This also needs to be added to the other overloads/alternative methods below

            //foreach( var nebula in AllOfThem.Where( x => x.hasCentralBody==false).ToList() )
            //{
            //    if( systemname == nebula.referenceSector )
            //    {
            //        listNebula.Add( nebula );
            //    }
            //}

            // If not in sector then check nebula list
            if(listNebula.Count == 0 ) {

                // Get the distance (squared) of all Nebula
                foreach( var nebula in NebulaDefinitions.AllOfThem.Where( x => x.hasCentralBody==true ) )
                {
                    if( nebula.x != null && nebula.y != null && nebula.z != null )
                    {
                        // We don't need the exact distance, use the faster method for sorting purposes
                        nebula.distance = Functions.StellarDistanceSquare(systemX, systemY, systemZ, nebula.x, nebula.y, nebula.z);
                        //nebula.distance = Functions.StellarDistanceLy(systemX, systemY, systemZ, nebula.x, nebula.y, nebula.z);
                        listNebula.Add(nebula);
                    }
                }
            }

            Nebula closest = listNebula.OrderBy( s => s.distance).First();
            closest.distance = Functions.StellarDistanceLy( closest.distance );
            
            return closest;
        }

        public static Nebula TryGetNearestNebula ( decimal? systemX, decimal? systemY, decimal? systemZ )
        {
            List<Nebula> listNebula = new List<Nebula>();

            // If not in sector then check nebula list
            if(listNebula.Count == 0 ) {

                // Get the distance (squared) of all Nebula
                foreach( var nebula in NebulaDefinitions.AllOfThem.Where( x => x.hasCentralBody==true ) )
                {
                    if( nebula.x != null && nebula.y != null && nebula.z != null )
                    {
                        // We don't need the exact distance, use the faster method for sorting purposes
                        nebula.distance = Functions.StellarDistanceSquare(systemX, systemY, systemZ, nebula.x, nebula.y, nebula.z);
                        listNebula.Add(nebula);
                    }
                }
            }

            Nebula closest = listNebula.OrderBy( s => s.distance).First();
            closest.distance = Functions.StellarDistanceLy( closest.distance );
            
            return closest;
        }

        public static List<Nebula> TryGetNearestNebulae ( decimal? systemX, decimal? systemY, decimal? systemZ, int maxCount=50, int maxDistance=10000, Nebula.FilterVisited filterVisited=Nebula.FilterVisited.All )
        {
            List<Nebula> listNebula = new List<Nebula>();

            // If not in sector then check nebula list
            if(listNebula.Count == 0 ) {

                if (filterVisited == Nebula.FilterVisited.All) {
                    // Get the distance (squared) of all Nebula
                    foreach( var nebula in NebulaDefinitions.AllOfThem.Where( x => x.hasCentralBody==true ) )
                    {
                        CalcNebulaDistance( systemX, systemY, systemZ, nebula, ref listNebula);
                    }
                }
                else if (filterVisited == Nebula.FilterVisited.Visited) {
                    // Get the distance (squared) of all Nebula
                    foreach( var nebula in NebulaDefinitions.AllOfThem.Where( x => x.hasCentralBody==true && x.visited==true ) )
                    {
                        CalcNebulaDistance( systemX, systemY, systemZ, nebula, ref listNebula);
                    }
                }
                else if (filterVisited == Nebula.FilterVisited.NotVisited) {
                    // Get the distance (squared) of all Nebula
                    foreach( var nebula in NebulaDefinitions.AllOfThem.Where( x => x.hasCentralBody==true && x.visited==false ) )
                    {
                        CalcNebulaDistance( systemX, systemY, systemZ, nebula, ref listNebula);
                    }
                }
            }

            var maxDistanceSquared = maxDistance*maxDistance;
            List<Nebula> closestList = listNebula.Where( s => s.distance <= maxDistanceSquared ).OrderBy( s => s.distance).Take(maxCount).ToList();
            for(int i = 0; i< closestList.Count; i++) {
                closestList[i].distance = Functions.StellarDistanceLy( closestList[i].distance );
            }
            
            return closestList;
        }

        private static void CalcNebulaDistance(decimal? systemX, decimal? systemY, decimal? systemZ, Nebula nebula, ref List<Nebula> listNebula) {
            if( nebula.x != null && nebula.y != null && nebula.z != null )
            {
                // We don't need the exact distance, use the faster method for sorting purposes
                nebula.distance = Functions.StellarDistanceSquare(systemX, systemY, systemZ, nebula.x, nebula.y, nebula.z);
                listNebula.Add(nebula);
            }
        }

        public static List<Nebula> TryGetNearestNebulae ( Nebula.NebulaType type, decimal? systemX, decimal? systemY, decimal? systemZ, int maxCount=50, int maxDistance=10000, Nebula.FilterVisited filterVisited=Nebula.FilterVisited.All )
        {
            List<Nebula> listNebula = new List<Nebula>();

            // If not in sector then check nebula list
            if(listNebula.Count == 0 ) {
                if (filterVisited == Nebula.FilterVisited.All) {
                    // Get the distance (squared) of all Nebula
                    foreach( var nebula in NebulaDefinitions.AllOfThem.Where( x => x.hasCentralBody==true && x.type==type ) )
                    {
                        CalcNebulaDistance( systemX, systemY, systemZ, nebula, ref listNebula);
                    }
                }
                else if (filterVisited == Nebula.FilterVisited.Visited) {
                    // Get the distance (squared) of all Nebula
                    foreach( var nebula in NebulaDefinitions.AllOfThem.Where( x => x.hasCentralBody==true && x.type==type && x.visited==true ) )
                    {
                        CalcNebulaDistance( systemX, systemY, systemZ, nebula, ref listNebula);
                    }
                }
                else if (filterVisited == Nebula.FilterVisited.NotVisited) {
                    // Get the distance (squared) of all Nebula
                    foreach( var nebula in NebulaDefinitions.AllOfThem.Where( x => x.hasCentralBody==true && x.type==type && x.visited==false ) )
                    {
                        CalcNebulaDistance( systemX, systemY, systemZ, nebula, ref listNebula);
                    }
                }
            }

            var maxDistanceSquared = maxDistance*maxDistance;
            List<Nebula> closestList = listNebula.Where( s => s.distance <= maxDistanceSquared ).OrderBy( s => s.distance).Take(maxCount).ToList();
            for(int i = 0; i< closestList.Count; i++) {
                closestList[i].distance = Functions.StellarDistanceLy( closestList[i].distance );
            }
            
            return closestList;
        }

        public static Nebula TryGetNearestNebula ( StarSystem starsystem )
        {
            return TryGetNearestNebula( starsystem.systemname, starsystem.x, starsystem.y, starsystem.z );
        }

    }
}
