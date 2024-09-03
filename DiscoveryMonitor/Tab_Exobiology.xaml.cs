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

        //public HashSet<Exobiology> biosignals2 => currentBody?.surfaceSignals?.bioSignals;

        public StarSystem currentStarSystem => EDDI.Instance?.CurrentStarSystem;

        public long? currentBodyId => discoveryMonitor().CurrentBodyId;

        //public Body currentBody => currentStarSystem?.BodyWithID( currentBodyId );

        public OrganicGenus selectedGenus;

        //public Exobiology selectedBio => currentBody?.surfaceSignals?.bioSignals?.Where(x => x.genus==selectedGenus).First();
        public Exobiology selectedBio;

        private bool isPredicting = false;

        internal long? _currentBodyId { get; set; }

        public long? CurrentBodyId
        {
            get { return _currentBodyId; }
            set {
                _currentBodyId = value;
                OnPropertyChanged("CurrentBodyId");
            }
        }

        internal Body _currentBody { get; set; }
        public Body CurrentBody
        {
            get { return _currentBody; }
            set {
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
                textbox_CurrentSystemName.Text = CurrentBody?.systemname;
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

        void DiscoveryMonitor_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ( !isPredicting && (e.PropertyName == "CurrentBodyId" || e.PropertyName == "RefreshData") )
            {
                this.Dispatcher.Invoke( () =>
                {
                    //Logging.Debug($"[00] ========> DiscoveryMonitor_PropertyChanged INVOKED ");
                    datagrid_bioData.SelectedIndex = -1;
                    CurrentBodyId = ( (DiscoveryMonitor)sender ).CurrentBodyId;
                    CurrentBody = EDDI.Instance?.CurrentStarSystem?.BodyWithID( CurrentBodyId );

                    RefreshData();
                } );
            }
        }

        void EddiInstance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ( !isPredicting && (e.PropertyName == "CurrentStarSystem") )
            {
                this.Dispatcher.Invoke( () =>
                {
                    //Logging.Debug($"[01] EddiInstance_PropertyChanged INVOKED ");
                    CurrentBodyId = 0;
                    datagrid_bioData.SelectedIndex = -1;
                    CurrentBody = null;
                    selectedBio = null;

                    RefreshData();
                    SetBioData();
                } );
            }
        }

        void This_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ( !isPredicting && (e.PropertyName == "CurrentBodyId") )
            {
                Logging.Debug( $"======> CurrentBodyId Property Changed" );
                // If the contextual body has changed, set bio selection to none and clear current display data
                datagrid_bioData.SelectedIndex = -1;
                selectedBio = null;
                SetBioData();
            }
        }

        void datagrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()+1).ToString();
        }

        private void buttonPredict ( object sender, RoutedEventArgs e ) {
            if (CurrentBodyId != null ) {
                isPredicting = true;
                //Logging.Debug($"[02] buttonPredict INVOKED ");
                discoveryMonitor().UpdatePredictedBios( currentStarSystem.systemAddress, CurrentBodyId );
                CurrentBody = EDDI.Instance?.CurrentStarSystem?.BodyWithID( CurrentBodyId );

                RefreshData(false);
                isPredicting = false;
            }
        }

        private void buttonRefresh ( object sender, RoutedEventArgs e )
        {
            CurrentBodyId = currentBodyId;
            CurrentBody = currentStarSystem?.BodyWithID( CurrentBodyId );

            RefreshData();
        }

        private void RefreshData (bool refreshPlanets=true)
        {
            //Logging.Debug($"[03] RefreshData INVOKED ");
            if ( currentStarSystem != null ) {

                if (refreshPlanets ) {
                    datagrid_PlanetsWithBios.DataContext = currentStarSystem.bodies.Where(x=>x.surfaceSignals.reportedBiologicalCount > 0).ToList();
                }

                if ( CurrentBody != null )
                {
                    textbox_CurrentSystemName.Text = CurrentBody?.systemname;
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

                datagrid_bioData.DataContext = bioSignals;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void EnsureValidInteger(object sender, TextCompositionEventArgs e)
        {
            // Match valid characters
            Regex regex = new Regex(@"[0-9]");
            // Swallow the character doesn't match the regex
            e.Handled = !regex.IsMatch(e.Text);
        }

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

        // When ENTER key pressed, try to set the ID as the new current body Id
        private void textbox_CurrentBodyId_KeyDown ( object sender, KeyEventArgs e )
        {
            //Logging.Debug($"TEXT CHANGED ({e.Key})");
            //string log = $"";

            if (e.Key != Key.Enter) return;

            //log += $"Enter Key Pressed: ";

            if (textbox_CurrentBodyId.Text.Length>0) {
                //log += $"Textbox length > 0, ";
                long? newBodyId = Convert.ToInt64(textbox_CurrentBodyId.Text);

                //log += $"newBodyId = {newBodyId}, ";

                if ( newBodyId != null ) {
                    if ( currentStarSystem.bodies.Exists(x=>x.bodyId==newBodyId) ) {

                        //log += $"body existis ";

                        CurrentBodyId = newBodyId;
                        CurrentBody = currentStarSystem?.BodyWithID( CurrentBodyId );

                        //log += $"({CurrentBody.bodyname}), REFRESH DATA...";

                        RefreshData();
                    }
                }
            }
            //Logging.Debug($"{log}");
        }

        private void datagrid_PlanetsWithBios_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            //DataGrid dataGrid = sender as DataGrid;
            // Future Reference - Getting Cell Data
            //DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            //DataGridCell RowColumn = dataGrid.Columns[ColumnIndex].GetCellContent(row).Parent as DataGridCell;
            //string CellValue = ((TextBlock)RowColumn.Content).Text;

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
