using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IRepositories;
using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Infra.Services
{
    public class UserFavoriteService(IUserFavoriteRepository repository) : IUserFavoriteService
    {
        public async Task CreateAsync(UserFavorite userFavorite)
        {
            await repository.CreateAsync(userFavorite);
        }

        public async Task<IEnumerable<UserFavorite>> GetAllByUserAsync(string userId)
        {
            return await repository.GetAllAsync(userId);
        }

        public async Task<UserFavorite?> GetByUserAsync(string userId, FavoriteId favoriteId)
        {
            return await repository.GetByIdAsync(userId, favoriteId);
        }

        public async Task DeleteAsync(string userId, FavoriteId favoriteId)
        {
            await repository.DeleteAsync(userId, favoriteId);
        }
    }
}
