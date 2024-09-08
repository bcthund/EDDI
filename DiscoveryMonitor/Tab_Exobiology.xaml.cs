using EddiCore;
using EddiConfigService;
using System;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows;
using Utilities;
using EddiDataDefinitions;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace EddiDiscoveryMonitor
{
    /// <summary>
    /// Interaction logic for Tab_Exobiology.xaml
    /// </summary>
    public partial class Tab_Exobiology : UserControl
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }

        public ObservableCollection<Exobiology> bioSignals { get; set; }

        public StarSystem currentStarSystem => EDDI.Instance?.CurrentStarSystem;

        public long? currentBodyId => discoveryMonitor().CurrentBodyId;

        public OrganicGenus selectedGenus;

        public Exobiology selectedBio;

        internal bool isPredicting = false;
        internal bool ignoreSelectionChange = false;


        internal int _lastPlanetIndex { get; set; }
        internal int _lastBioIndex { get; set; }

        internal long? _currentBodyId { get; set; }
        internal long? _lastBodyId { get; set; }

        public long? CurrentBodyId
        {
            get { return _currentBodyId; }
            set {
                _lastBioIndex = datagrid_bioData.SelectedIndex;
                _lastPlanetIndex = datagrid_PlanetsWithBios.SelectedIndex;
                _lastBodyId = _currentBodyId;
                _currentBodyId = value;
                OnPropertyChanged("CurrentBodyId");
            }
        }

        internal Body _currentBody { get; set; }
        internal Body _lastBody { get; set; }
        public Body CurrentBody
        {
            get { return _currentBody; }
            set {
                _lastBody = _currentBody;
                _currentBody = value;
                OnPropertyChanged("CurrentBody");
            }
        }

        private DiscoveryMonitor discoveryMonitor ()
        {
            return (DiscoveryMonitor)EDDI.Instance.ObtainMonitor( "Discovery Monitor" );
        }

        public Tab_Exobiology ()
        {
            InitializeComponent();

            discoveryMonitor().PropertyChanged += DiscoveryMonitor_PropertyChanged;
            EDDI.Instance.PropertyChanged += EddiInstance_PropertyChanged;
            this.PropertyChanged += This_PropertyChanged;

            //this.DataContext = this;

            CurrentBodyId = currentBodyId;
            CurrentBody = currentStarSystem?.BodyWithID( CurrentBodyId );

            bioSignals = new ObservableCollection<Exobiology>();
            if ( CurrentBody != null )
            {
                foreach ( Exobiology bio in CurrentBody.surfaceSignals?.bioSignals )
                {
                    bioSignals.Add( bio );
                }
            }
            datagrid_bioData.DataContext = bioSignals;

            if ( CurrentBody != null )
            {
                //textbox_CurrentSystemName.Text = CurrentBody?.systemname;
                textbox_CurrentSystemName.Text = currentStarSystem.systemname;
                textbox_CurrentBodyId.Text = CurrentBodyId.ToString();
                textbox_CurrentBodyShortName.Text = CurrentBody?.shortname;
            }
            else
            {
                textbox_CurrentSystemName.Text = "";
                textbox_CurrentBodyId.Text = "";
                textbox_CurrentBodyShortName.Text = "";
            }

            //selectedBio_Grid.DataContext = selectedBio;
            SetBioData();
        }

        /// <summary>
        /// If the discovery monitor changes its current body due to a handled event then update displayed data
        /// Also monitor manual property update via "RefreshData"
        /// Also update after a "handleScanOrganicEvent" which occurs after the DiscoveryMonitor has made its own internal updates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DiscoveryMonitor_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ( !isPredicting && (e.PropertyName == "CurrentBodyId" || e.PropertyName == "RefreshData" || e.PropertyName == "handleScanOrganicEvent" ) )
            {
                this.Dispatcher.Invoke( () =>
                {
                    long? newBodyId = ( (DiscoveryMonitor)sender ).CurrentBodyId;

                    var getIndex = currentStarSystem.bodies.Where(x=>x.surfaceSignals.reportedBiologicalCount > 0).ToList().FindIndex(x => x.bodyId == newBodyId);
                    datagrid_PlanetsWithBios.SelectedIndex = getIndex;

                    CurrentBodyId = newBodyId;
                    CurrentBody = EDDI.Instance?.CurrentStarSystem?.BodyWithID( CurrentBodyId );

                    RefreshData();
                } );
            }
        }

        /// <summary>
        /// If the current star system has changed, then reset selections/internals and refresh all displayed data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EddiInstance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ( !isPredicting && (e.PropertyName == "CurrentStarSystem") )
            {
                this.Dispatcher.Invoke( () =>
                {
                    CurrentBodyId = 0;
                    datagrid_bioData.SelectedIndex = -1;
                    CurrentBody = null;
                    selectedBio = null;

                    RefreshData();
                    SetBioData();
                } );
            }
        }

        /// <summary>
        /// If the contextual body has changed, set bio selection to none and clear current display data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void This_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ( !isPredicting && (e.PropertyName == "CurrentBodyId") )
            {
                datagrid_bioData.SelectedIndex = -1;
                selectedBio = null;
                SetBioData();
            }
        }

        /// <summary>
        /// Numbered rows for biologicals
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void datagrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()+1).ToString();
        }

        /// <summary>
        /// Force a prediction retry, will erase current biosignals (even already sampled ones) on body and recreate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPredict ( object sender, RoutedEventArgs e ) {
            if (CurrentBodyId != null ) {
                isPredicting = true;
                discoveryMonitor().UpdatePredictedBios( currentStarSystem.systemAddress, CurrentBodyId );
                CurrentBody = EDDI.Instance?.CurrentStarSystem?.BodyWithID( CurrentBodyId );

                RefreshData(false);
                isPredicting = false;
            }
        }

        /// <summary>
        /// Manual refresh of planets/biologicals
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRefresh ( object sender, RoutedEventArgs e )
        {
            RefreshData(true);
        }

        /// <summary>
        /// Refresh the list of planets and the list of biologicals for the current star system
        /// </summary>
        /// <param name="refreshPlanets"></param>
        private void RefreshData (bool refreshPlanets=true)
        {
            if ( currentStarSystem != null ) {

                if ( refreshPlanets ) {
                    datagrid_PlanetsWithBios.DataContext = currentStarSystem.bodies.Where(x=>x.surfaceSignals.reportedBiologicalCount > 0).ToList();
                    if(_lastBodyId==CurrentBodyId) {
                        ignoreSelectionChange = true;
                        datagrid_PlanetsWithBios.SelectedIndex = _lastPlanetIndex;
                        ignoreSelectionChange = false;
                    }
                }

                if ( CurrentBody != null )
                {
                    //textbox_CurrentSystemName.Text = CurrentBody?.systemname;
                    textbox_CurrentSystemName.Text = currentStarSystem.systemname;
                    textbox_CurrentBodyId.Text = CurrentBodyId.ToString();
                    textbox_CurrentBodyShortName.Text = CurrentBody?.shortname;
                }

                bioSignals = new ObservableCollection<Exobiology>();

                if ( CurrentBody != null )
                {
                    foreach ( Exobiology bio in CurrentBody.surfaceSignals?.bioSignals )
                    {
                        bioSignals.Add( bio );
                    }
                }

                string contextBioName = discoveryMonitor()._currentOrganic?.Genus.localizedName ?? "";
                var getIndex = bioSignals.ToList().FindIndex(x=>x.genus.localizedName==contextBioName);

                datagrid_bioData.DataContext = bioSignals;
                if(_lastBodyId==CurrentBodyId) {
                    ignoreSelectionChange = true;
                    datagrid_bioData.SelectedIndex = getIndex;
                    ignoreSelectionChange = false;
                }
            }
        }

        /// <summary>
        /// Ensure a TextBox only has valid integers (non-negative)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Ensure a TextBox only has valid integers (non-negative)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnsureValidInteger(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[0-9]");      // Match valid characters
            e.Handled = !regex.IsMatch(e.Text);     // Swallow the character doesn't match the regex
        }

        /// <summary>
        /// Biological selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datagrid_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            DataGrid dataGrid = sender as DataGrid;

            if ( datagrid_bioData?.SelectedIndex >= 0 ) {
                var row = datagrid_bioData?.SelectedIndex;
                if (row != null) {
                    selectedGenus = bioSignals[(int)row].genus;

                    selectedBio = CurrentBody?.surfaceSignals?.bioSignals?.Where(x => x.genus==selectedGenus).First();
                    selectedBio_Grid.DataContext = selectedBio;

                    SetBioData();
                }
            }

        }

        /// <summary>
        /// Update the visibility and content of selected biological data
        /// </summary>
        public void SetBioData() {
            textBlock_Genus.Visibility = Visibility.Hidden;
            textBlock_Species.Visibility = Visibility.Hidden;
            
            selectedBio_Sample1_x.Content = "";
            selectedBio_Sample1_y.Content = "";
            selectedBio_Sample2_x.Content = "";
            selectedBio_Sample2_y.Content = "";
            selectedBio_Sample3_x.Content = "";
            selectedBio_Sample3_y.Content = "";

            selectedBio_samples_title.Visibility = Visibility.Hidden;
            selectedBio_x_title.Visibility = Visibility.Hidden;
            selectedBio_y_title.Visibility = Visibility.Hidden;

            selectedBio_Sample1_header.Visibility = Visibility.Hidden;
            selectedBio_Sample1_x.Visibility = Visibility.Hidden;
            selectedBio_Sample1_y.Visibility = Visibility.Hidden;

            selectedBio_Sample2_header.Visibility = Visibility.Hidden;
            selectedBio_Sample2_x.Visibility = Visibility.Hidden;
            selectedBio_Sample2_y.Visibility = Visibility.Hidden;

            selectedBio_Sample3_header.Visibility = Visibility.Hidden;
            selectedBio_Sample3_x.Visibility = Visibility.Hidden;
            selectedBio_Sample3_y.Visibility = Visibility.Hidden;
            
            if (selectedBio != null) {
            
                if (selectedBio.genus != null) {
                    textBlock_Genus.Visibility = Visibility.Visible;
                }

                if (selectedBio.species != null) {
                    textBlock_Species.Visibility = Visibility.Visible;
                }

                if(selectedBio.sampleCoords.Count >= 1 ) {
                    selectedBio_samples_title.Visibility = Visibility.Visible;
                    selectedBio_x_title.Visibility = Visibility.Visible;
                    selectedBio_y_title.Visibility = Visibility.Visible;
                    
                    selectedBio_Sample1_x.Content = selectedBio.sampleCoords[0]?.Item1.ToString();
                    selectedBio_Sample1_y.Content = selectedBio.sampleCoords[0]?.Item2.ToString();
                    selectedBio_Sample1_header.Visibility = Visibility.Visible;
                    selectedBio_Sample1_x.Visibility = Visibility.Visible;
                    selectedBio_Sample1_y.Visibility = Visibility.Visible;
                }

                if(selectedBio.sampleCoords.Count >= 2 ) {
                    selectedBio_Sample2_x.Content = selectedBio.sampleCoords[1]?.Item1.ToString();
                    selectedBio_Sample2_y.Content = selectedBio.sampleCoords[1]?.Item2.ToString();
                    selectedBio_Sample2_header.Visibility = Visibility.Visible;
                    selectedBio_Sample2_x.Visibility = Visibility.Visible;
                    selectedBio_Sample2_y.Visibility = Visibility.Visible;
                }

                if(selectedBio.sampleCoords.Count >= 3 ) {
                    selectedBio_Sample3_x.Content = selectedBio.sampleCoords[2]?.Item1.ToString();
                    selectedBio_Sample3_y.Content = selectedBio.sampleCoords[2]?.Item2.ToString();
                    selectedBio_Sample3_header.Visibility = Visibility.Visible;
                    selectedBio_Sample3_x.Visibility = Visibility.Visible;
                    selectedBio_Sample3_y.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// When ENTER key pressed, try to set the ID as the new current body Id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textbox_CurrentBodyId_KeyDown ( object sender, KeyEventArgs e )
        {
            if (e.Key != Key.Enter) return;

            if (textbox_CurrentBodyId.Text.Length>0) {
                long? newBodyId = Convert.ToInt64(textbox_CurrentBodyId.Text);

                if ( newBodyId != null ) {
                    if ( currentStarSystem.bodies.Exists(x=>x.bodyId==newBodyId) ) {
                        CurrentBodyId = newBodyId;
                        CurrentBody = currentStarSystem?.BodyWithID( CurrentBodyId );
                        RefreshData();
                    }
                }
            }
        }

        /// <summary>
        /// Currently selected planet has changed, update internals and refresh the biologicals list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datagrid_PlanetsWithBios_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            if ( datagrid_PlanetsWithBios?.SelectedIndex >= 0 ) {
                var row = datagrid_PlanetsWithBios?.SelectedIndex;
                if (row != null) {
                    
                    var newBodyId = (this.datagrid_PlanetsWithBios.SelectedItem as Body).bodyId;
                    CurrentBodyId = newBodyId;
                    CurrentBody = currentStarSystem?.BodyWithID( CurrentBodyId );
                    RefreshData(false);
                    SetBioData();
                }
            }
        }

    }
}
