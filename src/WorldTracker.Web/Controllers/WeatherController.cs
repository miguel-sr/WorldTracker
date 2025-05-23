using Microsoft.AspNetCore.Mvc;
using WorldTracker.Domain.IServices;
using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WeatherController(IWeatherService service) : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetWeather([FromQuery] double latitude, [FromQuery] double longitude)
        {
            var weather = await service.GetWeather(new Coordinates(latitude, longitude));

            return Ok(weather);
        }
    }
}
