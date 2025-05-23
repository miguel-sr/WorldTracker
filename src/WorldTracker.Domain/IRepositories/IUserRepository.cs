using WorldTracker.Domain.Entities;

namespace WorldTracker.Domain.IRepositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<User?> GetByEmail(string email);
    }
}
