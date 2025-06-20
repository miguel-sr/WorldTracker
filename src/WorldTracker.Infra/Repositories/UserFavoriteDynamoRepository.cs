﻿using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IRepositories;
using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Infra.Repositories
{
    public class UserFavoriteDynamoRepository(IDynamoDBContext context) : IUserFavoriteRepository
    {
        public async Task CreateAsync(UserFavorite userFavorite)
        {
            await context.SaveAsync(userFavorite);
        }

        public async Task<IEnumerable<UserFavorite>> GetAllAsync(string userId)
        {
            return await context.ScanAsync<UserFavorite>([new(nameof(UserFavorite.UserId), ScanOperator.Equal, userId)]).GetRemainingAsync();
        }

        public async Task<UserFavorite?> GetByIdAsync(string userId, FavoriteId favoriteId)
        {
            return await context.LoadAsync<UserFavorite?>(userId, favoriteId.ToString());
        }

        public async Task DeleteAsync(string userId, FavoriteId favoriteId)
        {
            await GetByIdAsync(userId, favoriteId);

            await context.DeleteAsync<UserFavorite>(userId, favoriteId.ToString());
        }
    }
}
