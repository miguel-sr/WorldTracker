using WorldTracker.Domain.Entities;
using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Domain.IRepositories
{
    public interface IUserFavoriteService
    {
        Task CreateAsync(UserFavorite userFavorite);

        Task<IEnumerable<UserFavorite>> GetAllByUserAsync(string userId);

        Task SyncFavoritesAsync(string userId, string[] favorites);

        Task DeleteAsync(string userId, FavoriteId favoriteId);
    }
}
