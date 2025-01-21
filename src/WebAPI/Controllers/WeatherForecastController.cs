using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger = logger;

    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {
        return Ok("good job!");
    }
}
