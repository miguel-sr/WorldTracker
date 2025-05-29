using Microsoft.Extensions.Logging;
using System.Text.Json;
using WorldTracker.Common;
using WorldTracker.Common.Extensions;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IServices;
using WorldTracker.Domain.ValueObjects;
using WorldTracker.Infra.DTOs;

namespace WorldTracker.Infra.Services
{
    public class ExternalWeatherService : IExternalWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExternalWeatherService> _logger;
        private readonly string _apiKey;

        public ExternalWeatherService(HttpClient httpClient, ILogger<ExternalWeatherService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiKey = Constants.ENV_OPEN_WEATHER_API_KEY.GetRequiredEnvironmentVariable();
        }

        public async Task<Weather> GetWeather(Coordinates coordinates)
        {
            var url = $"data/2.5/weather?lat={coordinates.Latitude}&lon={coordinates.Longitude}&units=metric&lang=pt_br&appid={_apiKey}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var dto = JsonSerializer.Deserialize<WeatherResponseDto>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return new Weather
                {
                    Description = dto.CurrentWeather.Description,
                    Icon = dto.CurrentWeather.Icon,
                    Temperature = dto.Metrics.Temp,
                    FeelsLike = dto.Metrics.Temp,
                    Humidity = dto.Metrics.Humidity,
                    WindSpeed = dto.Wind.Speed
                };
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request to fetch weather for coordinates {coordinates} failed.", coordinates.ToString());
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Failed to deserialize weather data.");
                throw;
            }
        }
    }
}
