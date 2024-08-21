using Habr.DataAccess.DTOs;
using Habr.DataAccess.Enums;
using Habr.Services.Pagination;

namespace Habr.Services.Interfaces
{
    public interface IPostService
    {
        Task<PostViewDto> GetPostView(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<PublishedPostDto>> GetPublishedPosts(CancellationToken cancellationToken = default);
        Task<IEnumerable<PublishedPostV2Dto>> GetPublishedPostsV2(CancellationToken cancellationToken = default);
        Task<PaginatedDto<PublishedPostV2Dto>> GetPublishedPostsPaginated(int pageNumber, int pageSize,
            CancellationToken cancellationToken = default);
        Task<DraftedPostViewDto> GetDraftedPostView(int postId, int userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<DraftedPostDto>> GetDraftedPosts(int userId, CancellationToken cancellationToken = default);
        Task<PaginatedDto<DraftedPostDto>> GetDraftedPostsPaginated(int userId, int pageNumber, int pageSize,
            CancellationToken cancellationToken = default);
        Task AddPost(string title, string text, int userId, bool isPublishedNow = false, CancellationToken cancellationToken = default);
        Task PublishPost(int postId, int userId, RoleType role, CancellationToken cancellationToken = default);
        Task UnpublishPost(int postId, int userId, RoleType role, CancellationToken cancellationToken = default);
        Task UpdatePost(int postId, int userId, RoleType role,
            string? newTitle = null, string? newText = null, CancellationToken cancellationToken = default);
        Task DeletePost(int postId, int userId, RoleType role, CancellationToken cancellationToken = default);
    }
}
