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
using System.Globalization;
using System.Linq;

namespace EddiDiscoveryMonitor
{
    /// <summary>
    /// Interaction logic for Tab_General.xaml
    /// </summary>
    public partial class Tab_General : UserControl
    {
        
        public class Coord {
            public Decimal? x;
            public Decimal? y;
            public Decimal? z;

            public Coord() {
                x=0;
                y=0;
                z=0;
            }

            public Coord(Decimal? x, Decimal? y, Decimal? z) {
                this.x=x;
                this.y=y;
                this.z=z;
            }
        }

        public static Coord systemCoord = new Coord();

        internal StarSystem _currentSystem => EDDI.Instance?.CurrentStarSystem;

        private DiscoveryMonitor discoveryMonitor ()
        {
            return (DiscoveryMonitor)EDDI.Instance.ObtainMonitor( "Discovery Monitor" );
        }

        public Tab_General ()
        {
            InitializeComponent();
            EDDI.Instance.PropertyChanged += Instance_PropertyChanged;
            RefreshData();

            var configuration = ConfigService.Instance.discoveryMonitorConfiguration;

            checkboxIgnoreBrainTrees.IsChecked = configuration.exobiology.predictions.skipBrancae;
            checkboxIgnoreCrystalShards.IsChecked = configuration.exobiology.predictions.skipGroundStructIce;
            checkboxIgnoreBarkMounds.IsChecked = configuration.exobiology.predictions.skipCone;
            checkboxIgnoreTubers.IsChecked = configuration.exobiology.predictions.skipTubers;
        }

        private void EnsureValidDecimalWithNegative(object sender, TextCompositionEventArgs e)
        {
            var text = (sender as TextBox).Text;            // Previous text before changes
            var index = (sender as TextBox).CaretIndex;     // Cursor position
            char lastEntry = e.Text[e.Text.Length - 1];

            e.Handled = !( (!(text.Count(t => t == '.' )>0) && lastEntry == '.') 
                        || (!(text.Count(t => t == '-' )>0) && (text.Length == 0 || index==0) && lastEntry == '-')
                        || Char.IsDigit(lastEntry) );
        }

        void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentStarSystem")
            {
                this.Dispatcher.Invoke(() =>
                {
                    RefreshData();
                });
            }
        }

        private void RefreshData() {
            if ( _currentSystem != null )
            {
                system_xCoord.Text = _currentSystem.x.ToString();
                system_yCoord.Text = _currentSystem.y.ToString();
                system_zCoord.Text = _currentSystem.z.ToString();
                systemCoord = new Coord(_currentSystem.x, _currentSystem.y, _currentSystem.z);
            }
            else
            {
                systemCoord = new Coord();
            }
        }

        private void onClick_GetSystemAddress ( object sender, RoutedEventArgs e )
        {
            RefreshData();
        }

        private void system_xCoord_Changed ( object sender, TextChangedEventArgs e )
        { }

        private void system_yCoord_Changed ( object sender, TextChangedEventArgs e )
        { }

        private void system_zCoord_Changed ( object sender, TextChangedEventArgs e )
        { }

        private void onClick_FindRegion ( object sender, RoutedEventArgs e )
        {
            try
            {

                double x = Convert.ToDouble(systemCoord.x);
                double y = Convert.ToDouble(systemCoord.y);
                double z = Convert.ToDouble(systemCoord.z);

                Utilities.RegionMap.Region regionResult = Utilities.RegionMap.RegionMap.FindRegion(x, y, z);

                if ( regionResult != null )
                {
                    regionName.Text = regionResult.name;
                }
                else
                {
                    regionName.Text = "Error, Not Found.";
                }
            }
            catch
            {
                // Error; ignore it
                regionName.Text = "Failed to get results.";
            }
        }


        // ########################################
        //      Exobiology
        // ########################################
        private void IgnoreBrainTrees_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        {
            var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
            configuration.exobiology.predictions.skipBrancae = checkboxIgnoreBrainTrees.IsChecked ?? false;
            ConfigService.Instance.discoveryMonitorConfiguration = configuration;
            discoveryMonitor()?.Reload();
        }

        private void IgnoreCrystalShards_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        {
            var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
            configuration.exobiology.predictions.skipGroundStructIce = checkboxIgnoreCrystalShards.IsChecked ?? false;
            ConfigService.Instance.discoveryMonitorConfiguration = configuration;
            discoveryMonitor()?.Reload();
        }

        private void IgnoreBarkMounds_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        {
            var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
            configuration.exobiology.predictions.skipCone = checkboxIgnoreBarkMounds.IsChecked ?? false;
            ConfigService.Instance.discoveryMonitorConfiguration = configuration;
            discoveryMonitor()?.Reload();
        }

        private void IgnoreTubers_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        {
            var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
            configuration.exobiology.predictions.skipTubers = checkboxIgnoreTubers.IsChecked ?? false;
            ConfigService.Instance.discoveryMonitorConfiguration = configuration;
            discoveryMonitor()?.Reload();
        }
    }
}
