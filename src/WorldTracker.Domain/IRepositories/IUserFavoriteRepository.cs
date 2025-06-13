using WorldTracker.Domain.Entities;
using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Domain.IRepositories
{
    public interface IUserFavoriteRepository
    {
        Task CreateAsync(UserFavorite userFavorite);

        Task<IEnumerable<UserFavorite>> GetAllAsync(string userId);

        Task<UserFavorite?> GetByIdAsync(string userId, FavoriteId favoriteId);

        Task DeleteAsync(string userId, FavoriteId favoriteId);
    }
}
