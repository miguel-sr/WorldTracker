using WorldTracker.Common;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.Exceptions;
using WorldTracker.Domain.IRepositories;
using WorldTracker.Domain.IServices;

namespace WorldTracker.Application.Services
{
    public class UserService(IUserRepository repository, ITokenService tokenService) : IUserService
    {
        public async Task CreateAsync(User user)
        {
            var existingUser = await repository.GetByEmailAsync(user.Email);

            if (existingUser is not null)
                throw new EmailAlreadyInUseException();

            await repository.CreateAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await repository.GetByIdAsync(id);

            if (user is null)
                throw new ResourceNotFoundException(nameof(User), id);

            return user;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await repository.GetByEmailAsync(email);
        }

        public async Task UpdateAsync(User user)
        {
            await repository.UpdateAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await repository.GetByIdAsync(id);
            
            if (user == null)
                throw new ResourceNotFoundException(nameof(User), id);

            await repository.DeleteAsync(id);
        }

        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await GetByEmailAsync(email);

            if (user is null || !PasswordUtils.ValidateHash(password, user.Password))
                throw new InvalidLoginException();

            return tokenService.GenerateToken(user);
        }
    }
}
