namespace WorldTracker.Web.DTOs
{
    public class SyncUserFavoritesDto
    {
        public required string UserId { get; set; }
        public required string[] FavoriteIds { get; set; }
    }
}
