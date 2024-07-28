using Habr.DataAccess;
using Habr.DataAccess.Entities;
using Habr.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Habr.Tests
{
    public class PostServiceTests : IAsyncLifetime
    {
        private DataContext _context;
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
        }

        public async Task DisposeAsync()
        {
            await _context.Database.EnsureDeletedAsync();

            await _context.DisposeAsync();
            await _serviceProvider.DisposeAsync();
        }

        [Fact]
        public async Task GetPostView_PostPublished_ShouldReturnCorrectStructure()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);
            var comment = await CreateComment(user.Id, post.Id);
            var text = "reply";
            await _commentService.ReplyToComment(text, comment.Id, post.Id, user.Id);

            var postView = await _postService.GetPostView(post.Id);

            Assert.NotNull(postView);
            Assert.Equal(1, postView.Comments.Count);
            Assert.Equal(1, postView.Comments.First().Replies.Count);
            Assert.Equal(text, postView.Comments.First().Replies.First().Text);
            Assert.Equal(user.Name, postView.Comments.First().User.Name);
        }

        [Fact]
        public async Task GetPostView_PostNotPublished_ShouldThrowArgumentException()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, false);
            
            await Assert.ThrowsAsync<ArgumentException>(async () => await _postService.GetPostView(post.Id));
        }

        [Fact]
        public async Task GetPublishedPosts_ShouldReturnPublishedPosts()
        {
            await SeedPostRange();
            var result = await _postService.GetPublishedPosts();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetDraftedPosts_ShouldReturnOwnedDraftedPosts()
        {
            var user = await CreateUser();

            await SeedPostRange(user.Id);

            var result = await _postService.GetDraftedPosts(user.Id);

            Assert.Equal(2, result.Count());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task AddPost_ShouldAddCorrectly(bool isPublishedNow)
        {
            var user = await CreateUser();
            var title = "Sample Title";
            var text = "Sample Text";

            await _postService.AddPost(title, text, user.Id, isPublishedNow);
            var post = await _context.Posts.FirstOrDefaultAsync();

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
        public async Task PublishPost_ShouldPublishPost()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id);

            await _postService.PublishPost(post.Id, user.Id);

            Assert.True(post.IsPublished);
            Assert.NotNull(post.PublishedDate);
        }

        [Fact]
        public async Task PublishPost_UnauthorizedUser_ShouldThrowUnauthorizedAccessException()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id);
            var unauthorizedUserId = user.Id + 1;

            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _postService.PublishPost(post.Id, unauthorizedUserId));
        }

        [Fact]
        public async Task UnpublishPost_Success()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);

            await _postService.UnpublishPost(post.Id, user.Id);

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
            var user = await CreateUser();
            var post = await CreatePost(user.Id);
            var unauthorizedUserId = user.Id + 1;

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _postService.UnpublishPost(post.Id, unauthorizedUserId));
        }

        [Fact]
        public async Task UnpublishPost_PostHasComments_ShouldThrowInvalidOperationException()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id);
            await CreateComment(user.Id, post.Id);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _postService.UnpublishPost(post.Id, user.Id));
        }

        [Fact]
        public async Task UpdatePost_ShouldUpdatePost()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id);

            var newTitle = "Updated Title";
            var newText = "Updated Text";

            await _postService.UpdatePost(post.Id, user.Id, newTitle, newText);

            Assert.NotNull(post);
            Assert.NotNull(post.ModifiedDate);
            Assert.Equal(newTitle, post.Title);
            Assert.Equal(newText, post.Text);
        }

        [Fact]
        public async Task UpdatePost_PostIsPublished_ShouldThrowInvalidOperationException()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);

            var newTitle = "Updated Title";
            var newText = "Updated Text";

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _postService.UpdatePost(post.Id, user.Id, newTitle, newText));
        }

        [Fact]
        public async Task UpdatePost_UnauthorizedUser_ShouldThrowUnauthorizedAccessException()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id);

            var newTitle = "Updated Title";
            var newText = "Updated Text";
            var unauthorizedUserId = user.Id + 1;

            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () => 
                await _postService.UpdatePost(post.Id, unauthorizedUserId, newTitle, newText));
        }

        [Fact]
        public async Task DeletePost_ShouldDeletePost()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id);

            await _postService.DeletePost(post.Id, user.Id);

            post = await _context.Posts.FirstOrDefaultAsync();

            Assert.Null(post);
        }

        [Fact]
        public async Task DeletePost_PostDoesNotExist_ShouldThrowArgumentException()
        {
            var user = await CreateUser();
            var nonExistentPostId = -1;

            await Assert.ThrowsAsync<ArgumentException>(async () => await _postService.DeletePost(nonExistentPostId, user.Id));
        }

        [Fact]
        public async Task DeletePost_UnauthorizedUser_ShouldThrowUnauthorizedAccessException()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _postService.DeletePost(post.Id, user.Id + 1));
        }

        private async Task<User> CreateUser()
        {
            await _userService.CreateUser("john.doe@example.com", "password");
            return await _context.Users.FirstAsync();
        }
        private async Task<Post> CreatePost(int userId, bool isPublishedNow = false)
        {
            await _postService.AddPost("test", "test", userId, isPublishedNow);

            return await _context.Posts.FirstAsync();
        }
        private async Task<Comment> CreateComment(int userId, int postId)
        {
            await _commentService.AddComment("text", postId, userId);

            return await _context.Comments.FirstAsync();
        }
        private async Task SeedPostRange(int? userId = null)
        {
            if (userId == null)
            {
                var user = await CreateUser();
                userId = user.Id;
            }

            _context.Posts.AddRange(new List<Post>
            {
                new Post { Title = "Post 0", Text = "Text 0", UserId = (int)userId },
                new Post { Title = "Post 1", Text = "Text 1", UserId = (int)userId },
                new Post { Title = "Post 2", Text = "Text 2", UserId = (int)userId, IsPublished = true, PublishedDate = DateTime.UtcNow },
                new Post { Title = "Post 3", Text = "Text 3", UserId = (int)userId, IsPublished = true, PublishedDate = DateTime.UtcNow },
                new Post { Title = "Post 5", Text = "Text 5", UserId = (int)userId + 1, PublishedDate = DateTime.UtcNow }
            });
            await _context.SaveChangesAsync();
        }
    }
}
