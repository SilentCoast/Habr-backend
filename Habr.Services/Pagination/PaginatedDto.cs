using Habr.Services.Exceptions;
using Habr.Services.Resources;

namespace Habr.Services.Pagination
{
    public class PaginatedDto<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }

        public PaginatedDto(List<T> items, int totalCount, int currentPage, int pageSize)
        {
            if (totalCount == 0)
            {
                throw new NothingToPaginateException();
            }

            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            if (currentPage > TotalPages)
            {
                throw new ArgumentException(ExceptionMessage.CurrentPageMoreThanLast);
            }

            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            Items = items;
        }
    }
}
