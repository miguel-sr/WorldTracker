using WorldTracker.Common.DTOs;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IRepositories;
using WorldTracker.Domain.IServices;

namespace WorldTracker.Application.Services
{
    public class CountryService(ICountryRepository repository, IExternalCountryService externalService) : ICountryService
    {
        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            if (!await repository.HasAnyAsync())
                return await FetchAndSaveCountriesAsync();

            return await repository.GetAllAsync();
        }

        public async Task<PagedResultDto<Country>> GetPagedCountriesAsync(PagedRequestDto request)
        {
            if (!await repository.HasAnyAsync())
                await FetchAndSaveCountriesAsync();

            return await repository.GetPagedAsync(request);
        }

        public async Task<IEnumerable<Country>> GetCountriesByCodesAsync(string[] codes)
        {
            return await repository.GetByCodesAsync(codes);
        }

        private async Task<IEnumerable<Country>> FetchAndSaveCountriesAsync()
        {
            var countries = await externalService.GetCountriesAsync();
            await repository.SaveManyAsync(countries);

            return countries;
        }
    }
}
