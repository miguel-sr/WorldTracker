using Moq;
using WorldTracker.Application.Services;
using WorldTracker.Domain.IRepositories;

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

    }
}
