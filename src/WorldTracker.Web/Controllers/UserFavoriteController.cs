using Microsoft.AspNetCore.Mvc;
using WorldTracker.Domain.Entities;
using WorldTracker.Domain.IRepositories;
using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserFavoriteController(IUserFavoriteService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUserFavorite([FromBody] UserFavorite userFavorite)
        {
            await service.CreateAsync(userFavorite);

            return CreatedAtAction(nameof(GetByUser), new { userId = userFavorite.UserId, favoriteId = userFavorite.FavoriteId }, userFavorite);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllByUser(string userId)
        {
            var userFavorites = await service.GetAllByUserAsync(userId);

            return Ok(userFavorites);
        }

        [HttpGet("{userId}/{favoriteId}")]
        public async Task<IActionResult> GetByUser(string userId, string favoriteId)
        {
            var userFavorites = await service.GetByUserAsync(userId, FavoriteId.Parse(favoriteId));

            return Ok(userFavorites);
        }

        [HttpDelete("{userId}/{favoriteId}")]
        public async Task<IActionResult> DeleteUserFavorite(string userId, string favoriteId)
        {
            await service.DeleteAsync(userId, FavoriteId.Parse(favoriteId));

            return NoContent();
        }
    }
}
