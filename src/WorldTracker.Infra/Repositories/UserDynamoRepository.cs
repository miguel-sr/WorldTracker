using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.Exceptions;
using WorldTracker.Domain.IRepositories;

namespace WorldTracker.Infra.Repositories
{
    public class UserDynamoRepository(IDynamoDBContext context) : IUserRepository
    {
        public async Task CreateAsync(User user)
        {
            await context.SaveAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await context.ScanAsync<User>([]).GetRemainingAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await context.LoadAsync<User>(id);

            if (user is null)
                throw new ResourceNotFoundException(nameof(User), id);

            return user;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return (await context.ScanAsync<User>([new(nameof(User.Email), ScanOperator.Equal, email)]).GetRemainingAsync()).FirstOrDefault();
        }

        public async Task UpdateAsync(User user)
        {
            await context.SaveAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            await GetByIdAsync(id);

            await context.DeleteAsync<User>(id);
        }
    }
}
