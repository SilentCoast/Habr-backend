using Habr.DataAccess.Entities;
using Habr.DataAccess;
using Habr.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Habr.Tests
{
    [TestClass]
    public class PostServiceTests
    {
        private DataContext _context;
        private DataContext _secondContext;
        private ServiceProvider _serviceProvider;
        private IPostService _postService;
        private IUserService _userService;
        private ICommentService _commentService;

        [TestInitialize]
        public async Task InitializeAsync()
        {
            _serviceProvider = new ServiceCollection()
                .AddDbContext<DataContext>(options => Configurator.ConfigureDbContextOptions(options))
                .AddScoped<IPostService, PostService>()
                .AddScoped<ICommentService, CommentService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IPasswordHasher, PasswordHasher>()
                .AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Information);
                })
                .BuildServiceProvider();

            _context = _serviceProvider.GetRequiredService<DataContext>();
            _postService = _serviceProvider.GetRequiredService<IPostService>();
            _userService = _serviceProvider.GetRequiredService<IUserService>();
            _commentService = _serviceProvider.GetRequiredService<ICommentService>();

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            Configurator.ConfigureDbContextOptions(optionsBuilder);

            _secondContext = new DataContext(optionsBuilder.Options);

            await _context.Database.MigrateAsync();
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await _context.Users.ExecuteDeleteAsync();
            await _context.Posts.ExecuteDeleteAsync();
            await _context.Comments.ExecuteDeleteAsync();

            await _context.DisposeAsync();
            await _secondContext.DisposeAsync();
            await _serviceProvider.DisposeAsync();
        }

        [TestMethod]
        public async Task GetPublishedPostsAsync_ShouldReturnPublishedPosts()
        {
            await SeedPostRangeAsync();
            var result = await _postService.GetPublishedPostsAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetDraftedPostsAsync_ShouldReturnDraftedPosts()
        {
            await _userService.CreateUserAsync("john.doe@example.com", "password");

            var user = await _context.Users.FirstAsync();

            await SeedPostRangeAsync(user.Id);

            var result = await _postService.GetDraftedPostsAsync(user.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public async Task PublishPostAsync_ShouldPublishPost()
        {
            await _userService.CreateUserAsync("email@mail.com","password");

            var user = await _context.Users.FirstAsync();
            await _postService.AddPostAsync("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();
            
            await _postService.PublishPostAsync(post.Id, user.Id);

            post = await _secondContext.Posts.FirstAsync();

            Assert.IsNotNull(post);
            Assert.IsTrue(post.IsPublished);
            Assert.IsNotNull(post.PublishedDate);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public async Task PublishPostAsync_UserDoesNotHaveAccess_ShouldThrowUnauthorizedAccessException()
        {
            await _userService.CreateUserAsync("email@mail.com", "password");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPostAsync("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            var unauthorizedUserId = user.Id + 1;

            await _postService.PublishPostAsync(post.Id, unauthorizedUserId);
        }

        [TestMethod]
        public async Task AddPostAsync_ValidPost_ShouldAddToDatabase()
        {
            await _userService.CreateUserAsync("email@mail.com", "pas");
            
            var user = await _context.Users.FirstAsync();

            var title = "Sample Title";
            var text = "Sample Text";

            await _postService.AddPostAsync(title, text, user.Id);

            var post = await _secondContext.Posts.FirstOrDefaultAsync();
            Assert.IsNotNull(post);
            Assert.AreEqual(title, post.Title);
            Assert.AreEqual(text, post.Text);
            Assert.AreEqual(user.Id, post.UserId);
        }

        [TestMethod]
        public async Task AddPostAsync_PublishNow_ShouldSetPublishDate()
        {
            await _userService.CreateUserAsync("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            var title = "Sample Title";
            var text = "Sample Text";

            await _postService.AddPostAsync(title, text, user.Id, isPublishedNow: true);

            var post = await _secondContext.Posts.FirstOrDefaultAsync();
            Assert.IsNotNull(post);
            Assert.IsTrue(post.IsPublished);
            Assert.IsNotNull(post.PublishedDate);
        }

        [TestMethod]
        public async Task ReturnPostToDraft_Success()
        {
            await _userService.CreateUserAsync("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPostAsync("Sample Post", "Sample post text", user.Id, true);

            var post = await _context.Posts.FirstAsync();

            await _postService.UnpublishPostAsync(post.Id, user.Id);

            post = await _secondContext.Posts.FirstAsync();

            Assert.IsFalse(post.IsPublished, "Post should be in draft state");
            Assert.IsNull(post.PublishedDate, "PublishedDate should be null for a draft post");
        }

        [TestMethod]
        public async Task ReturnPostToDraft_PostNotFound_ShouldThrowArgumentException()
        {
            var postId = 99;
            var exception = await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                _postService.UnpublishPostAsync(postId, 1));

            Assert.AreEqual($"Post with ID {postId} not found", exception.Message);
        }

        [TestMethod]
        public async Task ReturnPostToDraft_UnauthorizedUser_ShouldThrowUnauthorizedAccessException()
        {
            await _userService.CreateUserAsync("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPostAsync("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            var exception = await Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() =>
                _postService.UnpublishPostAsync(post.Id, user.Id+1));
        }

        [TestMethod]
        public async Task ReturnPostToDraft_PostHasComments_ShouldThrowInvalidOperationException()
        {
            await _userService.CreateUserAsync("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPostAsync("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            await _commentService.AddCommentAsync("text", post.Id, user.Id);

            var exception = await Assert.ThrowsExceptionAsync<InvalidOperationException>(() =>
                _postService.UnpublishPostAsync(post.Id, user.Id));
        }

        [TestMethod]
        public async Task UpdatePostAsync_ShouldUpdatePostTitleAndText()
        {
            await _userService.CreateUserAsync("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPostAsync("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            var newTitle = "Updated Title";
            var newText = "Updated Text";

            await _postService.UpdatePostAsync(post.Id, user.Id, newTitle, newText);

            post = await _secondContext.Posts.FirstAsync();

            Assert.IsNotNull(post);
            Assert.AreEqual(newTitle, post.Title);
            Assert.AreEqual(newText, post.Text);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task UpdatePostAsync_PostIsPublished_ShouldThrowInvalidOperationException()
        {
            await _userService.CreateUserAsync("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPostAsync("test", "test", user.Id, true);

            var post = await _context.Posts.FirstAsync();

            var newTitle = "Updated Title";
            var newText = "Updated Text";

            await _postService.UpdatePostAsync(post.Id, user.Id, newTitle, newText);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public async Task UpdatePostAsync_ShouldThrowUnauthorizedAccessException()
        {
            await _userService.CreateUserAsync("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPostAsync("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            var newTitle = "Updated Title";
            var newText = "Updated Text";

            await _postService.UpdatePostAsync(post.Id, user.Id+1, newTitle, newText);
        }

        [TestMethod]
        public async Task DeletePostAsync_ShouldDeletePost()
        {
            await _userService.CreateUserAsync("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPostAsync("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            await _postService.DeletePostAsync(post.Id, user.Id);

            post = await _secondContext.Posts.FirstOrDefaultAsync();

            Assert.IsNull(post, "Post should be deleted from the database.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task DeletePostAsync_PostDoesNotExist_ShouldThrowArgumentException()
        {
            await _userService.CreateUserAsync("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.DeletePostAsync(1, user.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public async Task DeletePostAsync_UserDoesNotHaveAccess_ShouldThrowUnauthorizedAccessException()
        {
            await _userService.CreateUserAsync("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPostAsync("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            await _postService.DeletePostAsync(post.Id, user.Id+1);
        }

        private async Task SeedPostRangeAsync(int? userId = null)
        {
            if (userId == null)
            {
                await _userService.CreateUserAsync("john.doe@example.com", "password");

                var user = await _context.Users.FirstAsync();
                userId = user.Id;
            }

            _context.Posts.AddRange(new List<Post>
            {
                new Post { Title = "Post 1", Text = "Text 1", UserId = (int)userId },
                new Post { Title = "Post 2", Text = "Text 2", UserId = (int)userId, IsPublished = true, PublishedDate = DateTime.UtcNow },
                new Post { Title = "Post 3", Text = "Text 3", UserId = (int)userId, IsPublished = true, PublishedDate = DateTime.UtcNow }
            });
            await _context.SaveChangesAsync();
        }
    }
}
