using WorldTracker.Domain.Entities;

namespace WorldTracker.Domain.IRepositories
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);

        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetByIdAsync(Guid id);

        Task<User?> GetByEmailAsync(string email);

        Task UpdateAsync(User user);

        Task DeleteAsync(Guid id);
    }
}
