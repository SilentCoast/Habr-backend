using Habr.DataAccess.Entities;
using System.Linq.Expressions;

namespace Habr.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPostsAsync();
        Task<IEnumerable<Post>> GetPostsAsync(Expression<Func<Post, bool>> filter);
        Task AddPostAsync(string title, string text, int createdByUserId);
        Task UpdatePostAsync(int postId, int currentUserId, string? newTitle = null, string? text = null);
        Task DeletePostAsync(int postId, int currentUserId);
    }
}
