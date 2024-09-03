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
    /// Interaction logic for Tab_Nebulae.xaml
    /// </summary>
    public partial class Tab_Nebulae : UserControl
    {
        public static Tab_General.Coord systemCoord => Tab_General.systemCoord;

        List<Nebula> nebulaResult = new List<Nebula>();

        public Tab_Nebulae ()
        {
            InitializeComponent();
            datagrid_NebulaData.DataContext = nebulaResult;
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

        private void onClick_GetNebula ( object sender, RoutedEventArgs e )
        {
            try
            {
                try
                {
                    var maxDistance = Convert.ToInt32(nebulaMaxDistance.Text);
                    var maxCount = Convert.ToInt32(nebulaMaxCount.Text);

                    if(radioNebulafilter_Type_All.IsChecked == true) {
                        nebulaResult = Nebula.TryGetNearestNebulae(systemCoord.x, systemCoord.y, systemCoord.z, maxCount, maxDistance);
                    }
                    else if (radioNebulafilter_Type_Standard.IsChecked == true) {
                        nebulaResult = Nebula.TryGetNearestNebulae(Nebula.NebulaType.Standard, systemCoord.x, systemCoord.y, systemCoord.z, maxCount, maxDistance);
                    }
                    else if (radioNebulafilter_Type_Real.IsChecked == true) {
                        nebulaResult = Nebula.TryGetNearestNebulae(Nebula.NebulaType.Real, systemCoord.x, systemCoord.y, systemCoord.z, maxCount, maxDistance);
                    }
                    else if (radioNebulafilter_Type_Planetary.IsChecked == true) {
                        nebulaResult = Nebula.TryGetNearestNebulae(Nebula.NebulaType.Planetary, systemCoord.x, systemCoord.y, systemCoord.z, maxCount, maxDistance);
                    }
                }
                catch
                {
                    // Error; ignore it
                }
            }
            catch
            {
                // Error; ignore it
            }
            
            datagrid_NebulaData.DataContext = nebulaResult;
        }


    }
}
