using AutoMapper;
using AutoMapper.QueryableExtensions;
using Habr.DataAccess;
using Habr.DataAccess.Constraints;
using Habr.DataAccess.DTOs;
using Habr.DataAccess.Entities;
using Habr.DataAccess.Enums;
using Habr.Services.Helpers;
using Habr.Services.Interfaces;
using Habr.Services.Pagination;
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

        public async Task<PostViewDto> GetPostView(int id, CancellationToken cancellationToken = default)
        {
            var post = await _context.Posts
                .Where(p => p.Id == id && p.IsPublished == true)
                .ProjectTo<PostViewDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            return post ?? throw new ArgumentException(ExceptionMessage.PostNotFound);
        }
        public async Task<IEnumerable<PublishedPostDto>> GetPublishedPosts(CancellationToken cancellationToken = default)
        {
            return await _context.Posts
                .Where(p => p.IsPublished)
                .ProjectTo<PublishedPostDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(p => p.PublishDate)
                .ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<PublishedPostV2Dto>> GetPublishedPostsV2(CancellationToken cancellationToken = default)
        {
            return await _context.Posts
                .Where(p => p.IsPublished)
                .ProjectTo<PublishedPostV2Dto>(_mapper.ConfigurationProvider)
                .OrderByDescending(p => p.PublishedAt)
                .ToListAsync(cancellationToken);
        }
        public async Task<PaginatedDto<PublishedPostV2Dto>> GetPublishedPostsPaginated(int pageNumber, int pageSize,
            CancellationToken cancellationToken = default)
        {
            ValidationHelper.ValidateIntMoreThan0(pageNumber, ExceptionMessage.PageNumberLessThan1);
            ValidationHelper.ValidateIntMoreThan0(pageSize, ExceptionMessage.PageSizeLessThan1);

            return await _context.Posts
                .Where(p => p.IsPublished)
                .ProjectTo<PublishedPostV2Dto>(_mapper.ConfigurationProvider)
                .OrderByDescending(p => p.PublishedAt)
                .ToPaginatedDto(pageNumber, pageSize, cancellationToken);
        }
        public async Task<IEnumerable<DraftedPostDto>> GetDraftedPosts(int userId, CancellationToken cancellationToken = default)
        {
            return await _context.Posts
                .Where(p => p.IsPublished == false && p.UserId == userId)
                .ProjectTo<DraftedPostDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(p => p.UpdatedAt)
                .ToListAsync(cancellationToken);
        }
        public async Task<PaginatedDto<DraftedPostDto>> GetDraftedPostsPaginated(int userId, int pageNumber, int pageSize,
            CancellationToken cancellationToken = default)
        {
            ValidationHelper.ValidateIntMoreThan0(pageNumber, ExceptionMessage.PageNumberLessThan1);
            ValidationHelper.ValidateIntMoreThan0(pageSize, ExceptionMessage.PageSizeLessThan1);

            return await _context.Posts
                .Where(p => p.IsPublished == false && p.UserId == userId)
                .ProjectTo<DraftedPostDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(p => p.UpdatedAt)
                .ToPaginatedDto(pageNumber, pageSize, cancellationToken);
        }

        public async Task AddPost(string title, string text, int userId,
            bool isPublishedNow = false, CancellationToken cancellationToken = default)
        {
            CheckTitleConstraints(title);
            CheckTextConstraints(text);

            var post = new Post
            {
                Title = title,
                Text = text,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            if (isPublishedNow)
            {
                post.IsPublished = true;
                post.PublishedAt = DateTime.UtcNow;
            }

            _context.Posts.Add(post);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task PublishPost(int postId, int userId, RoleType role, CancellationToken cancellationToken = default)
        {
            var post = await GetPostById(postId, cancellationToken);

            AccessHelper.CheckPostAccess(userId, post.UserId, role);

            post.IsPublished = true;
            post.PublishedAt = DateTime.UtcNow;

            _context.Update(post);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Post (id:{postId}) published");
        }

        public async Task UnpublishPost(int postId, int userId, RoleType role, CancellationToken cancellationToken = default)
        {
            var post = await _context.Posts
                .Where(p => p.Id == postId)
                .Include(p => p.Comments)
                .SingleOrDefaultAsync(cancellationToken);

            if (post == null)
            {
                throw new ArgumentException(ExceptionMessage.PostNotFound);
            }

            AccessHelper.CheckPostAccess(userId, post.UserId, role);

            if (post.Comments.Where(p => p.IsDeleted == false).Any())
            {
                throw new InvalidOperationException(ExceptionMessage.CannotDraftPostWithComments);
            }

            post.IsPublished = false;
            post.PublishedAt = null;

            _context.Update(post);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Post (id:{postId}) drafted");
        }

        public async Task UpdatePost(int postId, int userId, RoleType role,
            string? newTitle = null, string? newText = null, CancellationToken cancellationToken = default)
        {
            var post = await GetPostById(postId, cancellationToken);

            AccessHelper.CheckPostAccess(userId, post.UserId, role);

            if (post.IsPublished)
            {
                throw new InvalidOperationException(ExceptionMessage.CannotEditPublishedPost);
            }

            if (newTitle != null)
            {
                CheckTitleConstraints(newTitle);
                post.Title = newTitle;
            }

            if (newText != null)
            {
                CheckTextConstraints(newText);
                post.Text = newText;
            }

            post.ModifiedAt = DateTime.UtcNow;

            _context.Posts.Update(post);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeletePost(int postId, int userId, RoleType role, CancellationToken cancellationToken = default)
        {
            var post = await GetPostById(postId, cancellationToken);

            AccessHelper.CheckPostAccess(userId, post.UserId, role);

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task<Post> GetPostById(int postId, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(p => p.Id == postId, cancellationToken);

            return post ?? throw new ArgumentException(ExceptionMessage.PostNotFound);
        }
        private static void CheckTitleConstraints(string title)
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
        private static void CheckTextConstraints(string text)
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
