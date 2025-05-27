using WorldTracker.Common.DTOs;
using WorldTracker.Domain.Entities;

namespace WorldTracker.Domain.IRepositories
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllAsync();

        Task<PagedResultDto<Country>> GetPagedAsync(PagedRequestDto request);

        Task<IEnumerable<Country>> GetByCodesAsync(string[] codes);

        Task SaveManyAsync(IEnumerable<Country> countries);

        Task<bool> HasAnyAsync();
    }
}
