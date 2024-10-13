using System.Text;
using System.Text.Json;
using MassTransit;
using Microsoft.Extensions.Logging;
using Web.Contract;

public class RawWeatherDataConsumer : IConsumer<WeatherData>
{
    private readonly ILogger<RawWeatherDataConsumer> _logger;

    public RawWeatherDataConsumer(ILogger<RawWeatherDataConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<WeatherData> context)
    {
        try
        {
            _logger.LogInformation("Получены данные");
            _logger.LogInformation("Температура: {1} Влажность {2}", context.Message.temperature, context.Message.humidity);
            return Task.CompletedTask;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}