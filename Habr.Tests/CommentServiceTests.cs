using Habr.DataAccess;
using Habr.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Habr.Tests
{
    //TODO: cover more cases
    [Collection("Sequential")]
    public class CommentServiceTests : IAsyncLifetime
    {
        private DataContext _context;
        private DataContext _secondContext;
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
        public async Task AddComment_ShouldAddComment()
        {
            await _userService.CreateUser("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPost("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            await _commentService.AddComment("Sample Comment", post.Id, user.Id);

            var comment = await _secondContext.Comments.FirstOrDefaultAsync();

            Assert.NotNull(comment);
            Assert.Equal(post.Id, comment.PostId);
            Assert.Equal(user.Id, comment.UserId);
        }

        [Fact]
        public async Task ReplyToComment_ShouldAddComment()
        {
            await _userService.CreateUser("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPost("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            await _commentService.AddComment("Sample Comment", post.Id, user.Id);

            var parentComment = await _context.Comments.FirstAsync();

            await _commentService.ReplyToComment("Reply Comment", parentComment.Id, post.Id, user.Id);

            var replyComment = await _secondContext.Comments.FirstOrDefaultAsync(c => c.Text == "Reply Comment");

            Assert.NotNull(replyComment);
            Assert.Equal(parentComment.Id, replyComment.ParentCommentId);
            Assert.Equal(post.Id, replyComment.PostId);
            Assert.Equal(user.Id, replyComment.UserId);
            Assert.Equal("Reply Comment", replyComment.Text);
        }

        [Fact]
        public async Task ModifyComment_ShouldUpdateComment()
        {
            await _userService.CreateUser("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPost("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            await _commentService.AddComment("Sample Comment", post.Id, user.Id);

            var comment = await _context.Comments.FirstAsync();

            var newText = "newText";

            await _commentService.ModifyComment(newText, comment.Id, user.Id);

            var modifiedComment = await _secondContext.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id);

            Assert.NotNull(modifiedComment);
            Assert.Equal(newText, modifiedComment.Text);
            Assert.Equal(user.Id, modifiedComment.UserId);
        }

        [Fact]
        public async Task DeleteComment_ShouldDeleteComment()
        {
            await _userService.CreateUser("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPost("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            await _commentService.AddComment("Sample Comment", post.Id, user.Id);

            var comment = await _context.Comments.FirstAsync();

            var initialModifiedDate = comment.ModifiedDate;

            await _commentService.DeleteComment(comment.Id, user.Id);

            var deletedComment = await _secondContext.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id);

            Assert.NotNull(deletedComment);
            Assert.True(deletedComment.IsDeleted);
            Assert.Equal("Comment deleted", deletedComment.Text);
            Assert.Equal(user.Id, deletedComment.UserId);
            Assert.NotEqual(initialModifiedDate, deletedComment.ModifiedDate);
        }
    }
}
