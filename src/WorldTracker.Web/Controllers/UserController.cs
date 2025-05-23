using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IServices;
using WorldTracker.Web.DTOs;

namespace WorldTracker.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController(IUserService service) : AppControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            await service.CreateUser(user);

            return CreatedAtAction(nameof(GetUserById), new UserGetDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            }, dto => dto.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = (await service.GetAllUsers()).Select(user => new UserGetDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            });

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await service.GetUserById(id);

            if (user is null)
                return NotFound();

            return Ok(new UserGetDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            await service.UpdateUser(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await service.DeleteUser(id);

            return NoContent();
        }

        [HttpPost("auth")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthDataDto authDataDto)
        {
            var token = await service.AuthenticateUser(authDataDto.Email, authDataDto.Password);

            return Ok(token);
        }

        [HttpGet("validar")]
        public IActionResult ValidarToken()
        {
            const bool TOKEN_VALIDO = true;

            return Ok(TOKEN_VALIDO);
        }
    }
}
