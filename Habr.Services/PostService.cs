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

            try
            {
                await _postRepository.AddPostAsync(post);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task UpdatePostAsync(int postId, int currentUserId, string? newTitle = null, string? text = null)
        {
            Post post = await GetPostAsync(p => p.Id == postId);

            if (post == null)
            {
                _logger.LogError($"Post with id:{postId} not found");
                return;
            }

            if (!CheckAccess(currentUserId, post.UserId))
            {
                return;
            }

            if (newTitle != null)
            {
                post.Title = newTitle;
            }

            if (text != null)
            {
                post.Text = text;
            }

            try
            {
                await _postRepository.UpdatePostAsync(post);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task DeletePostAsync(int postId, int currentUserId)
        {
            Post post = await GetPostAsync(p => p.Id == postId);

            if (post == null)
            {
                _logger.LogError($"Post with id:{postId} not found");
                return;
            }

            if (!CheckAccess(currentUserId, post.UserId))
            {
                return;
            }

            try
            {
                await _postRepository.DeletePostAsync(post);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        /// <summary>
        /// Checks if User sending the requst owns the post.
        /// </summary>
        private bool CheckAccess(int userId, int postUserId)
        {
            if (userId != postUserId)
            {
                _logger.LogError($"Access denied. User can only modify their own posts");
                return false;
            }
            return true;
        }
    }
}
