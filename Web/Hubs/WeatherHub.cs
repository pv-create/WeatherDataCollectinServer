using Microsoft.AspNetCore.SignalR;
using Web.Contract;

namespace Web.Hubs;

public class WeatherHub : Hub
{
    public async Task SendWeatherData(WeatherData data)
    {
        await Clients.All.SendAsync("ReceiveWeatherData", data);
    }
}