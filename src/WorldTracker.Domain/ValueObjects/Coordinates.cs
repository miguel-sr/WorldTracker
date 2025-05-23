namespace WorldTracker.Domain.ValueObjects
{
    public class Coordinates
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public const double MaxLatitude = 90;
        public const double MinLatitude = -90;
        public const double MaxLongitude = 180;
        public const double MinLongitude = -180;

        public Coordinates(double latitude, double longitude)
        {
            if (latitude is < MinLatitude or > MaxLatitude)
            throw new ArgumentOutOfRangeException(nameof(latitude), $"Latitude must be between {MinLatitude} and {MaxLatitude}.");

            if (longitude is < MinLongitude or > MaxLongitude)
                throw new ArgumentOutOfRangeException(nameof(longitude), $"Longitude must be between {MinLongitude} and {MaxLongitude}.");

            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString() => $"{Latitude}, {Longitude}";
    }
}
