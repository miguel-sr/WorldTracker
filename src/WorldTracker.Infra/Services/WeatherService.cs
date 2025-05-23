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
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WeatherService> _logger;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient, ILogger<WeatherService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiKey = Constants.ENV_OPEN_WEATHER_API_KEY.GetRequiredEnvironmentVariable();
        }


        // Create a DTO for the coordinates
        public async Task<Weather> GetWeather(Coordinates coordinates)
        {
            // Set units and lang by user preference 
            var url = $"data/2.5/weather?lat={coordinates.Latitude}&lon={coordinates.Longitude}&units=metric&lang=en&appid={_apiKey}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var dto = JsonSerializer.Deserialize<WeatherResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return new Weather
                {
                    Location = dto.Name,
                    Temperature = dto.Main.Temp,
                    Description = dto.Weather.FirstOrDefault()?.Description
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get weather for coordinates {coordinates}", coordinates.ToString());
                throw;
            }
        }
    }
}
