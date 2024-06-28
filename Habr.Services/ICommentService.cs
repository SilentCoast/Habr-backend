namespace Habr.Services
{
    public interface ICommentService
    {
        Task AddCommentAsync(string text, int postId, int userId);
        Task ReplyToCommentAsync(string text, int parentCommentId, int postId, int userId);
        Task ModifyCommentAsync(string newText, int commentId, int userId);
        Task DeleteCommentAsync(int commentId, int userId);
    }
}