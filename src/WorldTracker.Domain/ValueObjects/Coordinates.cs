using System.Globalization;
using WorldTracker.Common.Extensions;

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

        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "{0}, {1}", Latitude, Longitude);

        private static Coordinates Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Input string cannot be null or empty.");

            var parts = value.Split(", ");

            if (parts.Length != 2 || !parts[0].TryParseInvariant(out double lat) || !parts[1].TryParseInvariant(out double lon))
                throw new FormatException("Invalid coordinate format. Expected format: 'lat, lon'");

            return new Coordinates(lat, lon);
        }

        public static implicit operator string(Coordinates coordinates) => coordinates.ToString();

        public static implicit operator Coordinates(double[] values)
        {
            if (values == null || values.Length < 2)
                return new Coordinates(0, 0);

            return new Coordinates(values[0], values[1]);
        }

        public static explicit operator Coordinates(string value) => Parse(value);
    }
}
