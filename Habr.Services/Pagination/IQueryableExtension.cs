using Microsoft.EntityFrameworkCore;

namespace Habr.Services.Pagination
{
    public static class IQueryableExtension
    {
        public static async Task<PaginatedDto<T>> ToPaginatedDto<T>(
            this IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var totalCount = await source.CountAsync(cancellationToken);
            var items = await source.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync(cancellationToken);

            return new PaginatedDto<T>(items, totalCount, pageNumber, pageSize);
        }
    }
}
