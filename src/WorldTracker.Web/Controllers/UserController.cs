using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IServices;
using WorldTracker.Domain.ValueObjects;
using WorldTracker.Web.DTOs;

namespace WorldTracker.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController(IUserService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = (Email)dto.Email,
                Password = (Password)dto.Password
            };

            await service.CreateAsync(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, new UserGetDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await service.GetAllAsync();

            var userDtos = users.Select(user => new UserGetDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            });

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await service.GetByIdAsync(id);

            if (user is null)
                return NotFound();

            return Ok(new UserGetDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await service.DeleteAsync(id);

            return NoContent();
        }

        [HttpPost("auth")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthDataDto authDataDto)
        {
            var token = await service.AuthenticateAsync(authDataDto.Email, authDataDto.Password);

            return Ok(token);
        }
    }
}
