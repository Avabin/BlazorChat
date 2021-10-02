using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorChat.Shared;
using BlazorChat.Shared.Attributes;
using BlazorChat.UI.Shared.Features.WeatherForecast;

namespace BlazorChat.UI.WebClient.Features.WeatherForecast
{
    [Service]
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly HttpClient _client;

        public WeatherForecastService(HttpClient client)
        {
            _client = client;
        }
        public Task<WeatherForecastDto[]?> GetForecastAsync() => _client.GetFromJsonAsync<WeatherForecastDto[]>("/sample-data/weather.json");
    }
}