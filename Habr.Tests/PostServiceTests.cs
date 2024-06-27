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
        private ServiceProvider _serviceProvider;
        private IPostService _postService;

        [TestInitialize]
        public async Task InitializeAsync()
        {
            _serviceProvider = new ServiceCollection()
                .AddDbContext<DataContext>(options => {
                    options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
                })
                .AddScoped<IPostService, PostService>()
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
            var userService = _serviceProvider.GetRequiredService<IUserService>();

            await userService.CreateUserAsync("john.doe@example.com", "password");
            await userService.CreateUserAsync("johnsecond.doe@example.com", "passwordsecure");
        }

        [TestMethod]
        public async Task GetPostsAsync_ShouldReturnAllPosts()
        {
            await SeedPostRangeAsync();

            var posts = await _postService.GetPostsAsync();

            Assert.IsNotNull(posts, "Posts collection should not be null");
            Assert.AreEqual(3, posts.Count(), "Expected number of posts does not match");

            Assert.IsTrue(posts.Any(p => p.Title == "Post 1"));
            Assert.IsTrue(posts.Any(p => p.Title == "Post 2"));
            Assert.IsTrue(posts.Any(p => p.Title == "Post 3"));
        }

        [TestMethod]
        public async Task GetPostsAsync_Filter_ShouldReturnFilteredPosts()
        {
            await SeedPostRangeAsync();

            Expression<Func<Post, bool>> filter = p => p.Title == "Post 1";

            var posts = await _postService.GetPostsAsync(filter);

            Assert.IsNotNull(posts, "Filtered posts collection should not be null");
            Assert.AreEqual(1, posts.Count(), "Expected number of filtered posts does not match");
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
            await SeedPostRangeAsync();

            var result = await _postService.GetDraftedPostsAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public async Task PublishPostAsync_ShouldPublishPost()
        {
            int userId = 1;
            int postId = 1;
            await _postService.AddPostAsync("test", "test", userId);
            
            var post = await _context.Posts.FindAsync(postId);
            
            await _postService.PublishPostAsync(postId, userId);

            Assert.IsNotNull(post);
            Assert.IsTrue(post.IsPublished);
            Assert.IsNotNull(post.PublishedDate);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public async Task PublishPostAsync_ShouldThrowUnauthorizedAccessException_WhenUserDoesNotHaveAccess()
        {
            await _postService.AddPostAsync("test", "test", 2);
            int postId = 1;
            int userId = 1;

            await _postService.PublishPostAsync(postId, userId);
        }

        [TestMethod]
        public async Task AddPostAsync_ValidPost_ShouldAddToDatabase()
        {
            var title = "Sample Title";
            var text = "Sample Text";
            var createdByUserId = 1;

            await _postService.AddPostAsync(title, text, createdByUserId);

            var post = _context.Posts.FirstOrDefault();
            Assert.IsNotNull(post);
            Assert.AreEqual(title, post.Title);
            Assert.AreEqual(text, post.Text);
            Assert.AreEqual(createdByUserId, post.UserId);
        }

        [TestMethod]
        public async Task AddPostAsync_PublishNow_ShouldSetPublishDate()
        {
            var title = "Sample Title";
            var text = "Sample Text";
            var createdByUserId = 1;

            await _postService.AddPostAsync(title, text, createdByUserId, isPublishedNow: true);

            var post = _context.Posts.FirstOrDefault();
            Assert.IsNotNull(post);
            Assert.IsTrue(post.IsPublished);
            Assert.IsNotNull(post.PublishedDate);
        }

        [TestMethod]
        public async Task ReturnPostToDraft_Success()
        {
            await _postService.AddPostAsync("Sample Post", "Sample post text", 1, true);
            await _postService.ReturnPostToDraftAsync(1, 1);

            var post = await _context.Posts
                .Where(p => p.Id == 1)
                .Select(p => new
                {
                    p.IsPublished,
                    p.PublishedDate
                }).SingleAsync();

            Assert.IsFalse(post.IsPublished, "Post should be in draft state");
            Assert.IsNull(post.PublishedDate, "PublishedDate should be null for a draft post");
        }

        [TestMethod]
        public async Task ReturnPostToDraft_PostNotFound_ShouldThrowArgumentException()
        {
            int postId = 99;
            var exception = await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                _postService.ReturnPostToDraftAsync(postId, 1));

            Assert.AreEqual($"Post with ID {postId} not found", exception.Message);
        }

        [TestMethod]
        public async Task ReturnPostToDraft_UnauthorizedUser_ShouldThrowUnauthorizedAccessException()
        {
            await _postService.AddPostAsync("test", "test", 1);
            var exception = await Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() =>
                _postService.ReturnPostToDraftAsync(1, 2));
        }

        [TestMethod]
        public async Task ReturnPostToDraft_PostHasComments_ShouldThrowInvalidOperationException()
        {
            await _postService.AddPostAsync("test", "test", 1);
            _context.Comments.Add(new Comment { Text = "Sample comment", PostId = 1, UserId = 1 });
            await _context.SaveChangesAsync();

            var exception = await Assert.ThrowsExceptionAsync<InvalidOperationException>(() =>
                _postService.ReturnPostToDraftAsync(1, 1));
        }

        [TestMethod]
        public async Task UpdatePostAsync_ShouldUpdatePostTitleAndText()
        {
            int postId = 1;
            int userId = 1;

            await _postService.AddPostAsync("test", "test", userId);

            string newTitle = "Updated Title";
            string newText = "Updated Text";

            await _postService.UpdatePostAsync(postId, userId, newTitle, newText);

            var post = await _context.Posts.FindAsync(postId);
            Assert.IsNotNull(post);
            Assert.AreEqual(newTitle, post.Title);
            Assert.AreEqual(newText, post.Text);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task UpdatePostAsync_PostIsPublished_ShouldThrowInvalidOperationException()
        {
            int postId = 1;
            int userId = 1;

            await _postService.AddPostAsync("test","test",userId,isPublishedNow: true);

            string newTitle = "Updated Title";
            string newText = "Updated Text";

            await _postService.UpdatePostAsync(postId, userId, newTitle, newText);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public async Task UpdatePostAsync_ShouldThrowUnauthorizedAccessException()
        {
            int postId = 1;
            int userId = 1;

            await _postService.AddPostAsync("test", "test", 2);

            string newTitle = "Updated Title";
            string newText = "Updated Text";

            await _postService.UpdatePostAsync(postId, userId, newTitle, newText);
        }

        [TestMethod]
        public async Task DeletePostAsync_ShouldDeletePost()
        {
            int postId = 1;
            int userId = 1;

            await _postService.AddPostAsync("test", "test", userId);

            await _postService.DeletePostAsync(postId, userId);

            var post = await _context.Posts.FindAsync(postId);
            Assert.IsNull(post, "Post should be deleted from the database.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task DeletePostAsync_PostDoesNotExist_ShouldThrowArgumentException()
        {
            int postId = 999; // Non-existent post ID
            int userId = 1;

            await _postService.AddPostAsync("test", "test", userId);

            await _postService.DeletePostAsync(postId, userId);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public async Task DeletePostAsync_UserDoesNotHaveAccess_ShouldThrowUnauthorizedAccessException()
        {
            int postId = 1;
            int userId = 1;

            await _postService.AddPostAsync("test", "test", userId);

            await _postService.DeletePostAsync(postId, 2);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.DisposeAsync();
            await _serviceProvider.DisposeAsync();
        }

        private async Task SeedPostRangeAsync()
        {
            _context.Posts.AddRange(new List<Post>
            {
                new Post { Title = "Post 1", Text = "Text 1", UserId = 1 },
                new Post { Title = "Post 2", Text = "Text 2", UserId = 1, IsPublished = true, PublishedDate = DateTime.UtcNow },
                new Post { Title = "Post 3", Text = "Text 3", UserId = 1, IsPublished = true, PublishedDate = DateTime.UtcNow }
            });
            await _context.SaveChangesAsync();
        }
    }
}
