using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorldTracker.Common;
using WorldTracker.Common.Extensions;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IServices;

namespace WorldTracker.Infra.Services
{
    public class JwtTokenService : ITokenService
    {
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var dadosDoToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(GetSecuritySecret()),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(dadosDoToken);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var parametrosDeValidacao = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(GetSecuritySecret()),
                ValidateLifetime = false,
            };

            return (await tokenHandler.ValidateTokenAsync(token, parametrosDeValidacao)).IsValid;
        }

        public static byte[] GetSecuritySecret()
        {
            return Encoding.ASCII.GetBytes(Constants.ENV_JWT_SECRET.GetRequiredEnvironmentVariable());
        }
    }
}
