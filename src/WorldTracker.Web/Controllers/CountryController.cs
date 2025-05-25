using Microsoft.AspNetCore.Mvc;
using WorldTracker.Domain.IServices;

namespace WorldTracker.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CountryController(ICountryService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await service.GetAllCountriesAsync();

            return Ok(countries);
        }
    }
}
