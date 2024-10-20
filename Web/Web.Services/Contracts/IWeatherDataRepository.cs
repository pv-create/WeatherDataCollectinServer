using Web.Services.Models;

namespace Web.Services.Contracts;

public interface IWeatherDataRepository
{
    Task<Guid> AddWeatherDataAsync(WeatherData weatherData);
    Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(int count);
}