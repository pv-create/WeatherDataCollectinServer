using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services.Contracts;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWeatherDataRepository _weatherDataRepository;

    public HomeController(ILogger<HomeController> logger, IWeatherDataRepository weatherDataRepository)
    {
        _logger = logger;
        _weatherDataRepository = weatherDataRepository;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _weatherDataRepository.GetWeatherDataAsync(10);
        ViewBag.Labels = data.Select(x => x.TimeEvent);
        ViewBag.TemperatureData = data.Select(d => d.TemperatureValue);
        ViewBag.HumidityData = data.Select(d => d.HumidityValue);
        ViewBag.Data = data;
        return View(data);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
