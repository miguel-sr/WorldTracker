using WorldTracker.Common;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.Exceptions;
using WorldTracker.Domain.IRepositories;
using WorldTracker.Domain.IServices;

namespace WorldTracker.Infra.Services
{
    public class UserService(IUserRepository repository) : IUserService
    {
        public async Task CreateAsync(User user)
        {
            // Check if the e-mail is already in use

            user.Password = PasswordUtils.GenerateHash(user.Password);

            await repository.CreateAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await repository.GetByEmailAsync(email);
        }

        public async Task UpdateAsync(User user)
        {
            user.Password = PasswordUtils.GenerateHash(user.Password);

            await repository.UpdateAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            await repository.DeleteAsync(id);
        }

        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await GetByEmailAsync(email);

            if (user is null || !PasswordUtils.ValidateHash(password, user.Password))
                throw new InvalidLoginException();

            return TokenService.GenerateToken(user);
        }
    }
}
