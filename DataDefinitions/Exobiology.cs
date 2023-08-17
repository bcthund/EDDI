﻿using Utilities;

namespace EddiDataDefinitions
{
    public class Exobiology : OrganicItem
    {
        public enum Status
        {
            InsideSampleRange = 0,
            OutsideSampleRange = 1
        }

        public class Coordinates
        {
            public decimal? latitude;
            public decimal? longitude;
            public Status status;           // 0=Inside Radius, 1=Outside Radius
            public Status lastStatus;       // diff between this and status determines when to trigger update events
        }

        public bool prediction;             // Was this added as a prediction?
        public int samples;                 // 0=none, 1=Log, 2=Sample 1, 3=Sample 2, 4=Analyse
        public bool complete;               // Sampling of this biological is complete
        public int value
        {
            get {
                int val = 1;
                try
                {
                    val = 2;
                    if ( data != null )
                    {
                        val = (int)data.value;
                    }
                }
                catch
                {
                    val = 99;
                }
                return val;
            }

        }
        public Coordinates[] coords;        // coordinates of scan [n-1]. Only Log and Sample are stored.

        //public Exobiology ( bool prediction=false) : base ()
        //{
        //    this.prediction = prediction;
        //    this.samples = 0;
        //    coords = new Coordinates [ 2 ];
        //    for ( int i = 0; i < 2; i++ )
        //    {
        //        coords[ i ] = new Coordinates();
        //    }
        //}

        public Exobiology ( string edname_genus, bool prediction = false ) : base()
        {
            if ( edname_genus != null )
            {
                this.prediction = prediction;
                this.samples = 0;
                coords = new Coordinates[ 2 ];
                for ( int i = 0; i < 2; i++ )
                {
                    coords[ i ] = new Coordinates();
                }

                this.genus = OrganicInfo.SetGenus( edname_genus );
            }
        }

        [PublicAPI]
        /// <summary>Get all the biological data, this should be done at the first sample</summary>
        public void SetData ( string edname_variant )
        {
            OrganicItem item = OrganicInfo.LookupByVariant( edname_variant );

            this.exists = item.exists;
            this.genus = item.genus;
            this.species = item.species;
            this.variant = item.variant;
            this.data = item.data;
        }

        /// <summary>Increase the sample count, set the coordinates, and return the number of scans complete.</summary>
        public int Sample ( string scanType, string edname_variant, decimal? latitude, decimal? longitude )
        {
            // Never scanned before? Update data.
            if ( samples == 0 )
            {
                SetData( edname_variant );
                complete = false;
            }

            // Check for sample type and update sample numbers
            if ( scanType == "Log" )
            {
                try
                {
                    samples = 1;

                    if ( coords[ samples - 1 ].latitude == null )
                    {
                        coords[ samples - 1 ].latitude = new decimal( 0 );
                    }
                    if ( coords[ samples - 1 ].longitude == null )
                    {
                        coords[ samples - 1 ].longitude = new decimal( 0 );
                    }

                    coords[ samples - 1 ].latitude = latitude;
                    coords[ samples - 1 ].longitude = longitude;

                    coords[ samples - 1 ].status = Status.InsideSampleRange;
                    coords[ samples - 1 ].lastStatus = Status.InsideSampleRange;

                    complete = false;
                }
                catch(System.Exception e )
                {
                    Logging.Error( $"Exobiology: Log Failed [{e}]" );
                }
            }
            else if ( scanType == "Sample" && samples==1 )
            {
                try
                {
                    samples = 2;

                    if ( coords[ samples - 1 ].latitude == null )
                    {
                        coords[ samples - 1 ].latitude = new decimal( 0 );
                    }
                    if ( coords[ samples - 1 ].longitude == null )
                    {
                        coords[ samples - 1 ].longitude = new decimal( 0 );
                    }

                    coords[ samples - 1 ].latitude = latitude;
                    coords[ samples - 1 ].longitude = longitude;
                }
                catch ( System.Exception e )
                {
                    Logging.Error( $"Exobiology: Sample 1 Failed [{e}]" );
                }
            }
            else if ( scanType == "Sample" && samples == 2 )
            {
                try
                {
                    samples = 3;
                }
                catch ( System.Exception e )
                {
                    Logging.Error( $"Exobiology: Sample 2 Failed [{e}]" );
                }
            }
            else if ( scanType == "Analyse" )
            {
                complete = true;
                samples = 4;
            }
            
            return samples;
        }

        [PublicAPI]
        /// <summary>Is sampling of this biological complete?</summary>
        public bool IsComplete ()
        {
            return ( samples >= 4);
        }

        [PublicAPI]
        /// <summary>Get the number of samples remaining</summary>
        public int Remaining ()
        {
            return 3 - samples;
        }
    }
}
