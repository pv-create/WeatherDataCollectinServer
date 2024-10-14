using System.Text;
using System.Text.Json;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Web.Contract;
using Web.Hubs;

public class RawWeatherDataConsumer : IConsumer<WeatherData>
{
    private readonly ILogger<RawWeatherDataConsumer> _logger;
    private readonly IHubContext<WeatherHub> _hubContext;


    public RawWeatherDataConsumer(
        ILogger<RawWeatherDataConsumer> logger,
        IHubContext<WeatherHub> hubContext)
    {
        _logger = logger;
        _hubContext = hubContext;
    }

    public async Task Consume(ConsumeContext<WeatherData> context)
    {
        try
        {
            _logger.LogInformation("Получены данные");
            _logger.LogInformation("Температура: {1} Влажность {2}", context.Message.temperature, context.Message.humidity);
            await _hubContext.Clients.All.SendAsync("ReceiveWeatherData", context.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}