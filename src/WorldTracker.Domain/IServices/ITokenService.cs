using WorldTracker.Domain.Entities;

namespace WorldTracker.Domain.IServices
{
    public interface ITokenService
    {
        string GenerateToken(User user);

        Task<bool> ValidateToken(string token);
    }
}
