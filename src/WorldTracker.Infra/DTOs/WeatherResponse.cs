namespace WorldTracker.Infra.DTOs
{
    public class WeatherResponse
    {
        public string Name { get; set; }
        public MainDto Main { get; set; }
        public List<WeatherDto> Weather { get; set; }
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
