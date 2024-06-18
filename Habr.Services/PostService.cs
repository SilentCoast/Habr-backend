using Habr.DataAccess.Entities;
using Habr.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Habr.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<PostService> _logger;
        public PostService(IPostRepository postRepository, ILogger<PostService> logger)
        {
            _postRepository = postRepository;
            _logger = logger;
        }
        public async Task<Post?> GetPostAsync(Expression<Func<Post, bool>>? filter = null, bool includeUser = false, bool includeComments = false)
        {
            return await _postRepository.GetPostsQueryable(filter, includeUser, includeComments).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Post>> GetPostsAsync(Expression<Func<Post, bool>>? filter = null, bool includeUser = false, bool includeComments = false)
        {
            return await _postRepository.GetPostsQueryable(filter, includeUser, includeComments).ToListAsync();
        }

        public async Task AddPostAsync(string title, string text, int createdByUserId)
        {
            Post post = new Post { Title = title, Text = text, UserId = createdByUserId };
                
            await _postRepository.AddPostAsync(post);
        }

        public async Task UpdatePostAsync(int postId, int currentUserId, string? newTitle = null, string? text = null)
        {
            Post post = await GetPostAsync(p => p.Id == postId);

            EnsurePostExists(post, postId);

            CheckAccess(currentUserId, post.UserId);

            if (newTitle != null)
            {
                post.Title = newTitle;
            }

            if (text != null)
            {
                post.Text = text;
            }

            await _postRepository.UpdatePostAsync(post);
        }

        public async Task DeletePostAsync(int postId, int currentUserId)
        {
            Post post = await GetPostAsync(p => p.Id == postId);

            EnsurePostExists(post, postId);

            CheckAccess(currentUserId, post.UserId);

            await _postRepository.DeletePostAsync(post);
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
        private void EnsurePostExists(Post post, int postId)
        {
            if (post == null)
            {
                throw new ArgumentException($"Post with id:{postId} not found");
            }
        }
    }
}
