using Moq;
using WorldTracker.Application.Services;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.Exceptions;
using WorldTracker.Domain.IRepositories;
using WorldTracker.Domain.IServices;
using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _userService = new UserService(_userRepositoryMock.Object, _tokenServiceMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldCallRepositoryCreateAsync_WhenEmailIsAvailable()
        {
            var user = new User
            {
                Name = "Test User",
                Email = (Email)"test@mail.com",
                Password = (Password)"12345678"
            };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(user.Email))
                .ReturnsAsync((User?)null);

            await _userService.CreateAsync(user);

            _userRepositoryMock.Verify(r => r.GetByEmailAsync(user.Email), Times.Once);
            _userRepositoryMock.Verify(r => r.CreateAsync(user), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowEmailAlreadyInUseException_WhenEmailIsAlreadyInUse()
        {
            var user = new User
            {
                Name = "Test User",
                Email = (Email)"test@mail.com",
                Password = (Password)"12345678"
            };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(user.Email))
                .ReturnsAsync(user);

            await Assert.ThrowsAsync<EmailAlreadyInUseException>(() => _userService.CreateAsync(user));

            _userRepositoryMock.Verify(r => r.GetByEmailAsync(user.Email), Times.Once);
            _userRepositoryMock.Verify(r => r.CreateAsync(user), Times.Never);
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

            _userRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(users);

            var result = await _userService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, u => u.Email == (Email)"user1@mail.com");
            Assert.Contains(result, u => u.Email == (Email)"user2@mail.com");

            _userRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            _userRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync([]);

            var result = await _userService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Empty(result);

            _userRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
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

            _userRepositoryMock
                .Setup(r => r.GetByIdAsync(expectedUser.Id))
                .ReturnsAsync(expectedUser);

            var user = await _userService.GetByIdAsync(expectedUser.Id);

            Assert.NotNull(user);
            Assert.Equal(expectedUser.Id, user.Id);
            Assert.Equal(expectedUser.Email, user.Email);

            _userRepositoryMock.Verify(r => r.GetByIdAsync(expectedUser.Id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowResourceNotFoundException_WhenUserDoesNotExist()
        {
            var userId = Guid.NewGuid();

            _userRepositoryMock
                .Setup(r => r.GetByIdAsync(userId))
                .ReturnsAsync((User?)null);

            await Assert.ThrowsAsync<ResourceNotFoundException>(() => _userService.GetByIdAsync(userId));

            _userRepositoryMock.Verify(r => r.GetByIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldReturnUser_WhenUserExists()
        {
            var expectedUser = new User
            {
                Name = "Test User",
                Email = (Email)"test@mail.com",
                Password = (Password)"12345678"
            };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(expectedUser.Email))
                .ReturnsAsync(expectedUser);

            var user = await _userService.GetByEmailAsync(expectedUser.Email);

            Assert.NotNull(user);
            Assert.Equal(expectedUser.Id, user.Id);
            Assert.Equal(expectedUser.Email, user.Email);

            _userRepositoryMock.Verify(r => r.GetByEmailAsync(expectedUser.Email), Times.Once);
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            var email = "test@mail.com";

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(email))
                .ReturnsAsync((User?)null);

            var user = await _userService.GetByEmailAsync(email);

            Assert.Null(user);

            _userRepositoryMock.Verify(r => r.GetByEmailAsync(email), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallRepositoryUpdate_WhenUserIsProvided()
        {
            var user = new User
            {
                Name = "Test User",
                Email = (Email)"test@mail.com",
                Password = (Password)"newpassword"
            };

            await _userService.UpdateAsync(user);

            _userRepositoryMock.Verify(r => r.UpdateAsync(user), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDelete_WhenUserExists()
        {
            var existingUser = new User
            {
                Name = "Test User",
                Email = (Email)"test@mail.com",
                Password = (Password)"12345678"
            };

            _userRepositoryMock
                .Setup(r => r.GetByIdAsync(existingUser.Id))
                .ReturnsAsync(existingUser);

            _userRepositoryMock
                .Setup(r => r.DeleteAsync(existingUser.Id))
                .Returns(Task.CompletedTask);

            await _userService.DeleteAsync(existingUser.Id);

            _userRepositoryMock.Verify(r => r.GetByIdAsync(existingUser.Id), Times.Once);
            _userRepositoryMock.Verify(r => r.DeleteAsync(existingUser.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowResourceNotFoundException_WhenUserDoesNotExist()
        {
            var userId = Guid.NewGuid();

            _userRepositoryMock
                .Setup(r => r.GetByIdAsync(userId))
                .ReturnsAsync((User?)null);

            await Assert.ThrowsAsync<ResourceNotFoundException>(() => _userService.DeleteAsync(userId));

            _userRepositoryMock.Verify(r => r.GetByIdAsync(userId), Times.Once);
            _userRepositoryMock.Verify(r => r.DeleteAsync(userId), Times.Never);
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldThrowInvalidLoginException_WhenUserDoesNotExistWithEmail()
        {
            var user = new User
            {
                Name = "Test User",
                Email = (Email)"test@mail.com",
                Password = (Password)"12345678"
            };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(user.Email))
                .ReturnsAsync((User?)null);

            await Assert.ThrowsAsync<InvalidLoginException>(() => _userService.AuthenticateAsync("test@mail.com", "12345678"));

            _userRepositoryMock.Verify(r => r.GetByEmailAsync(user.Email), Times.Once);
            _tokenServiceMock.Verify(r => r.GenerateToken(user), Times.Never);
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldThrowInvalidLoginException_WhenPasswordIsIncorrect()
        {
            var existingUser = new User
            {
                Name = "Test User",
                Email = (Email)"test@mail.com",
                Password = (Password)"12345678"
            };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(existingUser.Email))
                .ReturnsAsync(existingUser);

            var wrongPassword = "87654321";

            await Assert.ThrowsAsync<InvalidLoginException>(() => _userService.AuthenticateAsync(existingUser.Email, wrongPassword));

            _userRepositoryMock.Verify(r => r.GetByEmailAsync(existingUser.Email), Times.Once);
            _tokenServiceMock.Verify(r => r.GenerateToken(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnToken_WhenCredentialsAreValid()
        {
            var existingUser = new User
            {
                Name = "Test User",
                Email = (Email)"test@mail.com",
                Password = (Password)"12345678"
            };

            _userRepositoryMock
                .Setup(r => r.GetByEmailAsync(existingUser.Email))
                .ReturnsAsync(existingUser);

            _tokenServiceMock
                .Setup(s => s.GenerateToken(existingUser))
                .Returns("generated_token");

            var token = await _userService.AuthenticateAsync("test@mail.com", "12345678");

            Assert.NotNull(token);
            Assert.Equal("generated_token", token);

            _userRepositoryMock.Verify(r => r.GetByEmailAsync(existingUser.Email), Times.Once);
            _tokenServiceMock.Verify(r => r.GenerateToken(existingUser), Times.Once);
        }
    }
}
