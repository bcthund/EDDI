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
    /// Interaction logic for Tab_Tools.xaml
    /// </summary>
    public partial class Tab_Tools : UserControl
    {
        public Tab_Tools ()
        {
            InitializeComponent();
        }

        private void EnsureValidInteger(object sender, TextCompositionEventArgs e)
        {
            // No Negative Values
            Regex regex = new Regex(@"[0-9]");  // Match valid characters
            e.Handled = !regex.IsMatch(e.Text); // Swallow the character doesn't match the regex
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


    }
}
