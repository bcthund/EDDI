﻿using Eddi;
using EddiConfigService;
using EddiCore;
using EddiDataDefinitions;
using EddiNavigationService;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EddiNavigationMonitor
{
    /// <summary>
    /// Interaction logic for RoutePlotterControl.xaml
    /// </summary>
    public partial class PlotCarrierControl : UserControl
    {
        private Task searchTask;

        private string LastCarrierOriginArg;

        private NavigationMonitor navigationMonitor()
        {
            return (NavigationMonitor)EDDI.Instance.ObtainMonitor("Navigation monitor");
        }

        public PlotCarrierControl()
        {
            InitializeComponent();
            plottedRouteData.ItemsSource = navigationMonitor().CarrierPlottedRoute.Waypoints;

            destinationSystemDropDown.Text = string.Empty;
            ClearRouteButton.IsEnabled = navigationMonitor().CarrierPlottedRoute.Waypoints.Count > 0;

            var navConfig = ConfigService.Instance.navigationMonitorConfiguration;
            destinationSystemDropDown.Text = navConfig.carrierDestinationArg;
            carrierNameTextBlock.Text = navigationMonitor().FleetCarrier?.name;
            carrierOriginSystemDropDown.Text = navigationMonitor().FleetCarrier?.currentStarSystem;
            carrierCurrentLoad.Text = navigationMonitor().FleetCarrier?.usedCapacity.ToString() ?? string.Empty;

            EDDI.Instance.PropertyChanged += OnPropertyChange;
            NavigationService.Instance.PropertyChanged += OnNavServiceChange;
            navigationMonitor().CarrierPlottedRoute.PropertyChanged += OnCarrierPlottedRouteChanged;
        }

        private void OnPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FleetCarrier))
            {
                Dispatcher.InvokeAsync( () =>
                {
                    var fleetCarrier = navigationMonitor().FleetCarrier;
                    LastCarrierOriginArg = fleetCarrier?.currentStarSystem;

                    carrierNameTextBlock.Text = !string.IsNullOrEmpty( fleetCarrier?.name )
                        ? $@"{fleetCarrier.name} ({fleetCarrier.callsign})"
                        : fleetCarrier?.callsign ?? Properties.NavigationMonitor
                            .carrier_err_frontier_api_connection_recommended;
                    carrierOriginSystemDropDown.Text = fleetCarrier?.currentStarSystem
                                                       ?? LastCarrierOriginArg
                                                       ?? Properties.NavigationMonitor
                                                           .carrier_err_frontier_api_connection_required;
                    carrierCurrentLoad.Text = ( fleetCarrier?.usedCapacity ?? 0 ).ToString();

                    if ( string.IsNullOrEmpty( fleetCarrier?.currentStarSystem ) )
                    {
                        SearchButton.IsEnabled = false;
                    }

                    SearchButton.IsEnabled = true;
                } );
            }
        }

        private void OnCarrierPlottedRouteChanged(object sender, PropertyChangedEventArgs e)
        {
            if ( !( sender is NavWaypointCollection navWaypointCollection ) )
            {
                return;
            }

            switch (e.PropertyName)
            {
                case nameof(NavWaypointCollection.Waypoints):
                {
                    Dispatcher.InvokeAsync( () =>
                    {
                        ClearRouteButton.IsEnabled = navWaypointCollection.Waypoints.Count > 0;
                    } );
                    break;
                }
            }
        }

        private void OnNavServiceChange(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(NavigationService.Instance.IsWorking):
                {
                    if ( NavigationService.Instance.IsWorking )
                    {
                        Dispatcher.InvokeAsync( () =>
                        {
                            SearchProgressBar.Visibility = Visibility.Visible;
                        } );
                    }
                    else
                    {
                        Dispatcher.InvokeAsync( () =>
                        {
                            SearchProgressBar.Visibility = Visibility.Collapsed;
                        } );
                    }
                    break;
                }
            }
        }

        private async void executeSearch(object sender, RoutedEventArgs e)
        {
            var originSystemArg = carrierOriginSystemDropDown.Text;
            var destinationSystemArg = destinationSystemDropDown.Text;
            var usedCapacity = Convert.ToInt32(carrierCurrentLoad.Text, CultureInfo.InvariantCulture);
            if (searchTask?.Status == TaskStatus.Running)
            { }
            else
            {
                searchTask = Task.Run(() =>
                {
                    var @event = NavigationService.Instance.NavQuery(QueryType.carrier, destinationSystemArg, originSystemArg, usedCapacity, null, true);
                    if (@event == null) { return; }
                    EDDI.Instance?.enqueueEvent(@event);
                });
            }
            await Task.WhenAll(searchTask);
        }

        private void DestinationSystemText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ( !( sender is StarSystemComboBox starSystemComboBox ) || !starSystemComboBox.IsLoaded ) { return; }
            destinationSystemDropDown.TextDidChange( sender, e, NavigationService.Instance.LastCarrierDestinationArg, null );
            UpdateSearchButtonEnabled();
        }

        private void DestinationSystemText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ( !( sender is StarSystemComboBox starSystemComboBox ) || !starSystemComboBox.IsLoaded ) { return; }

            void changeHandler (string newValue)
            {
                // Update to new destination system
                NavigationService.Instance.LastCarrierDestinationArg = newValue;
                UpdateSearchButtonEnabled();
            }
            destinationSystemDropDown.SelectionDidChange(changeHandler);
        }

        private void DestinationSystemText_LostFocus(object sender, RoutedEventArgs e)
        {
            destinationSystemDropDown.DidLoseFocus(NavigationService.Instance.LastCarrierDestinationArg);
            UpdateSearchButtonEnabled();
        }

        private void OriginSystemText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ( !(sender is StarSystemComboBox starSystemComboBox) || !starSystemComboBox.IsLoaded ) { return; }
            carrierOriginSystemDropDown.TextDidChange( sender, e, LastCarrierOriginArg, null );
            UpdateSearchButtonEnabled();
        }

        private void OriginSystemText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ( !( sender is StarSystemComboBox starSystemComboBox ) || !starSystemComboBox.IsLoaded ) { return; }

            void changeHandler (string newValue)
            {
                // Update to new origin system
                LastCarrierOriginArg = newValue;

                UpdateSearchButtonEnabled();
            }
            carrierOriginSystemDropDown.SelectionDidChange(changeHandler);
        }

        private void OriginSystemText_LostFocus(object sender, RoutedEventArgs e)
        {
            carrierOriginSystemDropDown.DidLoseFocus( LastCarrierOriginArg ?? 
                                                      navigationMonitor().FleetCarrier?.currentStarSystem );
            UpdateSearchButtonEnabled();
        }

        private void carrierCurrentLoad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                carrierLoad_Changed();
            }
        }

        private void carrierCurrentLoad_LostFocus(object sender, RoutedEventArgs e)
        {
            carrierLoad_Changed();
        }

        private void carrierLoad_Changed()
        {
            try
            {
                var navConfig = ConfigService.Instance.navigationMonitorConfiguration;
                int? distance = string.IsNullOrWhiteSpace(carrierCurrentLoad.Text)
                    ? 10000 : Convert.ToInt32(carrierCurrentLoad.Text, CultureInfo.InvariantCulture);
                if (distance != navConfig.maxSearchDistanceFromStarLs)
                {
                    navConfig.maxSearchDistanceFromStarLs = distance;
                    navigationMonitor().WriteNavConfig();
                }
                UpdateSearchButtonEnabled();
            }
            catch
            {
                // Bad user input; ignore it
            }
        }

        private void UpdateSearchButtonEnabled()
        {
            SearchButton.IsEnabled = !string.IsNullOrEmpty(carrierOriginSystemDropDown.Text)
                                     && !string.IsNullOrEmpty(destinationSystemDropDown.Text)
                                     && !string.IsNullOrEmpty(carrierCurrentLoad.Text);
        }

        private void EnsureValidInteger(object sender, TextCompositionEventArgs e)
        {
            // Match valid characters
            Regex regex = new Regex(@"[0-9]");
            // Swallow the character doesn't match the regex
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()).ToString();
        }

        private void ClearRouteButton_Click(object sender, RoutedEventArgs e)
        {
            if (plottedRouteData.Items.Count > 0)
            {
                navigationMonitor().CarrierPlottedRoute.Waypoints.Clear();
                navigationMonitor().WriteNavConfig();
            }
        }

        private void addBookmark(object sender, RoutedEventArgs e)
        {
            if (Parent is TabItem parentTab && parentTab.Parent is TabControl parentTabControl)
            {
                if (parentTabControl.Parent is DockPanel dockPanel)
                {
                    if (dockPanel.Parent is ConfigurationWindow configurationWindow)
                    {
                        configurationWindow.SwitchToTab(Properties.NavigationMonitor.tab_bookmarks);
                        configurationWindow.addBookmark(sender, e);
                    }
                }
            }
        }

        private void copySystemNameToClipboard(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.DataContext is NavWaypoint navWaypoint)
                {
                    Clipboard.SetText(navWaypoint.systemName);
                }
            }
        }
    }
}
