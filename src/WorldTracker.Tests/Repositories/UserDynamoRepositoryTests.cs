using Amazon.DynamoDBv2.DataModel;
using Moq;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.ValueObjects;
using WorldTracker.Infra.Repositories;

namespace WorldTracker.Tests.Repositories
{
    public class UserDynamoRepositoryTests
    {
        private readonly Mock<IDynamoDBContext> _contextMock;
        private readonly UserDynamoRepository _repository;

        public UserDynamoRepositoryTests()
        {
            _contextMock = new Mock<IDynamoDBContext>();
            _repository = new UserDynamoRepository(_contextMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldCallSaveAsync_WithGivenUser()
        {
            var user = new User
            {
                Name = "Test User",
                Email = (Email)"test@mail.com",
                Password = (Password)"12345678"
            };

            await _repository.CreateAsync(user);

            _contextMock.Verify(x => x.SaveAsync(user, default), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            var expectedUser = new User
            {
                Name = "Test User",
                Email = (Email)"test@mail.com",
                Password = (Password)"12345678"
            };

            _contextMock.Setup(x => x.LoadAsync<User>(expectedUser.Id, default)).ReturnsAsync(expectedUser);

            var result = await _repository.GetByIdAsync(expectedUser.Id);

            Assert.Equal(expectedUser, result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            var userId = Guid.NewGuid();

            _contextMock.Setup(x => x.LoadAsync<User?>(userId, default)).ReturnsAsync((User?)null);

            var result = await _repository.GetByIdAsync(userId);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldReturnUser_WhenEmailMatches()
        {
            var expectedUser = new User
            {
                Name = "Test User",
                Email = (Email)"test@mail.com",
                Password = (Password)"12345678"
            };

            var users = new List<User> 
            { 
                expectedUser 
            };

            _contextMock
                .Setup(x => x.ScanAsync<User>(It.IsAny<IEnumerable<ScanCondition>>()))
                .Returns(MockAsyncSearch(users));

            var result = await _repository.GetByEmailAsync("user@mail.com");

            Assert.Equal(expectedUser, result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllUsers_WhenUsersExist()
        {
            var users = new List<User>
            {
                new()
                {
                    Name = "User 1",
                    Email = (Email)"user1@mail.com",
                    Password = (Password)"12345678"
                },
                new()
                {
                    Name = "User 2",
                    Email = (Email)"user2@mail.com",
                    Password = (Password)"87654321"
                }
            };

            _contextMock
                .Setup(x => x.ScanAsync<User>(It.IsAny<IEnumerable<ScanCondition>>()))
                .Returns(MockAsyncSearch(users));

            var result = await _repository.GetAllAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallSaveAsync_WhenUserIsProvided()
        {
            var user = new User
            {
                Name = "Test User",
                Email = (Email)"test@mail.com",
                Password = (Password)"12345678"
            };

            await _repository.UpdateAsync(user);

            _contextMock.Verify(x => x.SaveAsync(user, default), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallContextDeleteAsync_WhenUserExists()
        {
            var userId = Guid.NewGuid();

            await _repository.DeleteAsync(userId);

            _contextMock.Verify(x => x.DeleteAsync<User>(userId, default), Times.Once);
        }

        private AsyncSearch<User> MockAsyncSearch(List<User> result)
        {
            var searchMock = new Mock<AsyncSearch<User>>();
            searchMock.Setup(s => s.GetRemainingAsync(default)).ReturnsAsync(result);

            return searchMock.Object;
        }
    }
}
