using System.Threading.Tasks;
using BlazorChat.Shared;

namespace BlazorChat.UI.Shared.Features.WeatherForecast
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecastDto[]?> GetForecastAsync();
    }
}