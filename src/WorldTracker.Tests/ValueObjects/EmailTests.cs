using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Tests.ValueObjects
{
    public class EmailTests
    {
        [Theory]
        [InlineData("teste@example.com")]
        [InlineData("user.name+tag+sorting@example.com")]
        [InlineData("user_name@example.co.uk")]
        [InlineData("user-name@example.org")]
        [InlineData("user.name@sub.domain.com")]
        public void Constructor_WithValidEmail_ShouldCreateEmail(string validEmail)
        {
            var email = new Email(validEmail);

            Assert.False(string.IsNullOrWhiteSpace(email.Value));
            Assert.Equal(validEmail.Trim(), email.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("    ")]
        [InlineData("invalid-email")]
        [InlineData("invalid@")]
        [InlineData("@invalid.com")]
        [InlineData("invalid@com")]
        [InlineData("invalid@.com")]
        public void Constructor_WithInvalidEmail_ShouldThrowArgumentException(string invalidEmail)
        {
            Assert.Throws<ArgumentException>(() => new Email(invalidEmail));
        }

        [Fact]
        public void ToString_ShouldReturnEmailValue()
        {
            var email = new Email("teste@example.com");

            Assert.Equal("teste@example.com", email.ToString());
        }

        [Fact]
        public void ImplicitConversion_ToString_ShouldReturnEmailValue()
        {
            var email = new Email("teste@example.com");

            string emailString = email;

            Assert.Equal("teste@example.com", emailString);
        }

        [Fact]
        public void ExplicitConversion_FromString_ShouldCreateEmail()
        {
            Email email = (Email)"teste@example.com";

            Assert.Equal("teste@example.com", email.Value);
        }
    }
}
