using System.Text.Json.Serialization;

namespace WorldTracker.Infra.DTOs
{
    public class WeatherResponseDto
    {
        [JsonPropertyName("main")]
        public required WeatherMetricsDto Metrics { get; set; }

        public required WindDto Wind { get; set; }

        public required List<WeatherDto> Weather { get; set; }

        [JsonIgnore]
        public WeatherDto CurrentWeather => Weather.FirstOrDefault() ?? new();
    }

    public class WeatherMetricsDto
    {
        public double Temp { get; set; }

        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }

        public int Humidity { get; set; }
    }

    public class WeatherDto
    {
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class WindDto
    {
        public double Speed { get; set; }
    }
}
