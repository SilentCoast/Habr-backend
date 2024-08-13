using Habr.DataAccess;
using Habr.DataAccess.Constraints;
using Habr.DataAccess.Entities;
using Habr.DataAccess.Enums;
using Habr.Services.Interfaces;
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
                CreatedAt = DateTime.UtcNow
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
                CreatedAt = DateTime.UtcNow
            };

            _context.Add(comment);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task EditComment(string newText, int commentId, int userId,
            RoleType role, CancellationToken cancellationToken = default)
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

            AccessHelper.CheckCommentAccess(comment.UserId, userId, role);

            comment.Text = newText;

            comment.ModifiedAt = DateTime.UtcNow;

            _context.Comments.Update(comment);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteComment(int commentId, int userId, RoleType role, CancellationToken cancellationToken = default)
        {
            var comment = await _context.Comments.SingleOrDefaultAsync(p => p.Id == commentId, cancellationToken)
                ?? throw new ArgumentException(ExceptionMessage.CommentNotFound);

            AccessHelper.CheckCommentAccess(comment.UserId, userId, role);

            comment.IsDeleted = true;
            comment.Text = "Comment deleted";
            comment.ModifiedAt = DateTime.UtcNow;

            _context.Comments.Update(comment);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private static void CheckTextConstraints(string text)
        {
            if (text.Length > ConstraintValue.CommentTextMaxLength)
            {
                throw new ArgumentOutOfRangeException(string.Format(ExceptionMessageGeneric.ValueOfMustBeLessThan, nameof(text), ConstraintValue.CommentTextMaxLength));
            }
        }
    }
}
