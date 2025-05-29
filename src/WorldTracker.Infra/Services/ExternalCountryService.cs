using Microsoft.Extensions.Logging;
using System.Text.Json;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IServices;
using WorldTracker.Infra.DTOs;

namespace WorldTracker.Infra.Services
{
    public class ExternalCountryService : IExternalCountryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExternalCountryService> _logger;

        public ExternalCountryService(HttpClient httpClient, ILogger<ExternalCountryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            var countryDtos = await FetchCountriesAsync();

            return countryDtos.Select(MapToEntity).ToList();
        }

        private async Task<CountryResponseDto[]> FetchCountriesAsync()
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

        private CurrencyInfo? GetCurrencyInfo(CountryResponseDto dto)
        {
            if (dto.PrimaryCurrencyInfo == null)
                return null;

            return new CurrencyInfo
            {
                Name = dto.PrimaryCurrencyInfo.Name,
                Code = dto.PrimaryCurrencyInfo.Code,
                Symbol = dto.PrimaryCurrencyInfo.Symbol
            };
        }
    }
}
