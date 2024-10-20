using Microsoft.EntityFrameworkCore;
using Web.Services.Contracts;
using Web.Services.Models;

namespace Web.Infrustructure.Repositories;

public class WeatherDataRepository: IWeatherDataRepository
{
    private readonly WeatherDbContext _context;
    
    public WeatherDataRepository(WeatherDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> AddWeatherDataAsync(WeatherData weatherData)
    {
        var result = _context.WeatherDatas.Add(weatherData);
        await _context.SaveChangesAsync();
        
        return result.Entity.Id;
    }

    public async Task<IReadOnlyCollection<WeatherData>> GetWeatherDataAsync(int count)
    {
        var dataResult =
            await _context.WeatherDatas
                .OrderByDescending(e => e.TimeEvent).Take(count).ToListAsync();

        return dataResult;
    }
}