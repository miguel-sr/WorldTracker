using Microsoft.AspNetCore.Mvc;
using Moq;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IServices;
using WorldTracker.Domain.ValueObjects;
using WorldTracker.Web.Controllers;
using WorldTracker.Web.DTOs;

namespace WorldTracker.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task CreateUser_ShouldCallServiceAndReturnsCreated_WhenCalledWithUserIsValid()
        {
            var dto = new UserCreateDto
            {
                Name = "Test User",
                Email = "test@mail.com",
                Password = "12345678"
            };

            var result = await _controller.CreateUser(dto) as CreatedAtActionResult;
            var value = result?.Value as UserGetDto;

            Assert.NotNull(result);
            Assert.NotNull(value);

            _userServiceMock.Verify(s => s.CreateAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnAllUsers_WhenUsersExist()
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

            _userServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(users);

            var result = await _controller.GetAllUsers() as OkObjectResult;
            var value = result?.Value as IEnumerable<UserGetDto>;

            Assert.NotNull(result);
            Assert.NotNull(value);
            Assert.Equal(2, value.Count());
        }

        [Fact]
        public async Task GetUserById_ShouldReturnUser_WhenUserExists()
        {
            var expectedUser = new User
            {
                Name = "Test User",
                Email = (Email)"test@mail.com",
                Password = (Password)"12345678"
            };

            _userServiceMock
                .Setup(s => s.GetByIdAsync(expectedUser.Id))
                .ReturnsAsync(expectedUser);

            var result = await _controller.GetUserById(expectedUser.Id) as OkObjectResult;
            var value = result?.Value as UserGetDto;

            Assert.NotNull(result);
            Assert.NotNull(value);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            var userId = Guid.NewGuid();

            _userServiceMock.Setup(s => s.GetByIdAsync(userId)).ReturnsAsync((User)null!);

            var result = await _controller.GetUserById(userId) as NotFoundResult;

            Assert.NotNull(result);

            _userServiceMock.Verify(s => s.GetByIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnNoContent_WhenUserExists()
        {
            var userId = Guid.NewGuid();

            var result = await _controller.DeleteUser(userId) as NoContentResult;

            Assert.NotNull(result);

            _userServiceMock.Verify(s => s.GetByIdAsync(userId), Times.Once);
            _userServiceMock.Verify(s => s.DeleteAsync(userId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnNotFound_WhenUserDoesNotExists()
        {
            var userId = Guid.NewGuid();

            var result = await _controller.DeleteUser(userId) as NotFoundResult;

            Assert.NotNull(result);

            _userServiceMock.Verify(s => s.GetByIdAsync(userId), Times.Once);
            _userServiceMock.Verify(s => s.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task AuthenticateUser_ShouldReturnToken_WhenCredentialsAreValid()
        {
            var dto = new AuthDataDto
            {
                Email = "valid@mail.com",
                Password = "12345678"
            };

            var expectedToken = "fake-jwt-token";

            _userServiceMock.Setup(s => s.AuthenticateAsync(dto.Email, dto.Password)).ReturnsAsync(expectedToken);

            var result = await _controller.AuthenticateUser(dto) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(expectedToken, result?.Value);
        }
    }
}
