using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.Net;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IRepositories;

namespace WorldTracker.Infra.Repositories
{
    public class UserDynamoRepository(IDynamoDBContext context) : IUserRepository
    {
        public async Task<User> Create(User user)
        {
            await context.SaveAsync(user);

            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await context.ScanAsync<User>([]).GetRemainingAsync();
        }

        public async Task<User?> GetById(Guid id)
        {
            return await context.LoadAsync<User>(id);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return (await context.ScanAsync<User>([new(nameof(User.Email), ScanOperator.Equal, email)]).GetRemainingAsync()).FirstOrDefault();
        }

        public async Task Update(User user)
        {
            await context.SaveAsync(user);
        }

        public async Task Delete(Guid id)
        {
            var user = await GetById(id);

            if (user is null)
                throw new HttpRequestException($"Usuário com ID {id} não encontrado.", null, HttpStatusCode.NotFound);

            await context.DeleteAsync<User>(id);
        }
    }
}
