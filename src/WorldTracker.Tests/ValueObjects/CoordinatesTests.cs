using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Tests.ValueObjects
{
    public class CoordinatesTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(-90, -180)]
        [InlineData(90, 180)]
        public void Constructor_WithValidCoordinates_ShouldCreateInstance(double latitude, double longitude)
        {
            var coords = new Coordinates(latitude, longitude);

            Assert.Equal(latitude, coords.Latitude);
            Assert.Equal(longitude, coords.Longitude);
        }

        [Theory]
        [InlineData(-91)]
        [InlineData(91)]
        public void Constructor_WithInvalidLatitude_ShouldThrowArgumentOutOfRangeException(double invalidLatitude)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Coordinates(invalidLatitude, 0));
        }

        [Theory]
        [InlineData(-181)]
        [InlineData(181)]
        public void Constructor_WithInvalidLongitude_ShouldThrowArgumentOutOfRangeException(double invalidLongitude)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Coordinates(0, invalidLongitude));
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            var coordinates = new Coordinates(12.34, 56.78);

            Assert.Equal("12.34, 56.78", coordinates.ToString());
        }

        [Theory]
        [InlineData("12.34, 56.78", 12.34, 56.78)]
        [InlineData("-90, -180", -90, -180)]
        [InlineData("90, 180", 90, 180)]
        public void ExplicitConversion_FromValidString_ShouldReturnCoordinates(string input, double expectedLat, double expectedLon)
        {
            Coordinates coordinates = (Coordinates)input;

            Assert.Equal(expectedLat, coordinates.Latitude);
            Assert.Equal(expectedLon, coordinates.Longitude);
        }

        [Theory]
        [InlineData("12.34")]
        [InlineData("12.34,")]
        [InlineData("abc, def")]
        [InlineData("12.34;56.78")]
        public void ExplicitConversion_FromInvalidString_ShouldThrowFormatException(string invalidInput)
        {
            Assert.Throws<FormatException>(() => (Coordinates)invalidInput);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ExplicitConversion_FromNullOrEmptyString_ShouldThrowArgumentException(string invalidInput)
        {
            Assert.Throws<ArgumentException>(() => (Coordinates)invalidInput);
        }

        [Fact]
        public void ImplicitConversion_ToString_ShouldReturnSameAsToString()
        {
            var coordinates = new Coordinates(1, 2);
            string str = coordinates;

            Assert.Equal(coordinates.ToString(), str);
        }

        [Fact]
        public void ImplicitConversion_FromDoubleArray_ShouldCreateCoordinates()
        {
            double[] values = { 10, 20 };
            Coordinates coords = values;

            Assert.Equal(10, coords.Latitude);
            Assert.Equal(20, coords.Longitude);
        }

        [Fact]
        public void ImplicitConversion_FromNullOrShortArray_ShouldCreateDefaultCoordinates()
        {
            Coordinates coordinates = Array.Empty<double>();

            Assert.Equal(0, coordinates.Latitude);
            Assert.Equal(0, coordinates.Longitude);
        }
    }
}
