using Habr.DataAccess;
using Habr.DataAccess.Entities;
using Habr.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Habr.Tests
{
    public class CommentServiceTests : IAsyncLifetime
    {
        private DataContext _context;
        private ServiceProvider _serviceProvider;
        private ICommentService _commentService;
        private IPostService _postService;
        private IUserService _userService;

        public async Task InitializeAsync()
        {
            _serviceProvider = Configurator.ConfigureServiceProvider();

            _context = _serviceProvider.GetRequiredService<DataContext>();
            _commentService = _serviceProvider.GetRequiredService<ICommentService>();
            _userService = _serviceProvider.GetRequiredService<IUserService>();
            _postService = _serviceProvider.GetRequiredService<IPostService>();
        }

        public async Task DisposeAsync()
        {
            await _context.Database.EnsureDeletedAsync();

            await _context.DisposeAsync();
            await _serviceProvider.DisposeAsync();
        }

        [Fact]
        public async Task AddComment_ShouldAddComment()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);

            await _commentService.AddComment("Sample Comment", post.Id, user.Id);

            var comment = await _context.Comments.FirstOrDefaultAsync();

            Assert.NotNull(comment);
            Assert.Equal(post.Id, comment.PostId);
            Assert.Equal(user.Id, comment.UserId);
        }

        [Fact]
        public async Task AddComment_PostDoesntExist_ShouldThrowArgumentException()
        {
            var user = await CreateUser();
            var nonExistentPostId = -1;

            await Assert.ThrowsAsync<ArgumentException>(async () => 
                await _commentService.AddComment("Sample Comment", nonExistentPostId, user.Id));
        }

        [Fact]
        public async Task ReplyToComment_ShouldAddComment()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);
            var parentComment = await CreateComment(user.Id, post.Id);

            await _commentService.ReplyToComment("Reply Comment", parentComment.Id, post.Id, user.Id);
            var replyComment = await _context.Comments.FirstOrDefaultAsync(c => c.Text == "Reply Comment");

            Assert.NotNull(replyComment);
            Assert.Equal(parentComment.Id, replyComment.ParentCommentId);
            Assert.Equal(post.Id, replyComment.PostId);
            Assert.Equal(user.Id, replyComment.UserId);
            Assert.Equal("Reply Comment", replyComment.Text);
        }

        [Fact]
        public async Task ReplyToComment_CommentDoesntExist_ShouldThrowArgumentException()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);
            var nonExistentCommentId = -1;

            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await _commentService.ReplyToComment("Reply Comment", nonExistentCommentId, post.Id, user.Id));
        }

        [Fact]
        public async Task ModifyComment_ShouldUpdateComment()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);
            var comment = await CreateComment(user.Id, post.Id);
            var newText = "newText";

            await _commentService.ModifyComment(newText, comment.Id, user.Id);
            var modifiedComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id);

            Assert.NotNull(modifiedComment);
            Assert.Equal(newText, modifiedComment.Text);
            Assert.Equal(user.Id, modifiedComment.UserId);
        }

        [Fact]
        public async Task ModifyComment_CommentDeleted_ShouldThrowArgumentException()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);
            var comment = await CreateComment(user.Id, post.Id);
            await _commentService.DeleteComment(comment.Id, user.Id);
            var newText = "newText";

            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await _commentService.ModifyComment(newText, comment.Id, user.Id));
        }

        [Fact]
        public async Task DeleteComment_ShouldDeleteComment()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);
            var comment = await CreateComment(user.Id, post.Id);

            await _commentService.DeleteComment(comment.Id, user.Id);

            Assert.NotNull(comment);
            Assert.True(comment.IsDeleted);
            Assert.Equal("Comment deleted", comment.Text);
            Assert.Equal(user.Id, comment.UserId);
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
    }
}
