using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Web.Contract;
using Web.Hubs;
using Web.Services.Contracts;
using Web.Services.Models;

public class RawWeatherDataConsumer : IConsumer<WeatherDataContract>
{
    private readonly IWeatherDataRepository _repository;
    private readonly ILogger<RawWeatherDataConsumer> _logger;
    private readonly IHubContext<WeatherHub> _hubContext;


    public RawWeatherDataConsumer(
        ILogger<RawWeatherDataConsumer> logger,
        IHubContext<WeatherHub> hubContext,
        IWeatherDataRepository repository)
    {
        _logger = logger;
        _hubContext = hubContext;
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<WeatherDataContract> context)
    {
        try
        {
            _logger.LogInformation("Получены данные");
            _logger.LogInformation("Температура: {1} Влажность {2}", context.Message.temperature, context.Message.humidity);
            WeatherData newWeatherData = new(context.Message.temperature, context.Message.humidity);
            await _repository.AddWeatherDataAsync(newWeatherData);
            await _hubContext.Clients.All.SendAsync("ReceiveWeatherData", context.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}