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

namespace EddiDiscoveryMonitor
{
    /// <summary>
    /// Interaction logic for Tab_Guardian.xaml
    /// </summary>
    public partial class Tab_Guardian : UserControl
    {
        public static Tab_General.Coord systemCoord => Tab_General.systemCoord;

        List<GuardianSite> guardianResult = new List<GuardianSite>();

        public Tab_Guardian ()
        {
            InitializeComponent();
            datagrid_GuardianData.DataContext = guardianResult;
        }

        private void EnsureValidInteger(object sender, TextCompositionEventArgs e)
        {
            // No Negative
            Regex regex = new Regex(@"[0-9]");      // Match valid characters
            e.Handled = !regex.IsMatch(e.Text);     // Swallow the character doesn't match the regex
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

        void datagrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()+1).ToString();
        }

        private void onClick_GetGuardianSite ( object sender, RoutedEventArgs e )
        {
            try
            {
                var maxDistance = Convert.ToInt32(guardianMaxDistance.Text);
                var maxCount = Convert.ToInt32(guardianMaxCount.Text);

                if (radioGuardianfilter_All.IsChecked == true ) {
                    guardianResult = GuardianSite.TryGetNearestGuardianSites(systemCoord.x, systemCoord.y, systemCoord.z, maxCount, maxDistance);
                }
                else {
                    if (radioGuardianfilter_Beacons.IsChecked == true ) {
                        guardianResult = GuardianSite.TryGetNearestGuardianSites(GuardianSite.GuardianSiteType.Beacon, systemCoord.x, systemCoord.y, systemCoord.z, maxCount, maxDistance);
                    }
                    else if (radioGuardianfilter_Ruins.IsChecked == true ) {
                        guardianResult = GuardianSite.TryGetNearestGuardianSites(GuardianSite.GuardianSiteType.Ruin, systemCoord.x, systemCoord.y, systemCoord.z, maxCount, maxDistance);
                    }
                    else if (radioGuardianfilter_Structures.IsChecked == true ) {
                        guardianResult = GuardianSite.TryGetNearestGuardianSites(GuardianSite.GuardianSiteType.Structure, systemCoord.x, systemCoord.y, systemCoord.z, maxCount, maxDistance);
                    }
                    else if (radioGuardianfilter_Vessel.IsChecked == true ) {
                        guardianResult = GuardianSite.TryGetNearestGuardianSites(GuardianSite.BlueprintType.Vessel, systemCoord.x, systemCoord.y, systemCoord.z, maxCount, maxDistance);
                    }
                    else if (radioGuardianfilter_Weapon.IsChecked == true ) {
                        guardianResult = GuardianSite.TryGetNearestGuardianSites(GuardianSite.BlueprintType.Weapon, systemCoord.x, systemCoord.y, systemCoord.z, maxCount, maxDistance);
                    }
                    else if (radioGuardianfilter_Module.IsChecked == true ) {
                        guardianResult = GuardianSite.TryGetNearestGuardianSites(GuardianSite.BlueprintType.Module, systemCoord.x, systemCoord.y, systemCoord.z, maxCount, maxDistance);
                    }
                }
            }
            catch
            {
                // Error; ignore it
            }

            datagrid_GuardianData.DataContext = guardianResult;
        }
    }
}
