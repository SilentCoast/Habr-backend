using Habr.DataAccess.DTOs;
using Habr.DataAccess.Enums;

namespace Habr.Services.Interfaces
{
    public interface IPostService
    {
        Task<PostViewDto> GetPostView(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<PublishedPostDto>> GetPublishedPosts(CancellationToken cancellationToken = default);
        Task<IEnumerable<DraftedPostDto>> GetDraftedPosts(int userId, CancellationToken cancellationToken = default);
        Task AddPost(string title, string text, int userId, bool isPublishedNow = false, CancellationToken cancellationToken = default);
        Task PublishPost(int postId, int userId, RoleType role, CancellationToken cancellationToken = default);
        Task UnpublishPost(int postId, int userId, RoleType role, CancellationToken cancellationToken = default);
        Task UpdatePost(int postId, int userId, RoleType role,
            string? newTitle = null, string? newText = null, CancellationToken cancellationToken = default);
        Task DeletePost(int postId, int userId, RoleType role, CancellationToken cancellationToken = default);
    }
}
