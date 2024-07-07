using AutoMapper;
using AutoMapper.QueryableExtensions;
using Habr.DataAccess;
using Habr.DataAccess.Constraints;
using Habr.DataAccess.DTOs;
using Habr.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Habr.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PostService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PostViewDTO> GetPostView(int id, CancellationToken cancellationToken = default)
        {
            var post = await _context.Posts
                .Where(p => p.Id == id && p.IsPublished == true)
                .ProjectTo<PostViewDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            return post ?? throw new ArgumentException("post not found");
        }
        public async Task<IEnumerable<PublishedPostDTO>> GetPublishedPosts(CancellationToken cancellationToken = default)
        {
            return await _context.Posts
                .Where(p => p.IsPublished)
                .ProjectTo<PublishedPostDTO>(_mapper.ConfigurationProvider)
                .OrderByDescending(p => p.PublishDate)
                .ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<DraftedPostDTO>> GetDraftedPosts(int userId, CancellationToken cancellationToken = default)
        {
            return await _context.Posts
                .Where(p => p.IsPublished == false && p.UserId == userId)
                .ProjectTo<DraftedPostDTO>(_mapper.ConfigurationProvider)
                .OrderByDescending(p => p.UpdatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task AddPost(string title, string text, int userId, bool isPublishedNow = false, CancellationToken cancellationToken = default)
        {
            CheckTitleContraints(title);
            CheckTextContraints(text);

            var post = new Post
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
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task PublishPost(int postId, int userId, CancellationToken cancellationToken = default)
        {
            var post = await GetPostById(postId, cancellationToken);

            CheckAccess(userId, post.UserId);

            post.IsPublished = true;
            post.PublishedDate = DateTime.UtcNow;

            _context.Update(post);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UnpublishPost(int postId, int userId, CancellationToken cancellationToken = default)
        {
            var post = await _context.Posts
                .Where(p => p.Id == postId)
                .SingleOrDefaultAsync(cancellationToken);

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
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdatePost(int postId, int userId, string? newTitle = null, string? newText = null, CancellationToken cancellationToken = default)
        {
            var post = await GetPostById(postId, cancellationToken);

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
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeletePost(int postId, int userId, CancellationToken cancellationToken = default)
        {
            var post = await GetPostById(postId, cancellationToken);

            CheckAccess(userId, post.UserId);

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Checks if User sending the requst owns the post.
        /// </summary>
        /// <exception cref="UnauthorizedAccessException"></exception>
        private void CheckAccess(int userId, int postOwnerId)
        {
            if (userId != postOwnerId)
            {
                throw new UnauthorizedAccessException($"Access denied. User can only modify their own posts");
            }
        }
        private async Task<Post> GetPostById(int postId, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(p => p.Id == postId, cancellationToken);

            return post ?? throw new ArgumentException("The post does not exist");
        }
        private void CheckTitleContraints(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException($"The {nameof(title)} is required.");
            }

            if (title.Length > ConstraintValue.PostTitleMaxLength)
            {
                throw new ArgumentOutOfRangeException($"The {nameof(title)} must be less than {ConstraintValue.PostTitleMaxLength} symbols");
            }
        }
        private void CheckTextContraints(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException($"The {nameof(text)} is required");
            }

            if (text.Length > ConstraintValue.PostTextMaxLength)
            {
                throw new ArgumentOutOfRangeException($"The {nameof(text)} must be less than {ConstraintValue.PostTextMaxLength} symbols");
            }
        }
    }
}
