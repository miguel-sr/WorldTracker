using System.IdentityModel.Tokens.Jwt;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IServices;
using WorldTracker.Domain.ValueObjects;
using WorldTracker.Infra.Services;

namespace WorldTracker.Tests.Services
{
    public class JwtTokenServiceTests
    {
        private readonly ITokenService _tokenService;

        public JwtTokenServiceTests()
        {
            _tokenService = new JwtTokenService();
        }

        [Fact]
        public void GenerateToken_ShouldReturnValidJwtToken()
        {
            var user = new User
            {
                Name = "Test User",
                Email = (Email)"test@mail.com",
                Password = (Password)"12345678"
            };

            var token = _tokenService.GenerateToken(user);

            Assert.NotNull(token);
            Assert.NotEqual(string.Empty, token);

            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            Assert.NotNull(jwtToken);
            Assert.Contains(jwtToken.Claims, c => c.Type == "nameid" && c.Value == user.Id.ToString());
            Assert.Contains(jwtToken.Claims, c => c.Type == "unique_name" && c.Value == user.Name);
            Assert.Contains(jwtToken.Claims, c => c.Type == "email" && c.Value == user.Email);
            Assert.True(jwtToken.ValidTo > DateTime.UtcNow);
        }

        [Fact]
        public async Task ValidateToken_ShouldReturnTrue_WhenTokenIsValid()
        {
            var user = new User
            {
                Name = "Test User",
                Email = (Email)"test@mail.com",
                Password = (Password)"12345678"
            };

            var token = _tokenService.GenerateToken(user);
            var isValid = await _tokenService.ValidateTokenAsync(token);

            Assert.True(isValid);
        }

        [Fact]
        public async Task ValidateToken_ShouldReturnFalse_WhenTokenIsInvalid()
        {
            var invalidToken = "invalid token";
            var isValid = await _tokenService.ValidateTokenAsync(invalidToken);

            Assert.False(isValid);
        }
    }
}
