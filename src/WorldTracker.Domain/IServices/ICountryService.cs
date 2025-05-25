using WorldTracker.Domain.Entities;

namespace WorldTracker.Domain.IServices
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
    }
}
