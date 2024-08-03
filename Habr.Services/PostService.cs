using AutoMapper;
using AutoMapper.QueryableExtensions;
using Habr.DataAccess;
using Habr.DataAccess.Constraints;
using Habr.DataAccess.DTOs;
using Habr.DataAccess.Entities;
using Habr.DataAccess.Enums;
using Habr.Services.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Habr.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;

        public PostService(DataContext context, IMapper mapper, ILogger<PostService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PostViewDTO> GetPostView(int id, CancellationToken cancellationToken = default)
        {
            var post = await _context.Posts
                .Where(p => p.Id == id && p.IsPublished == true)
                .ProjectTo<PostViewDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            return post ?? throw new ArgumentException(ExceptionMessage.PostNotFound);
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

        public async Task AddPost(string title, string text, int userId,
            bool isPublishedNow = false, CancellationToken cancellationToken = default)
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

        public async Task PublishPost(int postId, int userId, RoleType role, CancellationToken cancellationToken = default)
        {
            var post = await GetPostById(postId, cancellationToken);

            AccessController.CheckPostAccess(userId, post.UserId, role);

            post.IsPublished = true;
            post.PublishedDate = DateTime.UtcNow;

            _context.Update(post);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Post (id:{postId}) published");
        }

        public async Task UnpublishPost(int postId, int userId, RoleType role, CancellationToken cancellationToken = default)
        {
            var post = await _context.Posts
                .Where(p => p.Id == postId)
                .SingleOrDefaultAsync(cancellationToken);

            if (post == null)
            {
                throw new ArgumentException(ExceptionMessage.PostNotFound);
            }

            AccessController.CheckPostAccess(userId, post.UserId, role);

            if (post.Comments.Where(p => p.IsDeleted == false).Any())
            {
                throw new InvalidOperationException(ExceptionMessage.CannotDraftPostWithComments);
            }

            post.IsPublished = false;
            post.PublishedDate = null;

            _context.Update(post);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Post (id:{postId}) drafted");
        }

        public async Task UpdatePost(int postId, int userId, RoleType role,
            string? newTitle = null, string? newText = null, CancellationToken cancellationToken = default)
        {
            var post = await GetPostById(postId, cancellationToken);

            AccessController.CheckPostAccess(userId, post.UserId, role);

            if (post.IsPublished)
            {
                throw new InvalidOperationException(ExceptionMessage.CannotEditPublishedPost);
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
            //TODO: maybe add modifiedBy

            _context.Posts.Update(post);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeletePost(int postId, int userId, RoleType role, CancellationToken cancellationToken = default)
        {
            var post = await GetPostById(postId, cancellationToken);

            AccessController.CheckPostAccess(userId, post.UserId, role);

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task<Post> GetPostById(int postId, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(p => p.Id == postId, cancellationToken);

            return post ?? throw new ArgumentException(ExceptionMessage.PostNotFound);
        }
        private void CheckTitleContraints(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(string.Format(ExceptionMessageGeneric.ValueRequired, nameof(title)));
            }

            if (title.Length > ConstraintValue.PostTitleMaxLength)
            {
                throw new ArgumentOutOfRangeException(string.Format(ExceptionMessageGeneric.ValueOfMustBeLessThan, nameof(title), ConstraintValue.PostTitleMaxLength));
            }
        }
        private void CheckTextContraints(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(string.Format(ExceptionMessageGeneric.ValueRequired, nameof(text)));
            }

            if (text.Length > ConstraintValue.PostTextMaxLength)
            {
                throw new ArgumentOutOfRangeException(string.Format(ExceptionMessageGeneric.ValueOfMustBeLessThan, nameof(text), ConstraintValue.PostTextMaxLength));
            }
        }
    }
}
