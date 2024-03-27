namespace KamtkSchedule.Domain.Pagination
{
    public record PaginationMetadata
    {
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
    }
}
