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
using System.Windows.Data;
using System.Security.Permissions;

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

            _currentBodyId = currentBodyId;
            _currentBody = currentBody;

            bioSignals = new ObservableCollection<Exobiology>();

            if(currentBody != null ) {
                foreach ( Exobiology bio in currentBody.surfaceSignals?.bioSignals ) {
                    bioSignals.Add( bio );
                }
            }

            datagrid_bioData.DataContext = bioSignals;

            this.DataContext = this;
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
    }
}
