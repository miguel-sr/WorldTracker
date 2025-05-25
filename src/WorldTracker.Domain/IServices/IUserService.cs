using WorldTracker.Domain.Entities;

namespace WorldTracker.Domain.IServices
{
    public interface IUserService
    {
        Task CreateAsync(User user);

        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetByIdAsync(Guid id);

        Task<User?> GetByEmailAsync(string email);

        Task UpdateAsync(User user);

        Task DeleteAsync(Guid id);

        Task<string> AuthenticateAsync(string email, string password);
    }
}
