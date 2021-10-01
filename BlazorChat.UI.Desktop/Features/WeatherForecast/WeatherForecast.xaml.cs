using System;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;
using BlazorChat.UI.Shared.Features.WeatherForecast;
using ReactiveUI;

namespace BlazorChat.UI.Desktop.Features.WeatherForecast
{
    public partial class WeatherForecast
    {
        public WeatherForecast(WeatherForecastViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;

            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel,
                        vm => vm.Forecasts,
                        v => v.WeatherForecastDataGrid.ItemsSource)
                    .DisposeWith(d);
            });
        }

        private void LoadData(object sender, RoutedEventArgs e) => ViewModel?.LoadForecastsCommand.Execute().Subscribe();
    }
}