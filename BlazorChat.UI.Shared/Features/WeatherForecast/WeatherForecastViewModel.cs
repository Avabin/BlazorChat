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
        public ObservableCollection<WeatherForecastDto> Forecasts { get; private set; }
        public ReactiveCommand<Unit, Unit> LoadForecastsCommand {get; private set;}

        private readonly string _dataPath = "sample-data/weather.json";
        private readonly IFileProvider _fileProvider;

        public WeatherForecastViewModel(IScreen hostScreen, IFileProvider fileProvider) : base(hostScreen)
        {
            _fileProvider = fileProvider;
            Forecasts = new ObservableCollection<WeatherForecastDto>();
            LoadForecastsCommand = ReactiveCommand.CreateFromTask(LoadForecasts);
        }

        private async Task LoadForecasts()
        {
            await using var fileStream = _fileProvider.GetFileInfo(_dataPath).CreateReadStream();
            
            var forecasts = await JsonSerializer.DeserializeAsync<WeatherForecastDto[]>(fileStream);
            if (forecasts is null) return;
            
            Forecasts.Clear();
            Forecasts.AddRange(forecasts);
        }

        public override string? UrlPathSegment { get; } = "weatherforecast";
    }
}