using Habr.DataAccess;
using Habr.DataAccess.Constraints;
using Habr.DataAccess.Entities;
using Habr.Services.Resources;
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

        public async Task AddComment(string text, int postId, int userId, CancellationToken cancellationToken = default)
        {
            _ = await _context.Posts.SingleOrDefaultAsync(p => p.Id == postId, cancellationToken)
                ?? throw new ArgumentException(ExceptionMessage.PostNotFound);

            CheckTextConstraints(text);

            var comment = new Comment()
            {
                Text = text,
                PostId = postId,
                UserId = userId,
                CreatedDate = DateTime.UtcNow
            };

            _context.Add(comment);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ReplyToComment(string text, int parentCommentId, int postId, int userId, CancellationToken cancellationToken = default)
        {
            _ = await _context.Posts.SingleOrDefaultAsync(p => p.Id == postId, cancellationToken)
                ?? throw new ArgumentException(ExceptionMessage.PostNotFound);

            _ = await _context.Comments.SingleOrDefaultAsync(p => p.Id == parentCommentId, cancellationToken)
                ?? throw new ArgumentException(ExceptionMessage.ParentCommentNotFound);

            CheckTextConstraints(text);

            var comment = new Comment()
            {
                Text = text,
                PostId = postId,
                UserId = userId,
                ParentCommentId = parentCommentId,
                CreatedDate = DateTime.UtcNow
            };

            _context.Add(comment);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ModifyComment(string newText, int commentId, int userId, CancellationToken cancellationToken = default)
        {
            CheckTextConstraints(newText);

            var comment = await _context.Comments.SingleOrDefaultAsync(p => p.Id == commentId, cancellationToken);

            if (comment == null)
            {
                throw new ArgumentException(ExceptionMessage.CommentNotFound);
            }

            if (comment.IsDeleted)
            {
                throw new ArgumentException(ExceptionMessage.CannotEditDeletedComment);
            }

            CheckAccess(comment.UserId, userId);

            comment.Text = newText;

            comment.ModifiedDate = DateTime.UtcNow;

            _context.Comments.Update(comment);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteComment(int commentId, int userId, CancellationToken cancellationToken = default)
        {
            var comment = await _context.Comments.SingleOrDefaultAsync(p => p.Id == commentId, cancellationToken)
                ?? throw new ArgumentException(ExceptionMessage.CommentNotFound);

            CheckAccess(comment.UserId, userId);

            comment.IsDeleted = true;
            comment.Text = "Comment deleted";
            comment.ModifiedDate = DateTime.UtcNow;

            _context.Comments.Update(comment);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Checks if User sending the requst owns the comment.
        /// </summary>
        /// <exception cref="UnauthorizedAccessException"></exception>
        private void CheckAccess(int userId, int commentOwnerId)
        {
            if (userId != commentOwnerId)
            {
                throw new UnauthorizedAccessException(ExceptionMessage.AccessDeniedWrongCommentOwner);
            }
        }
        private void CheckTextConstraints(string text)
        {
            if (text.Length > ConstraintValue.CommentTextMaxLength)
            {
                throw new ArgumentOutOfRangeException(string.Format(ExceptionMessageGeneric.ValueOfMustBeLessThan, nameof(text), ConstraintValue.CommentTextMaxLength));
            }
        }
    }
}
