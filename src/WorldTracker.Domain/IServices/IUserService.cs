using WorldTracker.Domain.Entities;

namespace WorldTracker.Domain.IServices
{
    public interface IUserService
    {
        Task CreateUser(User user);

        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserById(Guid id);

        Task<User> GetUserByEmail(string email);

        Task UpdateUser(User user);

        Task DeleteUser(Guid id);

        Task<string> AuthenticateUser(string email, string password);
    }
}
