using Habr.DataAccess;
using Habr.DataAccess.Constraints;
using Habr.DataAccess.DTOs;
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
        public async Task<IEnumerable<PublishedPostDTO>> GetPublishedPostsAsync()
        {
            return await _context.Posts
                .Where(p => p.IsPublished)
                .Select(p => new PublishedPostDTO
                {
                    PostId = p.Id,
                    Title = p.Title,
                    AuthorEmail = p.User.Email,
                    PublishDate = (DateTime)p.PublishedDate
                })
                .OrderByDescending(p => p.PublishDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<DraftedPostDTO>> GetDraftedPostsAsync(int userId)
        {
            return await _context.Posts
                .Where(p => p.IsPublished == false && p.Id == userId)
                .Select(p => new DraftedPostDTO
                {
                    PostId = p.Id,
                    CreatedAt = p.CreatedDate,
                    UpdatedAt = p.ModifiedDate
                })
                .OrderByDescending(p => p.UpdatedAt)
                .ToListAsync();
        }

        public async Task AddPostAsync(string title, string text, int userId, bool isPublishedNow = false)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException($"The {nameof(title)} is required.");
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException($"The {nameof(text)} is required");
            }

            CheckTitleContraints(title);
            CheckTextContraints(text);

            Post post = new Post
            {
                Title = title,
                Text = text,
                UserId = userId,
                CreatedDate = DateTime.UtcNow
            };

            if (isPublishedNow)
            {
                post.IsPublished = true;
                post.PublishedDate = DateTime.UtcNow;
            }

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task PublishPostAsync(int postId, int userId)
        {
            Post post = await GetPostByIdAsync(postId);

            CheckAccess(userId, post.UserId);

            post.IsPublished = true;
            post.PublishedDate = DateTime.UtcNow;

            _context.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task ReturnPostToDraftAsync(int postId, int userId)
        {
            var post = await _context.Posts
                .Where(p => p.Id == postId)
                .SingleOrDefaultAsync();

            if (post == null)
            {
                throw new ArgumentException($"Post with ID {postId} not found");
            }

            CheckAccess(userId, post.UserId);

            if (post.Comments.Where(p => p.IsDeleted == false).Any())
            {
                throw new InvalidOperationException("Cannot draft post containing comments");
            }

            post.IsPublished = false;
            post.PublishedDate = null;

            _context.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(int postId, int userId, string? newTitle = null, string? newText = null)
        {
            Post post = await GetPostByIdAsync(postId);

            CheckAccess(userId, post.UserId);

            if (post.IsPublished)
            {
                throw new InvalidOperationException("Cannot edit published post, draft it first");
            }

            if (newTitle != null)
            {
                CheckTitleContraints(newTitle);
                post.Title = newTitle;
            }

            if (newText != null)
            {
                CheckTextContraints(newText);
                post.Text = newText;
            }

            post.ModifiedDate = DateTime.UtcNow;

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int postId, int userId)
        {
            Post post = await GetPostByIdAsync(postId);

            CheckAccess(userId, post.UserId);

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if User sending the requst owns the post.
        /// </summary>
        private void CheckAccess(int userId, int postOwnerId)
        {
            if (userId != postOwnerId)
            {
                throw new UnauthorizedAccessException($"Access denied. User can only modify their own posts");
            }
        }
        private async Task<Post> GetPostByIdAsync(int postId)
        {
            Post post = await _context.Posts.SingleOrDefaultAsync(p => p.Id == postId);

            if (post == null)
            {
                throw new ArgumentException("The post does not exist");
            }

            return post;
        }
        private void CheckTitleContraints(string title)
        {
            int titleMaxLength = ConstraintValue.PostTitleMaxLength;

            if (title.Length > titleMaxLength)
            {
                throw new ArgumentOutOfRangeException($"The {nameof(title)} must be less than {titleMaxLength} symbols");
            }
        }
        private void CheckTextContraints(string text)
        {
            int textMaxLength = ConstraintValue.PostTextMaxLength;

            if (text.Length > textMaxLength)
            {
                throw new ArgumentOutOfRangeException($"The {nameof(text)} must be less than {textMaxLength} symbols");
            }
        }
    }
}
