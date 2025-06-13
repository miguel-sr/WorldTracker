using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Tests.ValueObjects
{
    public class FavoriteIdTests
    {
        [Fact]
        public void Constructor_WithValidInputs_ShouldCreateInstance()
        {
            var type = "country";
            var value = "BRA";

            var favoriteId = new FavoriteId(type, value);

            Assert.Equal(type, favoriteId.Type);
            Assert.Equal(value, favoriteId.Value);
        }

        [Theory]
        [InlineData(null, "BRA")]
        [InlineData("", "BRA")]
        [InlineData(" ", "BRA")]
        public void Constructor_WithInvalidType_ShouldThrowArgumentException(string invalidType, string value)
        {
            Assert.Throws<ArgumentException>(() => new FavoriteId(invalidType, value));
        }

        [Theory]
        [InlineData("country", null)]
        [InlineData("country", "")]
        [InlineData("country", " ")]
        public void Constructor_WithInvalidValue_ShouldThrowArgumentException(string type, string invalidValue)
        {
            Assert.Throws<ArgumentException>(() => new FavoriteId(type, invalidValue));
        }

        [Fact]
        public void ToString_ShouldReturnExpectedFormat()
        {
            var favoriteId = new FavoriteId("Country", "BRA");

            Assert.Equal("country#BRA", favoriteId.ToString());
        }

        [Fact]
        public void ExplicitConversion_FromValidString_ShouldReturnFavoriteId()
        {
            var input = "country#BRA";
            var expectedType = "country";
            var expectedValue = "BRA";

            FavoriteId favId = (FavoriteId)input;

            Assert.Equal(expectedType, favId.Type);
            Assert.Equal(expectedValue, favId.Value);
        }

        [Theory]
        [InlineData("invalid")]
        [InlineData("")]
        [InlineData("type#")]
        [InlineData("#value")]
        [InlineData("type#value#extra")]
        public void ExplicitConversion_FromInvalidString_ShouldThrowArgumentException(string invalidInput)
        {
            Assert.Throws<ArgumentException>(() => (FavoriteId)invalidInput);
        }

        [Fact]
        public void ImplicitConversion_ToString_ShouldReturnToStringValue()
        {
            var favoriteId = new FavoriteId("country", "BRA");
            string str = favoriteId;

            Assert.Equal("country#BRA", str);
        }

        [Fact]
        public void OperatorEquals_ShouldReturnTrue_ForSameEmailValue()
        {
            var favoriteId1 = new FavoriteId("country", "BRA");
            var favoriteId2 = new FavoriteId("country", "BRA");

            Assert.True(favoriteId1 == favoriteId2);
            Assert.False(favoriteId1 != favoriteId2);
        }

        [Fact]
        public void OperatorEquals_ShouldReturnFalse_ForDifferentEmailValues()
        {
            var favoriteId1 = new FavoriteId("country", "BRA");
            var favoriteId2 = new FavoriteId("country", "CAN");

            Assert.False(favoriteId1 == favoriteId2);
            Assert.True(favoriteId1 != favoriteId2);
        }

        [Fact]
        public void OperatorEquals_ShouldReturnFalse_WhenOneSideIsNull()
        {
            var favoriteId1 = new FavoriteId("country", "BRA");
            FavoriteId? favoriteId2 = null;

            Assert.False(favoriteId1 == favoriteId2);
            Assert.True(favoriteId1 != favoriteId2);
        }
    }
}
