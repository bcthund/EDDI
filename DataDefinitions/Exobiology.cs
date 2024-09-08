using System;
using System.Collections.Generic;
using System.ComponentModel;
using Utilities;
using Newtonsoft.Json;

namespace EddiDataDefinitions
{
    public class Exobiology : Organic, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }

        public enum State
        {
            Predicted,
            Confirmed,
            SampleStarted,    // Logged (1st sample collected)
            SampleInProgress, // Sampled (2nd sample collected)
            SampleComplete,   // Sampled (3rd sample collected)
            SampleAnalysed    // Analysed - this comes shortly after the final sample is collected
        }

        
        private State _scanState;

        [JsonProperty]
        public State ScanState
        {
            get { return _scanState; }
            set {
                _scanState = value;
                OnPropertyChanged("ScanState");
            }
        }

        [JsonIgnore]
        public OrganicGenus Genus
        {
            get { return this.genus; }
            set {
                this.genus = value;
                OnPropertyChanged("Genus");
            }
        }

        [JsonIgnore]
        public OrganicSpecies Species
        {
            get { return this.species; }
            set {
                this.species = value;
                OnPropertyChanged("Species");
            }
        }

        [JsonIgnore]
        public OrganicVariant Variant
        {
            get { return this.variant; }
            set {
                this.variant = value;
                OnPropertyChanged("Variant");
            }
        }

        [PublicAPI, JsonIgnore]
        public string state => ScanState.ToString();

        // coordinates of scan [n-1]. Only Log and Sample are stored.
        [ PublicAPI ]
        public List<Tuple<decimal?, decimal?>> sampleCoords = new List<Tuple<decimal?, decimal?>>(); 
        
        [PublicAPI, JsonProperty]
        public bool nearPriorSample { get; set; }

        [PublicAPI, JsonIgnore]
        public int samples => sampleCoords.Count;

        public Exobiology () 
        {
        }

        // This was made specifically for predictions
        //public Exobiology ( Organic organic, bool isPrediction = false ) : base( organic.genus )
        //{
        //    this = (Exobiology) organic;
        //    this.ScanState = isPrediction ? State.Predicted : State.Confirmed;
        //}

        public Exobiology ( OrganicGenus genus, bool isPrediction = false ) : base ( genus )
        {
            this.Genus = genus;
            this.ScanState = isPrediction ? State.Predicted : State.Confirmed;
        }

        public Exobiology ( OrganicSpecies species, bool isPrediction = false ) : base( species )
        {
            this.Species = species;
            this.ScanState = isPrediction ? State.Predicted : State.Confirmed;
        }

        public Exobiology ( OrganicVariant variant, bool isPrediction = false ) : base( variant )
        {
            this.Variant = variant;
            this.ScanState = isPrediction ? State.Predicted : State.Confirmed;
        }

        public void SetScanState(State state) {
            this.ScanState = state;
        }

        public void SetPrediction(bool isPrediction) {
            this.ScanState = isPrediction ? State.Predicted : State.Confirmed;
        }

        /// <summary>Increase the sample count, set the coordinates, and return the number of scans complete.</summary>
        public void Sample ( string scanType, OrganicVariant sampleVariant, decimal? latitude, decimal? longitude )
        {
            if ( this.Variant is null )
            {
                SetVariantData( sampleVariant );
            }

            // Check for sample type and update sample coordinates
            if ( scanType == "Log" )
            {
                ScanState = State.SampleStarted;
                sampleCoords.Add( new Tuple<decimal?, decimal?>( latitude, longitude ) );
            }
            else if ( scanType == "Sample" && samples < 2 )
            {
                ScanState = State.SampleInProgress;
                sampleCoords.Add( new Tuple<decimal?, decimal?>( latitude, longitude ) );
            }
            else if ( scanType == "Sample" && samples == 2 )
            {
                ScanState = State.SampleComplete;
                sampleCoords.Add( new Tuple<decimal?, decimal?>( latitude, longitude ) );
            }
            else if ( scanType == "Analyse" )
            {
                ScanState = State.SampleAnalysed;
            } 
            
            nearPriorSample = true;
        }
    }
}
