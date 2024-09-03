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


        //private StarSystem currentStarSystem ()
        //{
        //    return (StarSystem)EDDI.Instance?.CurrentStarSystem;
        //}

        public ConfigurationWindow ()
        {
            InitializeComponent();
        }


    }
}
