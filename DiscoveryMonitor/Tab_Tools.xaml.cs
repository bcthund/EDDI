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

namespace EddiDiscoveryMonitor
{
    /// <summary>
    /// Interaction logic for Tab_Tools.xaml
    /// </summary>
    public partial class Tab_Tools : UserControl
    {
        private DiscoveryMonitor discoveryMonitor ()
        {
            return (DiscoveryMonitor)EDDI.Instance.ObtainMonitor( "Discovery Monitor" );
        }

        public Tab_Tools ()
        {
            InitializeComponent();
        }

        private void EnsureValidInteger(object sender, TextCompositionEventArgs e)
        {
            // No Negative

            // Match valid characters
            Regex regex = new Regex(@"[0-9]");
            // Swallow the character doesn't match the regex
            e.Handled = !regex.IsMatch(e.Text);
        }

        protected string _decimalSeperator => CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        private void EnsureValidDecimalWithNegative(object sender, TextCompositionEventArgs e)
        {
            var text = (sender as TextBox).Text;            // Previous text before changes
            //var pos = (sender as TextBox).SelectionStart;   // Cursor position
            var index = (sender as TextBox).CaretIndex;     // Cursor position

            char lastEntry = e.Text[e.Text.Length - 1];

            //var log = $" ======================================> ({e.Text})";
            //log += $", (last='{lastEntry}'), (pos={pos}), (index={index}), ('.'Count={text.Count(t => t == '.' )}), ('.'={!(text.Count(t => t == '.' )>1)}), ('-' Count={text.Count(t => t == '-' )}), ('-'={!(text.Count(t => t == '-' )>1)}), (len={text.Length}), (digit={Char.IsDigit(lastEntry)})";
            //Logging.Debug( log );

            // TODO: _decimalSeperator should probably be used here (as defined above)
            //      - But this is just a visual item and the toDecimal() function would still require changing ',' back to '.'
            e.Handled = !( (!(text.Count(t => t == '.' )>0) && lastEntry == '.') 
                        || (!(text.Count(t => t == '-' )>0) && (text.Length == 0 || index==0) && lastEntry == '-')
                        || Char.IsDigit(lastEntry) );
        }

        private void onClick_GetSystemAddress ( object sender, RoutedEventArgs e )
        {

            StarSystem currentSystem = EDDI.Instance?.CurrentStarSystem;

            if ( currentSystem != null )
            {
                system_xCoord.Text = currentSystem.x.ToString();
                system_yCoord.Text = currentSystem.y.ToString();
                system_zCoord.Text = currentSystem.z.ToString();
            }
            else
            {
                system_xCoord.Text = "0";
                system_yCoord.Text = "0";
                system_zCoord.Text = "0";
            }
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
                double x = Convert.ToDouble(system_xCoord.Text);
                double y = Convert.ToDouble(system_yCoord.Text);
                double z = Convert.ToDouble(system_zCoord.Text);

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
                regionName.Text = "Coordinate conversion error.";
            }
        }

        private void onClick_GetNebula ( object sender, RoutedEventArgs e )
        {
            try
            {
                decimal x = Convert.ToDecimal(system_xCoord.Text);
                decimal y = Convert.ToDecimal(system_yCoord.Text);
                decimal z = Convert.ToDecimal(system_zCoord.Text);

                Nebula nebulaResult = Nebula.TryGetNearestNebula(x, y, z);

                if ( nebulaResult != null )
                {
                    nebulaName.Text = nebulaResult.name;
                    nebulaReferenceBody.Text = nebulaResult.referenceBody;
                    nebulaDistance.Text = nebulaResult.distance.ToString();
                }
                else
                {
                    nebulaName.Text = "Error, Not Found";
                    nebulaReferenceBody.Text = "";
                    nebulaDistance.Text = "";
                }
            }
            catch
            {
                // Error; ignore it
                regionName.Text = "Coordinate conversion error.";
            }
        }

        private void onClick_GetGuardianSite ( object sender, RoutedEventArgs e )
        {
            try
            {
                decimal x = Convert.ToDecimal(system_xCoord.Text);
                decimal y = Convert.ToDecimal(system_yCoord.Text);
                decimal z = Convert.ToDecimal(system_zCoord.Text);

                GuardianSite result = null;

                if (radioGuardianfilter_All.IsChecked == true ) {
                    result = GuardianSite.TryGetNearestGuardianSite(x, y, z);
                }
                else {
                    if (radioGuardianfilter_Beacons.IsChecked == true ) {
                        result = GuardianSite.TryGetNearestGuardianSite(GuardianSite.GuardianSiteType.Beacon, x, y, z);
                    }
                    else if (radioGuardianfilter_Ruins.IsChecked == true ) {
                        result = GuardianSite.TryGetNearestGuardianSite(GuardianSite.GuardianSiteType.Ruin, x, y, z);
                    }
                    else if (radioGuardianfilter_Structures.IsChecked == true ) {
                        result = GuardianSite.TryGetNearestGuardianSite(GuardianSite.GuardianSiteType.Structure, x, y, z);
                    }
                    else if (radioGuardianfilter_Vessel.IsChecked == true ) {
                        result = GuardianSite.TryGetNearestGuardianSite(GuardianSite.BlueprintType.Vessel, x, y, z);
                    }
                    else if (radioGuardianfilter_Weapon.IsChecked == true ) {
                        result = GuardianSite.TryGetNearestGuardianSite(GuardianSite.BlueprintType.Weapon, x, y, z);
                    }
                    else if (radioGuardianfilter_Module.IsChecked == true ) {
                        result = GuardianSite.TryGetNearestGuardianSite(GuardianSite.BlueprintType.Module, x, y, z);
                    }
                }

                if ( result != null )
                {
                    guardianSite_Type.Text = result.type.ToString();
                    guardianSite_SystemName.Text = result.systemName;
                    guardianSite_Body.Text = result.body;
                    guardianSite_Blueprint.Text = result.blueprintType.ToString();
                    guardianSite_Distance.Text = result.distance.ToString();
                }
                else
                {
                    guardianSite_Type.Text = "Error, Not Found";
                    guardianSite_SystemName.Text = "";
                    guardianSite_Body.Text = "";
                    guardianSite_Blueprint.Text = "";
                    guardianSite_Distance.Text = "";
                }
            }
            catch
            {
                // Error; ignore it
                regionName.Text = "Coordinate conversion error.";
            }
        }

    }
}
