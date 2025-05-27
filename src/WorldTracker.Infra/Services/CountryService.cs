using Microsoft.Extensions.Logging;
using System.Text.Json;
using WorldTracker.Common.DTOs;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IRepositories;
using WorldTracker.Domain.IServices;
using WorldTracker.Infra.DTOs;

namespace WorldTracker.Infra.Services
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CountryService> _logger;
        private readonly ICountryRepository _countryRepository;

        public CountryService(HttpClient httpClient, ILogger<CountryService> logger, ICountryRepository countryRepository)
        {
            _httpClient = httpClient;
            _logger = logger;
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            if (await _countryRepository.HasAnyAsync())
                return await _countryRepository.GetAllAsync();

            var countryDtos = await GetCountriesAsync();
            var countries = countryDtos.Select(MapToEntity).ToList();

            await _countryRepository.SaveManyAsync(countries);

            return countries;
        }

        public async Task<PagedResultDto<Country>> GetPagedCountriesAsync(PagedRequestDto request)
        {
            if (await _countryRepository.HasAnyAsync())
                return await _countryRepository.GetPagedAsync(request);

            var countryDtos = await GetCountriesAsync();
            var countries = countryDtos.Select(MapToEntity).ToList();

            await _countryRepository.SaveManyAsync(countries);

            return await _countryRepository.GetPagedAsync(request);
        }

        public async Task<IEnumerable<Country>> GetCountriesByCodesAsync(string[] codes)
        {
            return await _countryRepository.GetByCodesAsync(codes);
        }

        private async Task<CountryResponseDto[]> GetCountriesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("all");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<CountryResponseDto[]>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? [];
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request to fetch countries failed.");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Failed to deserialize country data.");
                throw;
            }
        }

        private Country MapToEntity(CountryResponseDto dto)
        {
            return new Country
            {
                Name = dto.Name.Common,
                NameLower = dto.Name.Common.ToLowerInvariant(),
                Code = dto.IsoAlpha3Code,
                Region = dto.Region,
                Subregion = dto.Subregion,
                Languages = dto.Languages,
                Population = dto.Population,
                CurrencyInfo = GetCurrencyInfo(dto),
                Coordinates = dto.Coordinates,
                Flag = new Flag
                {
                    Url = dto.Flags.Svg,
                    AltText = dto.Flags.Alt
                }
            };
        }

        private CurrencyInfo? GetCurrencyInfo(CountryResponseDto country)
        {
            if (country.PrimaryCurrencyInfo == null)
                return null;

            return new CurrencyInfo
            {
                Name = country.PrimaryCurrencyInfo.Name,
                Code = country.PrimaryCurrencyInfo.Code,
                Symbol = country.PrimaryCurrencyInfo.Symbol
            };
        }
    }
}
