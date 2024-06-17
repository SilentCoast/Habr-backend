using Habr.DataAccess.Entities;
using System.Linq.Expressions;

namespace Habr.Services
{
    public interface IPostService
    {
        Task<Post?> GetPostAsync(Expression<Func<Post, bool>>? filter = null, bool includeUser = false, bool includeComments = false);
        Task<IEnumerable<Post>> GetPostsAsync(Expression<Func<Post, bool>>? filter = null, bool includeUser = false, bool includeComments = false);
        Task AddPostAsync(string title, string text, int createdByUserId);
        Task UpdatePostAsync(int postId, int currentUserId, string? newTitle = null, string? text = null);
        Task DeletePostAsync(int postId, int currentUserId);
    }
}
