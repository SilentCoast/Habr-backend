using Habr.DataAccess;
using Habr.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Habr.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _context;
        public PostService(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await _context.Posts.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Post>> GetPostsAsync(Expression<Func<Post, bool>> filter)
        {
            return await _context.Posts.Where(filter).AsNoTracking().ToListAsync();
        }

        public async Task AddPostAsync(string title, string text, int createdByUserId)
        {
            Post post = new Post
            {
                Title = title,
                Text = text,
                UserId = createdByUserId,
                CreatedDate = DateTime.UtcNow
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(int postId, int currentUserId, string? newTitle = null, string? text = null)
        {
            Post post = await _context.Posts.SingleAsync(p => p.Id == postId);

            CheckAccess(currentUserId, post.UserId);

            if (newTitle != null)
            {
                post.Title = newTitle;
            }

            if (text != null)
            {
                post.Text = text;
            }

            post.ModifiedDate = DateTime.UtcNow;

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int postId, int currentUserId)
        {
            Post post = await _context.Posts.SingleAsync(p => p.Id == postId);

            CheckAccess(currentUserId, post.UserId);

            post.ModifiedDate = DateTime.UtcNow;

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if User sending the requst owns the post.
        /// </summary>
        /// <exception cref="UnauthorizedAccessException"></exception>
        private void CheckAccess(int userId, int postUserId)
        {
            if (userId != postUserId)
            {
                throw new UnauthorizedAccessException($"Access denied. User can only modify their own posts");
            }
        }
    }
}
