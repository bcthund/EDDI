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
using System.Collections.Generic;
using Windows.UI.Xaml.Data;
//using System.Windows.Data;
using System.Security.Permissions;
using System.Linq;

namespace EddiDiscoveryMonitor
{
    /// <summary>
    /// Interaction logic for Tab_Exobiology.xaml
    /// </summary>
    public partial class Tab_Exobiology : UserControl
    {
        public ObservableCollection<Exobiology> bioSignals { get; set; }

        //public long currentBodyId {get; set; }
        public long? currentBodyId => discoveryMonitor().CurrentBodyId;

        public Body currentBody => EDDI.Instance?.CurrentStarSystem?.BodyWithID( currentBodyId );

        public OrganicGenus selectedGenus;

        public Exobiology selectedBio => currentBody?.surfaceSignals.bioSignals.Where(x => x.genus==selectedGenus).First();

        public long? _currentBodyId { get; set; }

        public Body _currentBody { get; set; }


        private DiscoveryMonitor discoveryMonitor ()
        {
            return (DiscoveryMonitor)EDDI.Instance.ObtainMonitor( "Discovery Monitor" );
        }

        public Tab_Exobiology ()
        {
            InitializeComponent();

            discoveryMonitor().PropertyChanged += DiscoveryMonitor_PropertyChanged;

            this.DataContext = this;

            _currentBodyId = currentBodyId;
            _currentBody = currentBody;

            bioSignals = new ObservableCollection<Exobiology>();
            if(currentBody != null ) {
                foreach ( Exobiology bio in currentBody.surfaceSignals?.bioSignals ) {
                    bioSignals.Add( bio );
                }
            }
            datagrid_bioData.DataContext = bioSignals;

            if(currentBody != null ) {
                textbox_CurrentSystemName.Text = _currentBody?.systemname;
                textbox_CurrentBodyId.Text = _currentBodyId.ToString();
                textbox_CurrentBodyShortName.Text = _currentBody?.shortname;
            }
            else {
                textbox_CurrentSystemName.Text = "";
                textbox_CurrentBodyId.Text = "";
                textbox_CurrentBodyShortName.Text = "";
            }

            selectedBio_Grid.DataContext = selectedBio;
            SetBioData();
        }

        void DiscoveryMonitor_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentBodyId" || e.PropertyName == "RefreshData")
            {
                this.Dispatcher.Invoke(() =>
                {
                    _currentBodyId = ((DiscoveryMonitor)sender).CurrentBodyId;
                    _currentBody = EDDI.Instance?.CurrentStarSystem?.BodyWithID( _currentBodyId );;

                    RefreshData();
                });
            }
        }

        void datagrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()+1).ToString();
        }

        private void buttonPredict ( object sender, RoutedEventArgs e ) {
            if (_currentBodyId != null ) {
                discoveryMonitor().UpdatePredictedBios( _currentBodyId );

                RefreshData();
            }
        }

        private void buttonRefresh ( object sender, RoutedEventArgs e )
        {
            _currentBodyId = currentBodyId;
            _currentBody = currentBody;

            RefreshData();
        }

        private void RefreshData() {
            if(currentBody != null ) {
                textbox_CurrentSystemName.Text = _currentBody?.systemname;
                textbox_CurrentBodyId.Text = _currentBodyId.ToString();
                textbox_CurrentBodyShortName.Text = _currentBody?.shortname;
            }
            
            bioSignals = new ObservableCollection<Exobiology>();

            if(currentBody != null ) {
                foreach ( Exobiology bio in currentBody.surfaceSignals?.bioSignals ) {
                    bioSignals.Add( bio );
                }
            }

            datagrid_bioData.DataContext = bioSignals;
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

            // Future Reference - Getting Cell Data
            //DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            //DataGridCell RowColumn = dataGrid.Columns[ColumnIndex].GetCellContent(row).Parent as DataGridCell;
            //string CellValue = ((TextBlock)RowColumn.Content).Text;

            var row = datagrid_bioData.SelectedIndex;
            selectedGenus = bioSignals[row].genus;

            selectedBio_Grid.DataContext = selectedBio;

            SetBioData();

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

        //public object BooleanToVisibiltyConverter(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        //{
        //    bool isVisible = (bool)value;
        //    return (isVisible ? Visibility.Visible : Visibility.Collapsed);
        //}

    }
}
