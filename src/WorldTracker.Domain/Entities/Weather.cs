namespace WorldTracker.Domain.Entities
{
    public class Weather
    {
        public required string Description { get; set; }

        public required string Icon { get; set; }

        public double Temperature { get; set; }

        public double FeelsLike { get; set; }

        public int Humidity { get; set; }

        public double WindSpeed { get; set; }
    }
}
