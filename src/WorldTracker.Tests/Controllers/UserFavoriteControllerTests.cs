using Microsoft.AspNetCore.Mvc;
using Moq;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IServices;
using WorldTracker.Domain.ValueObjects;
using WorldTracker.Web.Controllers;
using WorldTracker.Web.DTOs;

namespace WorldTracker.Tests.Controllers
{
    public class UserFavoriteControllerTests
    {
        private readonly Mock<IUserFavoriteService> _userFavoriteServiceMock;
        private readonly UserFavoriteController _controller;

        public UserFavoriteControllerTests()
        {
            _userFavoriteServiceMock = new Mock<IUserFavoriteService>();
            _controller = new UserFavoriteController(_userFavoriteServiceMock.Object);
        }

        [Fact]
        public async Task GetAllByUser_ShouldReturnsOk_WhenCalledWithValidUserId()
        {
            var userId = Guid.NewGuid().ToString();

            var userFavorites = new List<UserFavorite>
            {
                new() { FavoriteId = (FavoriteId)"country#BRA" },
                new() { FavoriteId = (FavoriteId)"country#CAN" },
            };

            _userFavoriteServiceMock
                .Setup(s => s.GetAllByUserAsync(userId))
                .ReturnsAsync(userFavorites);

            var result = await _controller.GetAllByUser(userId) as OkObjectResult;
            var value = result?.Value as IEnumerable<string>;

            Assert.NotNull(result);
            Assert.NotNull(value);
            Assert.True(value.SequenceEqual(value.OrderByDescending(x => x)));

            _userFavoriteServiceMock.Verify(s => s.GetAllByUserAsync(userId), Times.Once);
        }

        [Fact]
        public async Task SyncFavorites_ShouldCallServiceAndReturnsNoContent_WhenCalledWithValidDto()
        {
            var dto = new SyncUserFavoritesDto
            {
                UserId = "user123",
                FavoriteIds = ["country#BRA", "country#CAN"]
            };

            var result = await _controller.SyncFavorites(dto) as NoContentResult;

            Assert.NotNull(result);

            _userFavoriteServiceMock.Verify(s => s.SyncFavoritesAsync(dto.UserId, dto.FavoriteIds), Times.Once);
        }
    }
}
