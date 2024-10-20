using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Interfaces;
using Web.Services.Models;

namespace Web.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        private readonly IWeatherDataService _weatherDataService;

        public TemperatureController(IWeatherDataService weatherDataService)
        {
            _weatherDataService = weatherDataService;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<WeatherData>>> GetTemperature()
        {
            var dataResult = await _weatherDataService.GetWeatherDataAsync(10);
            
            return Ok(dataResult);
        }
    }
}
