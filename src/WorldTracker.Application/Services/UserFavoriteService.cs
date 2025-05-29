using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IRepositories;
using WorldTracker.Domain.IServices;
using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Application.Services
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

        public async Task SyncFavoritesAsync(string userId, string[] favorites)
        {
            var existingFavorites = await GetAllByUserAsync(userId);
            var existingIds = existingFavorites.Select(f => f.FavoriteId).ToHashSet();

            var incomingIds = favorites
                .Select(FavoriteId.Parse)
                .ToHashSet();

            var idsToAdd = incomingIds.Except(existingIds);
            var addTasks = idsToAdd.Select(id =>
                CreateAsync(new UserFavorite
                {
                    UserId = userId,
                    FavoriteId = id
                }));

            var idsToRemove = existingIds.Except(incomingIds);
            var removeTasks = existingFavorites
                .Where(f => idsToRemove.Contains(f.FavoriteId))
                .Select(f => DeleteAsync(userId, f.FavoriteId));

            await Task.WhenAll(addTasks.Concat(removeTasks));
        }

        public async Task DeleteAsync(string userId, FavoriteId favoriteId)
        {
            await repository.DeleteAsync(userId, favoriteId);
        }
    }
}
