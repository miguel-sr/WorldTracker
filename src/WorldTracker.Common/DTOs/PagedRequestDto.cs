namespace WorldTracker.Common.DTOs
{
    public class PagedRequestDto
    {
        public int Size { get; set; } = 10;

        public string? PaginationToken { get; set; }
    }
}
