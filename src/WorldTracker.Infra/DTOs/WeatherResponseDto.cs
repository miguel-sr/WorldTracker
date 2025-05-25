namespace WorldTracker.Infra.DTOs
{
    public class WeatherResponseDto
    {
        public required string Name { get; set; }
        public required MainDto Main { get; set; }
        public required List<WeatherDto> Weather { get; set; }
    }

    public class MainDto
    {
        public double Temp { get; set; }
    }

    public class WeatherDto
    {
        public string Description { get; set; }
    }
}
