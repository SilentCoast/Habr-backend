namespace Habr.Services.Pagination
{
    public class PaginatedDto<T>
    {
        public PaginationContext PaginationContext { get; set; }
        public List<T> Items { get; set; }

        public PaginatedDto(List<T> items, int totalCount, int currentPage, int pageSize)
        {
            PaginationContext = new PaginationContext()
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = currentPage,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
            Items = items;
        }
    }
}
