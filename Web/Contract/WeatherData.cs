namespace Web.Contract;
public record WeatherData
{
    public double? temperature { get; set; }

    public double? humidity { get; set; }
}