using Web.Services.Models;

namespace Web.Services.Interfaces;

public interface IWeatherDataService
{
    Task<Guid> AddWeatherDataAsync(WeatherData weatherData);
    Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(int count);
}