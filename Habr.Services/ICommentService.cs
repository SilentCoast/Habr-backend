namespace Habr.Services
{
    public interface ICommentService
    {
        Task AddComment(string text, int postId, int userId, CancellationToken cancellationToken = default);
        Task ReplyToComment(string text, int parentCommentId, int postId, int userId, CancellationToken cancellationToken = default);
        Task ModifyComment(string newText, int commentId, int userId, CancellationToken cancellationToken = default);
        Task DeleteComment(int commentId, int userId, CancellationToken cancellationToken = default);
    }
}