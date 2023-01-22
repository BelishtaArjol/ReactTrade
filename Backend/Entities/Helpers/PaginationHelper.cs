namespace Entities.Helpers
{
    public class PaginationHelper<T>
    {
        public T Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
