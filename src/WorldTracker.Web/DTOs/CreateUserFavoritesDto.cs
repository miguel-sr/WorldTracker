namespace WorldTracker.Web.DTOs
{
    public class CreateUserFavoritesDto
    {
        public required string UserId { get; set; }

        public required string[] FavoriteIds { get; set; }
    }
}
