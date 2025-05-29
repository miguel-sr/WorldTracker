using WorldTracker.Domain.Entities;

namespace WorldTracker.Domain.IServices
{
    public interface IExternalCountryService
    {
        Task<IEnumerable<Country>> GetCountriesAsync();
    }
}
