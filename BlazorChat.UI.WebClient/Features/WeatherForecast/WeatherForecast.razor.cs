using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorChat.Shared;
using BlazorChat.UI.Shared;
using BlazorChat.UI.Shared.Features.WeatherForecast;
using Microsoft.AspNetCore.Components;
using ReactiveUI.Blazor;

namespace BlazorChat.UI.WebClient.Features.WeatherForecast
{
    public partial class WeatherForecast
    {
        private readonly NavigationManager _navigationManager;

        public WeatherForecast() : this(ServiceLocator.Get<WeatherForecastViewModel>(), ServiceLocator.Get<NavigationManager>())
        {
            
        }

        public WeatherForecast(WeatherForecastViewModel viewModel, NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            ViewModel = viewModel;
            
        }

        protected override void OnInitialized()
        {
            ViewModel?.LoadForecastsCommand.Execute().Subscribe(_ => StateHasChanged());
        }
    }
}