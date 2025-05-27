using Microsoft.AspNetCore.Mvc;
using WorldTracker.Common.DTOs;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IServices;
using WorldTracker.Web.DTOs;

namespace WorldTracker.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CountryWithWeatherController(ICountryService service, IWeatherService weatherService) : ControllerBase
    {

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int pageSize = 1, [FromQuery] string? paginationToken = null, [FromQuery] string? filter = null)
        {
            var request = new PagedRequestDto
            {
                Size = pageSize,
                PaginationToken = paginationToken,
                Filter = filter
            };

            var pagedResult = await service.GetPagedCountriesAsync(request);
            var countriesWithWeather = await GetCountriesWithWeatherDtos(pagedResult.Items);

            return Ok(new PagedResultDto<CountryWithWeatherDto>
            {
                Items = countriesWithWeather,
                PaginationToken = pagedResult.PaginationToken
            });
        }

        [HttpGet("byCodes")]
        public async Task<IActionResult> GetByCodes([FromQuery] string codes)
        {
            if (string.IsNullOrWhiteSpace(codes))
                throw new ArgumentException("Parameter 'codes' is required.");

            var codesArray = codes.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var countries = await service.GetCountriesByCodesAsync(codesArray);
            var countriesWithWeather = await GetCountriesWithWeatherDtos(countries);

            return Ok(countriesWithWeather);
        }

        private async Task<CountryWithWeatherDto[]> GetCountriesWithWeatherDtos(IEnumerable<Country> countries)
        {
            var tasks = countries
                .Where(c => c.Coordinates != null)
                .Select(async country =>
                {
                    var weather = await weatherService.GetWeather(country.Coordinates);
                    return CountryWithWeatherDto.Parse(country, weather);
                });

            var countriesWithWeather = await Task.WhenAll(tasks);

            return [.. countriesWithWeather.OrderBy(c => c.Name)];
        }
    }
}
