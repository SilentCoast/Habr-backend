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

            try
            {
                await _commentRepository.AddCommentAsync(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
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

            try
            {
                await _commentRepository.AddCommentAsync(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task ModifyCommentAsync(string newText, int commentId, int currentUserId)
        {
            Comment comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
            {
                _logger.LogError($"Comment with Id: {commentId} not found");
                return;
            }

            if (!CheckAccess(comment.UserId, currentUserId))
            {
                return;
            }

            comment.Text = newText;

            try
            {
                await _commentRepository.UpdateCommentAsync(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task DeleteCommentAsync(int commentId, int currentUserId)
        {
            Comment comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
            {
                _logger.LogError($"Comment with Id: {commentId} not found");
                return;
            }

            if (!CheckAccess(comment.UserId, currentUserId))
            {
                return;
            }

            comment.IsDeleted = true;
            comment.Text = "Comment deleted";

            try
            {
                await _commentRepository.UpdateCommentAsync(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        /// <summary>
        /// Checks if User sending the requst owns the comment.
        /// </summary>
        private bool CheckAccess(int userId, int commentUserId)
        {
            if (userId != commentUserId)
            {
                _logger.LogError($"Access denied. User can only modify their own comments");
                return false;
            }
            return true;
        }
    }
}
