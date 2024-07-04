using Habr.DataAccess.DTOs;
using Habr.DataAccess.Entities;
using System.Linq.Expressions;

namespace Habr.Services
{
    public interface IPostService
    {
        Task<PostViewDTO> GetPostViewAsync(int id);
        Task<IEnumerable<PublishedPostDTO>> GetPublishedPostsAsync();
        Task<IEnumerable<DraftedPostDTO>> GetDraftedPostsAsync(int userId);
        Task AddPostAsync(string title, string text, int userId, bool isPublishedNow = false);
        Task PublishPostAsync(int postId, int userId);
        Task UnpublishPostAsync(int postId, int userId);
        Task UpdatePostAsync(int postId, int userId, string? newTitle = null, string? newText = null);
        Task DeletePostAsync(int postId, int userId);
    }
}
