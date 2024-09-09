using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Bouvet.Developer.Survey.Backend.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    // The Web API will only accept tokens 1) for users, and 2) having the "user_impersonation" scope for this API
    static readonly string[] scopeRequiredByApi = new string[] { "user_impersonation" };

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Retrieve weather forecast.
    /// </summary>
    /// <returns>The weather forecast</returns>
    /// <response code="200">Returns the weather forecast</response>
    /// <response code="401">If user is not authorized</response>
    /// <response code="403">If user don't have the scope user_impersonation</response>
    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        var rng = new Random();
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
    }
}