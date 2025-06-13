using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Tests.ValueObjects
{
    public class EmailTests
    {
        [Theory]
        [InlineData("test@mail.com")]
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
            var email = new Email("test@mail.com");

            Assert.Equal("test@mail.com", email.ToString());
        }

        [Fact]
        public void ImplicitConversion_ToString_ShouldReturnEmailValue()
        {
            var email = new Email("test@mail.com");

            string emailString = email;

            Assert.Equal("test@mail.com", emailString);
        }

        [Fact]
        public void ExplicitConversion_FromString_ShouldCreateEmail()
        {
            Email email = (Email)"test@mail.com";

            Assert.Equal("test@mail.com", email.Value);
        }

        [Fact]
        public void OperatorEquals_ShouldReturnTrue_ForSameEmailValue()
        {
            var email1 = new Email("test@mail.com");
            var email2 = new Email("test@mail.com");

            Assert.True(email1 == email2);
            Assert.False(email1 != email2);
        }

        [Fact]
        public void OperatorEquals_ShouldReturnFalse_ForDifferentEmailValues()
        {
            var email1 = new Email("test@mail.com");
            var email2 = new Email("other@mail.com");

            Assert.False(email1 == email2);
            Assert.True(email1 != email2);
        }

        [Fact]
        public void OperatorEquals_ShouldReturnFalse_WhenOneSideIsNull()
        {
            var email1 = new Email("test@mail.com");
            Email? email2 = null;

            Assert.False(email1 == email2);
            Assert.True(email1 != email2);
        }
    }
}
