using WorldTracker.Domain.Entities;

namespace WorldTracker.Domain.IRepositories
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllAsync();

        Task SaveManyAsync(IEnumerable<Country> countries);

        Task<bool> HasAnyAsync();
    }
}
