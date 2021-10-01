using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorChat.Shared;
using BlazorChat.UI.Shared;
using BlazorChat.UI.Shared.Features.WeatherForecast;

namespace BlazorChat.UI.WebClient.Features.WeatherForecast
{
    public partial class WeatherForecast
    {
        public WeatherForecast() : this(ServiceLocator.Get<WeatherForecastViewModel>())
        {
            
        }

        public WeatherForecast(WeatherForecastViewModel viewModel) => ViewModel = viewModel;

        protected override void OnInitialized() => ViewModel?.LoadForecastsCommand.Execute().Subscribe();
    }
}