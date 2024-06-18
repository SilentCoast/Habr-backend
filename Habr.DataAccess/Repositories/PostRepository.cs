using Habr.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Habr.DataAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;

        public PostRepository(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Post> GetPostsQueryable(Expression<Func<Post, bool>>? filter = null, bool includeUser = false, bool includeComments = false)
        {
            var query = _context.Posts.AsQueryable();

            if (includeUser)
            {
                query = query.Include(p => p.User);
            }

            if (includeComments)
            {
                query = query.Include(p => p.Comments).ThenInclude(p => p.Replies);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public async Task AddPostAsync(Post post)
        {
            post.Created = DateTime.UtcNow;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            post.Modified = DateTime.UtcNow;
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}
