using Habr.DataAccess.DTOs;

namespace Habr.Services
{
    public interface IPostService
    {
        Task<PostViewDTO> GetPostView(int id);
        Task<IEnumerable<PublishedPostDTO>> GetPublishedPosts();
        Task<IEnumerable<DraftedPostDTO>> GetDraftedPosts(int userId);
        Task AddPost(string title, string text, int userId, bool isPublishedNow = false);
        Task PublishPost(int postId, int userId);
        Task UnpublishPost(int postId, int userId);
        Task UpdatePost(int postId, int userId, string? newTitle = null, string? newText = null);
        Task DeletePost(int postId, int userId);
    }
}
