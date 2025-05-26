namespace WorldTracker.Common.DTOs
{
    public class PagedResultDto<T> where T : class
    {
        public IEnumerable<T> Items { get; set; } = [];

        public string? PaginationToken { get; set; }
    }
}
