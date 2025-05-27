using WorldTracker.Common.DTOs;
using WorldTracker.Domain.Entities;

namespace WorldTracker.Domain.IServices
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();

        Task<PagedResultDto<Country>> GetPagedCountriesAsync(PagedRequestDto request);

        Task<IEnumerable<Country>> GetCountriesByCodesAsync(string[] codes);
    }
}
