using Habr.DataAccess.Entities;
using System.Linq.Expressions;

namespace Habr.DataAccess.Repositories
{
    public interface IPostRepository
    {
        IQueryable<Post> GetPostsQueryable(Expression<Func<Post, bool>>? filter = null, bool includeUser = false, bool includeComments = false);
        Task AddPostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(Post post);
    }
}
