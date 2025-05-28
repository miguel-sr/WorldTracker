using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorldTracker.Domain.IRepositories;
using WorldTracker.Web.DTOs;

namespace WorldTracker.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserFavoriteController(IUserFavoriteService service) : ControllerBase
    {
        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetAllByUser(string userId)
        {
            var userFavorites = await service.GetAllByUserAsync(userId);

            return Ok(userFavorites.Select(f => f.FavoriteId.ToString()).OrderDescending());
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> SyncFavorites([FromBody] SyncUserFavoritesDto dto)
        {
            await service.SyncFavoritesAsync(dto.UserId, dto.FavoriteIds);

            return NoContent();
        }
    }
}
