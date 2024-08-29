using EddiCore;
using EddiConfigService;
using System;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows;
using Utilities;
using EddiDataDefinitions;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;

namespace EddiDiscoveryMonitor
{
    /// <summary>
    /// Interaction logic for ConfigurationWindow.xaml
    /// </summary>
    public partial class ConfigurationWindow : UserControl
    {
        //private DiscoveryMonitor discoveryMonitor ()
        //{
        //    return (DiscoveryMonitor)EDDI.Instance.ObtainMonitor( "Discovery Monitor" );
        //}

        //public static  configuration = ConfigService.Instance.discoveryMonitorConfiguration;

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

        //private StarSystem currentStarSystem ()
        //{
        //    return (StarSystem)EDDI.Instance?.CurrentStarSystem;
        //}

        internal StarSystem _currentSystem => EDDI.Instance?.CurrentStarSystem;

        public ConfigurationWindow ()
        {
            InitializeComponent();
            EDDI.Instance.PropertyChanged += Instance_PropertyChanged;
            RefreshData();
        }

        protected string _decimalSeperator => CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        private void EnsureValidDecimalWithNegative(object sender, TextCompositionEventArgs e)
        {
            var text = (sender as TextBox).Text;            // Previous text before changes
            var index = (sender as TextBox).CaretIndex;     // Cursor position
            char lastEntry = e.Text[e.Text.Length - 1];

            // TODO: _decimalSeperator should probably be used here (as defined above)
            //      - But this is just a visual item and the toDecimal() function would still require changing ',' back to '.'
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


    }
}
