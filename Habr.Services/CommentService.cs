using Habr.DataAccess;
using Habr.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Habr.Services
{
    public class CommentService : ICommentService
    {
        private readonly DataContext _context;

        public CommentService(DataContext context)
        {
            _context = context;
        }
        public async Task AddCommentAsync(string text, int postId, int userId)
        {
            Comment comment = new Comment()
            {
                Text = text,
                PostId = postId,
                UserId = userId,
                CreatedDate = DateTime.UtcNow
            };

            _context.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task ReplyToCommentAsync(string text, int parentCommentId, int postId, int userId)
        {
            Comment comment = new Comment()
            {
                Text = text,
                PostId = postId,
                UserId = userId,
                ParentCommentId = parentCommentId,
                CreatedDate = DateTime.UtcNow
            };

            _context.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task ModifyCommentAsync(string newText, int commentId, int currentUserId)
        {
            Comment comment = await _context.Comments.SingleAsync(p => p.Id == commentId);
            
            CheckAccess(comment.UserId, currentUserId);

            comment.Text = newText;

            comment.ModifiedDate = DateTime.UtcNow;

            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int commentId, int currentUserId)
        {
            Comment comment = await _context.Comments.SingleAsync(p => p.Id == commentId);

            CheckAccess(comment.UserId, currentUserId);

            comment.IsDeleted = true;
            comment.Text = "Comment deleted";
            comment.ModifiedDate = DateTime.UtcNow;

            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if User sending the requst owns the comment.
        /// </summary>
        private void CheckAccess(int userId, int commentUserId)
        {
            if (userId != commentUserId)
            {
                throw new UnauthorizedAccessException($"Access denied. User can only modify their own comments");
            }
        }
    }
}
