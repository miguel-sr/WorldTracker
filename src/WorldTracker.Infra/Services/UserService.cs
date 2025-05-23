using WorldTracker.Common;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.Exceptions;
using WorldTracker.Domain.IRepositories;
using WorldTracker.Domain.IServices;

namespace WorldTracker.Infra.Services
{
    public class UserService(IUserRepository repository) : IUserService
    {
        public async Task CreateUser(User user)
        {
            // Check if the e-mail is already in use

            user.Password = PasswordUtils.GenerateHash(user.Password);

            await repository.Create(user);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await repository.GetAll();
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await repository.GetById(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await repository.GetByEmail(email);
        }

        public async Task UpdateUser(User user)
        {
            await repository.Update(user);
        }

        public async Task DeleteUser(Guid id)
        {
            await repository.Delete(id);
        }

        public async Task<string> AuthenticateUser(string email, string password)
        {
            var user = await GetUserByEmail(email);

            if (!PasswordUtils.ValidateHash(password, user.Password))
                throw new InvalidLoginException();

            return TokenService.GenerateToken(user);
        }
    }
}
