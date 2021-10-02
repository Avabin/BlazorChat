using System;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorChat.Shared;
using BlazorChat.Shared.Attributes;
using BlazorChat.UI.Shared.Features.WeatherForecast;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace BlazorChat.UI.Desktop.Features.WeatherForecast
{
    [Service]
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<WeatherForecastService> _logger;
        private readonly string _dataPath = "sample-data/weather.json";

        public WeatherForecastService(IFileProvider fileProvider, ILogger<WeatherForecastService> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
        }
        public async Task<WeatherForecastDto[]?> GetForecastAsync()
        {
            _logger.LogInformation("Fetching weather forecast");

            if (_fileProvider.GetFileInfo(_dataPath) is { Exists: true } file)
            {
                await using var stream = file.CreateReadStream();
                var data = await JsonSerializer.DeserializeAsync<WeatherForecastDto[]>(stream);
                return data;
            }
            return Array.Empty<WeatherForecastDto>(); 
        }
    }
}