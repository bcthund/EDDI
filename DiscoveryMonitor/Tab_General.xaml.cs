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

        private void exobioSlowBios_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        {
            //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
            //configuration.exobiology.reportSlowBios = exobioSlowBios.IsChecked ?? false;
            //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
            //discoveryMonitor()?.Reload();
        }

        private void exobioMinimumBios_Changed ( object sender, TextChangedEventArgs e )
        {
            //try
            //{
            //    var configuration = ConfigService.Instance.discoveryMonitorConfiguration;

            //    int? minimumBios = string.IsNullOrWhiteSpace(exobioMinimumBios.Text) ? 0
            //            : Convert.ToInt32(exobioMinimumBios.Text/*, CultureInfo.InvariantCulture*/);

            //    configuration.exobiology.minimumBiosForReporting = (int)minimumBios;
            //    ConfigService.Instance.discoveryMonitorConfiguration = configuration;
            //    discoveryMonitor()?.Reload();
            //}
            //catch
            //{
            //    // Bad user input; ignore it
            //}
        }

        private void exobioReportDest_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        {
            //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
            //configuration.exobiology.reportDestinationBios = exobioReportDest.IsChecked ?? false;
            //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
            //discoveryMonitor()?.Reload();
        }

        private void exobioSystemReport_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        {
            //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
            //configuration.exobiology.reportBiosInSystemReport = exobioSystemReport.IsChecked ?? false;
            //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
            //discoveryMonitor()?.Reload();
        }

        private void exobioSystemScan_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        {
            //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
            //configuration.exobiology.reportBiosAfterSystemScan = exobioSystemScan.IsChecked ?? false;
            //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
            //discoveryMonitor()?.Reload();
        }

        //private void exobioSoldBreakdown_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.exobiology.dataSold.reportBreakdown = exobioSoldBreakdown.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void exobioSoldReportTotal_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.exobiology.dataSold.reportTotalAlways = exobioSoldReportTotal.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void exobioScansReportBase_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.exobiology.scans.reportBaseValue = exobioScansReportBase.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void exobioScansHumanizeBase_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.exobiology.scans.humanizeBaseValue = exobioScansHumanizeBase.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void exobioScansReportBonus_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.exobiology.scans.reportBonusValue = exobioScansReportBonus.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void exobioScansHumanizeBonus_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.exobiology.scans.humanizeBonusValue = exobioScansHumanizeBonus.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void exobioScansReportLocation_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.exobiology.scans.reportLocation = exobioScansReportLocation.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void exobioScansRecommendBodies_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.exobiology.scans.recommendOtherBios = exobioScansRecommendBodies.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void exobioScansGenusNum_Changed ( object sender, TextChangedEventArgs e )
        //{
        //    //try
        //    //{
        //    //    var configuration = ConfigService.Instance.discoveryMonitorConfiguration;

        //    //    int? genusNum = string.IsNullOrWhiteSpace(exobioScansGenusNum.Text) ? 0
        //    //            : Convert.ToInt32(exobioScansGenusNum.Text/*, CultureInfo.InvariantCulture*/);

        //    //    configuration.exobiology.scans.reportGenusOnScan = (int)genusNum;
        //    //    ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //    discoveryMonitor()?.Reload();
        //    //}
        //    //catch
        //    //{
        //    //    // Bad user input; ignore it
        //    //}
        //}

        //private void exobioScansSpeciesNum_Changed ( object sender, TextChangedEventArgs e )
        //{
        //    //try
        //    //{
        //    //    var configuration = ConfigService.Instance.discoveryMonitorConfiguration;

        //    //    int? speciesNum = string.IsNullOrWhiteSpace(exobioScansSpeciesNum.Text) ? 0
        //    //            : Convert.ToInt32(exobioScansSpeciesNum.Text/*, CultureInfo.InvariantCulture*/);

        //    //    configuration.exobiology.scans.reportSpeciesOnScan = (int)speciesNum;
        //    //    ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //    discoveryMonitor()?.Reload();
        //    //}
        //    //catch
        //    //{
        //    //    // Bad user input; ignore it
        //    //}
        //}

        //private void exobioScansConditionsNum_Changed ( object sender, TextChangedEventArgs e )
        //{
        //    //try
        //    //{
        //    //    var configuration = ConfigService.Instance.discoveryMonitorConfiguration;

        //    //    int? conditionsNum = string.IsNullOrWhiteSpace(exobioScansConditionsNum.Text) ? 0
        //    //            : Convert.ToInt32(exobioScansConditionsNum.Text/*, CultureInfo.InvariantCulture*/);

        //    //    configuration.exobiology.scans.reportConditionsOnScan = (int)conditionsNum;
        //    //    ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //    discoveryMonitor()?.Reload();
        //    //}
        //    //catch
        //    //{
        //    //    // Bad user input; ignore it
        //    //}
        //}

        //// ########################################
        ////      Codex Entries
        //// ########################################
        //private void codexReportAllScans_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.reportAllScans = codexReportAllScans.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexReportNewEntries_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.reportNewEntries = codexReportNewEntries.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexReportNewTraits_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.reportNewTraits = codexReportNewTraits.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexReportVoucherAmount_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.reportVoucherAmounts = codexReportVoucherAmount.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexReportNewOnly_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.reportNewDetailsOnly = codexReportNewOnly.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexReportSystem_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.reportSystem = codexReportSystem.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexReportRegion_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.reportRegion = codexReportRegion.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexAstroEnable_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.astronomicals.enable = codexAstroEnable.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexAstroType_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.astronomicals.reportType = codexAstroType.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexAstroDetails_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.astronomicals.reportDetails = codexAstroDetails.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexBioEnable_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.biologicals.enable = codexBioEnable.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexBioGenus_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.biologicals.reportGenusDetails = codexBioGenus.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexBioSpecies_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.biologicals.reportSpeciesDetails = codexBioSpecies.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexBioConditions_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.biologicals.reportConditions = codexBioConditions.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexGeoEnable_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.geologicals.enable = codexGeoEnable.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexGeoClass_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.geologicals.reportClass = codexGeoClass.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexGeoDetails_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.geologicals.reportDetails = codexGeoDetails.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexGuardianEnable_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.guardian.enable = codexGuardianEnable.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexGuardianDetails_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.guardian.reportDetails = codexGuardianDetails.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexThargoidEnable_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.thargoid.enable = codexThargoidEnable.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}

        //private void codexThargoidDetails_Toggle ( object sender, System.Windows.RoutedEventArgs e )
        //{
        //    //var configuration = ConfigService.Instance.discoveryMonitorConfiguration;
        //    //configuration.codex.thargoid.reportDetails = codexThargoidDetails.IsChecked ?? false;
        //    //ConfigService.Instance.discoveryMonitorConfiguration = configuration;
        //    //discoveryMonitor()?.Reload();
        //}
    }
}
