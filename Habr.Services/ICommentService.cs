namespace Habr.Services
{
    public interface ICommentService
    {
        Task AddComment(string text, int postId, int userId);
        Task ReplyToComment(string text, int parentCommentId, int postId, int userId);
        Task ModifyComment(string newText, int commentId, int userId);
        Task DeleteComment(int commentId, int userId);
    }
}