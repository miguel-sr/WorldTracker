using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Tests.ValueObjects
{
    public class PasswordTests
    {
        [Fact]
        public void Constructor_WithValidPassword_ShouldCreateHashedPassword()
        {
            var password = new Password("12345678");

            Assert.False(string.IsNullOrWhiteSpace(password.Hash));
            Assert.NotEqual("12345678", password.Hash);
        }

        [Fact]
        public void Constructor_WithHash_ShouldNotRehash()
        {
            var originalHash = new Password("12345678").Hash;

            var passwordFromHash = new Password(originalHash, true);

            Assert.Equal(originalHash, passwordFromHash.Hash);
            Assert.True(passwordFromHash.Matches("12345678"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        [InlineData("short")]
        public void Constructor_WithInvalidPassword_ShouldThrowArgumentException(string invalidPassword)
        {
            Assert.Throws<ArgumentException>(() => new Password(invalidPassword));
        }

        [Fact]
        public void IsMatch_WithCorrectPassword_ShouldReturnTrue()
        {
            var originalPassword = new Password("12345678");

            bool isMatch = originalPassword.Matches("12345678");

            Assert.True(isMatch);
        }

        [Fact]
        public void IsMatch_WithIncorrectPassword_ShouldReturnFalse()
        {
            var originalPassword = new Password("12345678");

            bool isMatch = originalPassword.Matches("87654321");

            Assert.False(isMatch);
        }
    }
}
