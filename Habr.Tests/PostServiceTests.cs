using Habr.DataAccess;
using Habr.DataAccess.Entities;
using Habr.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Habr.Tests
{
    //TODO: cover more cases
    [Collection("Sequential")]
    public class PostServiceTests : IAsyncLifetime
    {
        private DataContext _context;
        private DataContext _secondContext;
        private ServiceProvider _serviceProvider;
        private IPostService _postService;
        private IUserService _userService;
        private ICommentService _commentService;

        public async Task InitializeAsync()
        {
            _serviceProvider = Configurator.ConfigureServiceProvider();

            _context = _serviceProvider.GetRequiredService<DataContext>();
            _postService = _serviceProvider.GetRequiredService<IPostService>();
            _userService = _serviceProvider.GetRequiredService<IUserService>();
            _commentService = _serviceProvider.GetRequiredService<ICommentService>();

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            Configurator.ConfigureDbContextOptions(optionsBuilder);

            _secondContext = new DataContext(optionsBuilder.Options);

            await _context.Database.MigrateAsync();
        }

        public async Task DisposeAsync()
        {
            await _context.Users.ExecuteDeleteAsync();
            await _context.Posts.ExecuteDeleteAsync();
            await _context.Comments.ExecuteDeleteAsync();

            await _context.DisposeAsync();
            await _secondContext.DisposeAsync();
            await _serviceProvider.DisposeAsync();
        }

        [Fact]
        public async Task GetPublishedPosts_ShouldReturnPublishedPosts()
        {
            await SeedPostRange();
            var result = await _postService.GetPublishedPosts();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetDraftedPosts_ShouldReturnDraftedPosts()
        {
            await _userService.CreateUser("john.doe@example.com", "password");

            var user = await _context.Users.FirstAsync();

            await SeedPostRange(user.Id);

            var result = await _postService.GetDraftedPosts(user.Id);

            Assert.NotNull(result);
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async Task PublishPost_ShouldPublishPost()
        {
            await _userService.CreateUser("email@mail.com", "password");
            var user = await _context.Users.FirstAsync();

            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();

            await _postService.PublishPost(post.Id, user.Id);

            post = await _secondContext.Posts.FirstAsync();

            Assert.NotNull(post);
            Assert.True(post.IsPublished);
            Assert.NotNull(post.PublishedDate);
        }

        [Fact]
        public async Task PublishPost_UserDoesNotHaveAccess_ShouldThrowUnauthorizedAccessException()
        {
            await _userService.CreateUser("email@mail.com", "password");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPost("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            var unauthorizedUserId = user.Id + 1;

            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _postService.PublishPost(post.Id, unauthorizedUserId));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task AddPost_ShouldBehaveCorrectly(bool isPublishedNow)
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();

            var title = "Sample Title";
            var text = "Sample Text";

            await _postService.AddPost(title, text, user.Id, isPublishedNow);

            var post = await _secondContext.Posts.FirstOrDefaultAsync();

            Assert.NotNull(post);
            Assert.Equal(title, post.Title);
            Assert.Equal(text, post.Text);
            Assert.Equal(user.Id, post.UserId);
            Assert.Equal(isPublishedNow, post.IsPublished);

            if (isPublishedNow)
            {
                Assert.NotNull(post.PublishedDate);
            }
            else
            {
                Assert.Null(post.PublishedDate);
            }
        }

        [Fact]
        public async Task UnpublishPost_Success()
        {
            await _userService.CreateUser("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPost("Sample Post", "Sample post text", user.Id, true);

            var post = await _context.Posts.FirstAsync();

            await _postService.UnpublishPost(post.Id, user.Id);

            post = await _secondContext.Posts.FirstAsync();

            Assert.False(post.IsPublished, "Post should be in draft state");
            Assert.Null(post.PublishedDate);
        }

        [Fact]
        public async Task UnpublishPost_PostNotFound_ShouldThrowArgumentException()
        {
            //non-existent postId
            var postId = -99;
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                _postService.UnpublishPost(postId, 1));
        }

        [Fact]
        public async Task UnpublishPost_UnauthorizedUser_ShouldThrowUnauthorizedAccessException()
        {
            await _userService.CreateUser("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPost("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                _postService.UnpublishPost(post.Id, user.Id + 1));
        }

        [Fact]
        public async Task UnpublishPost_PostHasComments_ShouldThrowInvalidOperationException()
        {
            await _userService.CreateUser("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPost("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            await _commentService.AddComment("text", post.Id, user.Id);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _postService.UnpublishPost(post.Id, user.Id));
        }

        [Fact]
        public async Task UpdatePost_ShouldUpdatePost()
        {
            await _userService.CreateUser("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPost("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            var newTitle = "Updated Title";
            var newText = "Updated Text";

            await _postService.UpdatePost(post.Id, user.Id, newTitle, newText);

            post = await _secondContext.Posts.FirstAsync();
            
            Assert.NotNull(post);
            Assert.NotNull(post.ModifiedDate);
            Assert.Equal(newTitle, post.Title);
            Assert.Equal(newText, post.Text);
        }

        [Fact]
        public async Task UpdatePost_PostIsPublished_ShouldThrowInvalidOperationException()
        {
            await _userService.CreateUser("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPost("test", "test", user.Id, true);

            var post = await _context.Posts.FirstAsync();

            var newTitle = "Updated Title";
            var newText = "Updated Text";

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _postService.UpdatePost(post.Id, user.Id, newTitle, newText));
        }

        [Fact]
        public async Task UpdatePost_UnauthorizedUser_ShouldThrowUnauthorizedAccessException()
        {
            await _userService.CreateUser("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPost("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            var newTitle = "Updated Title";
            var newText = "Updated Text";

            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _postService.UpdatePost(post.Id, user.Id + 1, newTitle, newText));
        }

        [Fact]
        public async Task DeletePost_ShouldDeletePost()
        {
            await _userService.CreateUser("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPost("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            await _postService.DeletePost(post.Id, user.Id);

            post = await _secondContext.Posts.FirstOrDefaultAsync();

            Assert.Null(post);
        }

        [Fact]
        public async Task DeletePost_PostDoesNotExist_ShouldThrowArgumentException()
        {
            await _userService.CreateUser("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await Assert.ThrowsAsync<ArgumentException>(async () => await _postService.DeletePost(1, user.Id));
        }

        [Fact]
        public async Task DeletePost_UserDoesNotHaveAccess_ShouldThrowUnauthorizedAccessException()
        {
            await _userService.CreateUser("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPost("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _postService.DeletePost(post.Id, user.Id + 1));
        }

        private async Task SeedPostRange(int? userId = null)
        {
            if (userId == null)
            {
                await _userService.CreateUser("john.doe@example.com", "password");

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
