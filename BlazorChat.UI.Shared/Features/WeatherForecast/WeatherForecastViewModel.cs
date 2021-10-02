using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reactive;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorChat.Shared;
using BlazorChat.UI.Shared.Features.Navigation;
using DynamicData;
using Microsoft.Extensions.FileProviders;
using ReactiveUI;

namespace BlazorChat.UI.Shared.Features.WeatherForecast
{
    public class WeatherForecastViewModel : RoutableViewModel
    {
        private readonly IWeatherForecastService _weatherForecastService;
        public ObservableCollection<WeatherForecastDto> Forecasts { get; private set; }
        public ReactiveCommand<Unit, Unit> LoadForecastsCommand {get; private set;}

        private readonly string _dataPath = "sample-data/weather.json";
        private readonly IFileProvider _fileProvider;

        public WeatherForecastViewModel(IScreen hostScreen, IWeatherForecastService weatherForecastService) : base(hostScreen)
        {
            _weatherForecastService = weatherForecastService;
            Forecasts = new ObservableCollection<WeatherForecastDto>();
            LoadForecastsCommand = ReactiveCommand.CreateFromTask(LoadForecasts);
        }

        private async Task LoadForecasts()
        {
            var forecasts = await _weatherForecastService.GetForecastAsync();
            if (forecasts is null) return;
            
            Forecasts.Clear();
            Forecasts.AddRange(forecasts);
        }

        public override string? UrlPathSegment { get; } = "weatherforecast";
    }
}