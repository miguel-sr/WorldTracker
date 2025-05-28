using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IServices;
using WorldTracker.Web.DTOs;

namespace WorldTracker.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController(IUserService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            await service.CreateAsync(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, new UserGetDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            });
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

        [HttpPost("auth")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthDataDto authDataDto)
        {
            var token = await service.AuthenticateAsync(authDataDto.Email, authDataDto.Password);

            return Ok(token);
        }
    }
}
