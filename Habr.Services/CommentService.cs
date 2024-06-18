using Habr.DataAccess;
using Habr.DataAccess.Entities;
using Habr.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace Habr.Services
{
    public class CommentService : ICommentService
    {
        private readonly DataContext _context;
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<CommentService> _logger;

        public CommentService(DataContext context, ICommentRepository commentRepository, ILogger<CommentService> logger)
        {
            _context = context;
            _commentRepository = commentRepository;
            _logger = logger;
        }
        public async Task AddCommentAsync(string text, int postId, int userId)
        {
            Comment comment = new Comment()
            {
                Text = text,
                PostId = postId,
                UserId = userId
            };

            await _commentRepository.AddCommentAsync(comment);
        }

        public async Task ReplyToCommentAsync(string text, int parentCommentId, int postId, int userId)
        {
            Comment comment = new Comment()
            {
                Text = text,
                PostId = postId,
                UserId = userId,
                ParentCommentId = parentCommentId
            };

            await _commentRepository.AddCommentAsync(comment);
        }

        public async Task ModifyCommentAsync(string newText, int commentId, int currentUserId)
        {
            Comment comment = await _context.Comments.FindAsync(commentId);
            
            EnsureCommentExists(comment, commentId);

            CheckAccess(comment.UserId, currentUserId);

            comment.Text = newText;
            
            await _commentRepository.UpdateCommentAsync(comment);
        }

        public async Task DeleteCommentAsync(int commentId, int currentUserId)
        {
            Comment comment = await _context.Comments.FindAsync(commentId);

            EnsureCommentExists(comment, commentId);

            CheckAccess(comment.UserId, currentUserId);

            comment.IsDeleted = true;
            comment.Text = "Comment deleted";

            await _commentRepository.UpdateCommentAsync(comment);
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
        private void EnsureCommentExists(Comment comment, int commentId)
        {
            if (comment == null)
            {
                throw new ArgumentException($"Post with id:{commentId} not found");
            }
        }
    }
}
