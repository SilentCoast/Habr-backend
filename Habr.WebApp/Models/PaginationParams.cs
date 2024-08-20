using Microsoft.AspNetCore.Mvc;

namespace Habr.WebApp.Models
{
    public class PaginationParams
    {
        private const int DefaultPageSize = 15;
        public PaginationParams(int pageNumber, int? pageSize)
        {
            PageSize = pageSize ?? DefaultPageSize;
            PageNumber = pageNumber;
        }

        [FromQuery]
        public int PageNumber { get; set; }
        [FromQuery]
        public int? PageSize { get; set; }
    }
}
