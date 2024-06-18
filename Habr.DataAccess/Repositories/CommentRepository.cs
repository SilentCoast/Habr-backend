using Habr.DataAccess.Entities;

namespace Habr.DataAccess.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;
        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddCommentAsync(Comment comment)
        {
            comment.Created = DateTime.UtcNow;
            _context.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            comment.Modified = DateTime.UtcNow;
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }
    }
}
