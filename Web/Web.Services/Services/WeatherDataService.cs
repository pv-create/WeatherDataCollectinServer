using Microsoft.Extensions.Logging;
using Web.Services.Contracts;
using Web.Services.Interfaces;
using Web.Services.Models;

namespace Web.Services.Services;

public sealed class WeatherDataService: IWeatherDataService
{
    private readonly IWeatherDataRepository _weatherDataRepository;
    private readonly ILogger<WeatherDataService> _logger;

    public WeatherDataService(
        IWeatherDataRepository weatherDataRepository,
        ILogger<WeatherDataService> logger)
    {
        _weatherDataRepository = weatherDataRepository;
        _logger = logger;
    }

    public Task<Guid> AddWeatherDataAsync(WeatherData weatherData)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(int count)
    {
        _logger.LogInformation("Getting weather data");
        var data = await _weatherDataRepository.GetWeatherDataAsync(count);

        return data;
    }
}