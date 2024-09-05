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
using EddiDataProviderService;
using Newtonsoft.Json.Bson;

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
            RefreshNebula();
        }

        private void RefreshNebula() {
            try
            {
                var maxDistance = Convert.ToInt32(nebulaMaxDistance.Text);
                var maxCount = Convert.ToInt32(nebulaMaxCount.Text);

                Nebula.FilterVisited filter = Nebula.FilterVisited.All;
                if (radioNebulafilter_Visted_Visited.IsChecked == true) {
                    filter = Nebula.FilterVisited.Visited;
                }
                else if (radioNebulafilter_Visted_NotVisited.IsChecked == true) {
                    filter = Nebula.FilterVisited.NotVisited;
                }

                if(radioNebulafilter_Type_All.IsChecked == true) {
                    nebulaResult = Nebula.TryGetNearestNebulae(systemCoord.x, systemCoord.y, systemCoord.z, maxCount, maxDistance, filter);
                }
                else if (radioNebulafilter_Type_Standard.IsChecked == true) {
                    nebulaResult = Nebula.TryGetNearestNebulae(Nebula.NebulaType.Standard, systemCoord.x, systemCoord.y, systemCoord.z, maxCount, maxDistance, filter);
                }
                else if (radioNebulafilter_Type_Real.IsChecked == true) {
                    nebulaResult = Nebula.TryGetNearestNebulae(Nebula.NebulaType.Real, systemCoord.x, systemCoord.y, systemCoord.z, maxCount, maxDistance, filter);
                }
                else if (radioNebulafilter_Type_Planetary.IsChecked == true) {
                    nebulaResult = Nebula.TryGetNearestNebulae(Nebula.NebulaType.Planetary, systemCoord.x, systemCoord.y, systemCoord.z, maxCount, maxDistance, filter);
                }

                NebulaSqLiteRepository.Instance.GetNebulaeVisited( ref nebulaResult );
            }
            catch
            {
                // Error; ignore it
            }
            
            datagrid_NebulaData.DataContext = nebulaResult;
        }

        private void datagrid_Nebula_ToggleVisited ( object sender, RoutedEventArgs e )
        {
            Button button = sender as Button;
            NebulaSqLiteRepository.Instance.ToggleNebulaVisited( Convert.ToInt32( button.Tag ) );

            Logging.Debug($"=============> [TOGGLE VISITED] Tag={button.Tag}, Int={Convert.ToInt32( button.Tag )}, {NebulaSqLiteRepository.Instance.GetNebulaVisited( Convert.ToInt32( button.Tag ) )}");

            RefreshNebula();
        }

        private void checkbox_EditVisited_Changed ( object sender, RoutedEventArgs e )
        {
            CheckBox checkbox = sender as CheckBox;
            if (checkbox.IsChecked == true) {
                datagrid_Nebula_ToggleVisited_Template.Visibility = Visibility.Visible;
            }
            else
            {
                datagrid_Nebula_ToggleVisited_Template.Visibility = Visibility.Hidden;
            }
        }
    }
}
