using WorldTracker.Domain.Entities;
using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Domain.IRepositories
{
    public interface IUserFavoriteService
    {
        Task CreateAsync(UserFavorite userFavorite);

        Task<IEnumerable<UserFavorite>> GetAllByUserAsync(string userId);

        Task<UserFavorite?> GetByUserAsync(string userId, FavoriteId favoriteId);

        Task DeleteAsync(string userId, FavoriteId favoriteId);
    }
}
