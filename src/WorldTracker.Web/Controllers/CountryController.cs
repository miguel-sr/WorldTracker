using Microsoft.AspNetCore.Mvc;
using WorldTracker.Common.DTOs;
using WorldTracker.Domain.IServices;
using WorldTracker.Web.DTOs;

namespace WorldTracker.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CountryController(ICountryService service, IWeatherService weatherService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCountriesWithWeather()
        {
            var countries = await service.GetAllCountriesAsync();

            var tasks = countries
                .Where(c => c.Coordinates != null)
                .Select(async country =>
                {
                    var weather = await weatherService.GetWeather(country.Coordinates);

                    return CountryWithWeatherDto.Parse(country, weather);
                });

            var countriesWithWeather = await Task.WhenAll(tasks);

            return Ok(countriesWithWeather);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedCountriesWithWeather([FromQuery] int pageSize = 1, [FromQuery] string? paginationToken = null)
        {
            var request = new PagedRequestDto
            {
                Size = pageSize,
                PaginationToken = paginationToken
            };

            var pagedResult = await service.GetPagedCountriesAsync(request);

            var tasks = pagedResult.Items
                .Where(c => c.Coordinates != null)
                .Select(async country =>
                {
                    var weather = await weatherService.GetWeather(country.Coordinates);

                    return CountryWithWeatherDto.Parse(country, weather);
                });

            var countriesWithWeather = await Task.WhenAll(tasks);

            return Ok(new PagedResultDto<CountryWithWeatherDto>
            {
                Items = countriesWithWeather,
                PaginationToken = pagedResult.PaginationToken
            });
        }
    }
}
