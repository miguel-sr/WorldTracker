using Moq;
using WorldTracker.Application.Services;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.Exceptions;
using WorldTracker.Domain.IRepositories;
using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Tests.Services
{
    public class UserFavoriteServiceTests
    {
        private readonly Mock<IUserFavoriteRepository> _userFavoriteRepositoryMock;
        private readonly UserFavoriteService _userFavoriteService;

        public UserFavoriteServiceTests()
        {
            _userFavoriteRepositoryMock = new Mock<IUserFavoriteRepository>();
            _userFavoriteService = new UserFavoriteService(_userFavoriteRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldCallRepositoryCreateAsync_WithGivenUserFavorite()
        {
            var userFavorite = new UserFavorite
            {
                UserId = Guid.NewGuid().ToString(),
                FavoriteId = (FavoriteId)"country#BRA"
            };

            _userFavoriteRepositoryMock
                .Setup(r => r.CreateAsync(userFavorite))
                .Returns(Task.CompletedTask);

            await _userFavoriteService.CreateAsync(userFavorite);

            _userFavoriteRepositoryMock.Verify(r => r.CreateAsync(userFavorite), Times.Once);
        }

        [Fact]
        public async Task GetAllByUserAsync_ShouldReturnUserFavorites_WhenUserHasFavorites()
        {
            var userId = Guid.NewGuid().ToString();

            var userFavorites = new List<UserFavorite>
            {
                new()
                {
                    UserId = userId,
                    FavoriteId = (FavoriteId)"country#BRA",
                },
                new()
                {
                    UserId = userId,
                    FavoriteId = (FavoriteId)"country#CAN",
                }
            };

            _userFavoriteRepositoryMock
                .Setup(r => r.GetAllAsync(userId))
                .ReturnsAsync(userFavorites);

            var result = await _userFavoriteService.GetAllByUserAsync(userId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, u => u.FavoriteId == (FavoriteId)"country#BRA");
            Assert.Contains(result, u => u.FavoriteId == (FavoriteId)"country#CAN");

            _userFavoriteRepositoryMock.Verify(r => r.GetAllAsync(userId), Times.Once);
        }

        [Fact]
        public async Task SyncFavoritesAsync_ShouldAddNewFavorites_WhenTheyDontExist()
        {
            var userId = Guid.NewGuid().ToString();

            var existing = new List<UserFavorite>
            {
                new()
                {
                    UserId = userId,
                    FavoriteId = (FavoriteId)"country#BRA"
                }
            };

            var incoming = new[] { "country#BRA", "country#CAN" };

            _userFavoriteRepositoryMock
                .Setup(r => r.GetAllAsync(userId))
                .ReturnsAsync(existing);

            _userFavoriteRepositoryMock
                .Setup(r => r.CreateAsync(It.IsAny<UserFavorite>()))
                .Returns(Task.CompletedTask);

            await _userFavoriteService.SyncFavoritesAsync(userId, incoming);

            _userFavoriteRepositoryMock.Verify(r =>
                r.CreateAsync(It.Is<UserFavorite>(f => f.UserId == userId && f.FavoriteId == (FavoriteId)"country#CAN")), Times.Once);

            _userFavoriteRepositoryMock.Verify(r =>
                r.DeleteAsync(It.IsAny<string>(), It.IsAny<FavoriteId>()), Times.Never);
        }

        [Fact]
        public async Task SyncFavoritesAsync_ShouldRemoveOldFavorites_WhenTheyAreMissingFromIncomingList()
        {
            var userId = Guid.NewGuid().ToString();

            var existing = new List<UserFavorite>
            {
                new()
                {
                    UserId = userId,
                    FavoriteId = (FavoriteId)"country#BRA"
                },
                new()
                {
                    UserId = userId,
                    FavoriteId = (FavoriteId)"country#CAN"
                }
            };

            var incoming = new[] { "country#BRA" };

            _userFavoriteRepositoryMock
                .Setup(r => r.GetAllAsync(userId))
                .ReturnsAsync(existing);

            _userFavoriteRepositoryMock
                .Setup(r => r.DeleteAsync(userId, (FavoriteId)"country#CAN"))
                .Returns(Task.CompletedTask);

            _userFavoriteRepositoryMock
                .Setup(r => r.GetByIdAsync(userId, (FavoriteId)"country#CAN"))
                .ReturnsAsync(new UserFavorite
                {
                    UserId = userId,
                    FavoriteId = (FavoriteId)"country#CAN"
                });

            await _userFavoriteService.SyncFavoritesAsync(userId, incoming);

            _userFavoriteRepositoryMock.Verify(r =>
                r.DeleteAsync(userId, (FavoriteId)"country#CAN"), Times.Once);

            _userFavoriteRepositoryMock.Verify(r =>
                r.CreateAsync(It.IsAny<UserFavorite>()), Times.Never);
        }

        [Fact]
        public async Task SyncFavoritesAsync_ShouldNotAddOrRemove_WhenFavoritesAlreadySynced()
        {
            var userId = Guid.NewGuid().ToString();

            var existing = new List<UserFavorite>
            {
                new()
                {
                    UserId = userId,
                    FavoriteId = (FavoriteId)"country#BRA"
                },
                new()
                {
                    UserId = userId,
                    FavoriteId = (FavoriteId)"country#CAN"
                }
            };

            var incoming = new[] { "country#BRA", "country#CAN" };

            _userFavoriteRepositoryMock
                .Setup(r => r.GetAllAsync(userId))
                .ReturnsAsync(existing);

            await _userFavoriteService.SyncFavoritesAsync(userId, incoming);

            _userFavoriteRepositoryMock.Verify(r =>
                r.CreateAsync(It.IsAny<UserFavorite>()), Times.Never);

            _userFavoriteRepositoryMock.Verify(r =>
                r.DeleteAsync(It.IsAny<string>(), It.IsAny<FavoriteId>()), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDelete_WhenUserFavoriteExists()
        {
            var userId = Guid.NewGuid().ToString();
            var favoriteId = (FavoriteId)"country#BRA";

            var existingUserFavorite = new UserFavorite
            {
                UserId = userId,
                FavoriteId = favoriteId
            };

            _userFavoriteRepositoryMock
                .Setup(r => r.GetByIdAsync(userId, favoriteId))
                .ReturnsAsync(existingUserFavorite);

            _userFavoriteRepositoryMock
                .Setup(r => r.DeleteAsync(userId, favoriteId))
                .Returns(Task.CompletedTask);

            await _userFavoriteService.DeleteAsync(userId, favoriteId);

            _userFavoriteRepositoryMock.Verify(r => r.GetByIdAsync(userId, favoriteId), Times.Once);
            _userFavoriteRepositoryMock.Verify(r => r.DeleteAsync(userId, favoriteId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowResourceNotFoundException_WhenUserFavoriteDoesNotExist()
        {
            var userId = Guid.NewGuid().ToString();
            var favoriteId = (FavoriteId)"country#BRA";

            _userFavoriteRepositoryMock
                .Setup(r => r.GetByIdAsync(userId, favoriteId))
                .ReturnsAsync((UserFavorite?)null);

            await Assert.ThrowsAsync<ResourceNotFoundException>(() => _userFavoriteService.DeleteAsync(userId, favoriteId));

            _userFavoriteRepositoryMock.Verify(r => r.GetByIdAsync(userId, favoriteId), Times.Once);
            _userFavoriteRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<string>(), It.IsAny<FavoriteId>()), Times.Never);
        }
    }
}
