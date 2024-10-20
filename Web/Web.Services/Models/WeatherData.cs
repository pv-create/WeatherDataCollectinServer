namespace Web.Services.Models;

public sealed class WeatherData
{
    public Guid Id { get; set; }
    public double TemperatureValue { get; set; }
    public double HumidityValue { get; set; }
    public DateTime TimeEvent { get; set; }

    public WeatherData(double temperatureValue, double humidityValue)
    {
        Id = Guid.NewGuid();
        TemperatureValue = temperatureValue;
        HumidityValue = humidityValue;
        TimeEvent = DateTime.Now;
    }
}