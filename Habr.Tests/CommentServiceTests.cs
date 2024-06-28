using Habr.DataAccess;
using Habr.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Habr.Tests
{
    [TestClass]
    public class CommentServiceTests
    {
        private DataContext _context;
        private DataContext _secondContext;
        private ServiceProvider _serviceProvider;
        private ICommentService _commentService;
        private IPostService _postService;
        private IUserService _userService;

        [TestInitialize]
        public async Task InitializeAsync()
        {
            _serviceProvider = new ServiceCollection()
                .AddDbContext<DataContext>(options => Configurator.ConfigureDbContextOptions(options))
                .AddScoped<IUserService, UserService>()
                .AddSingleton<IPasswordHasher, PasswordHasher>()
                .AddScoped<ICommentService, CommentService>()
                .AddScoped<IPostService, PostService>()
                .AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Information);
                })
                .BuildServiceProvider();

            _context = _serviceProvider.GetRequiredService<DataContext>();
            _commentService = _serviceProvider.GetRequiredService<ICommentService>();
            _userService = _serviceProvider.GetRequiredService<IUserService>();
            _postService = _serviceProvider.GetRequiredService<IPostService>();

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
        public async Task AddCommentAsync_ShouldAddComment()
        {
            await _userService.CreateUserAsync("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPostAsync("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            await _commentService.AddCommentAsync("Sample Comment", post.Id, user.Id);

            var comment = await _secondContext.Comments.FirstOrDefaultAsync();

            Assert.IsNotNull(comment, "Comment should be added to the database.");
            Assert.AreEqual(post.Id, comment.PostId, "Comment should be linked to the correct post.");
            Assert.AreEqual(user.Id, comment.UserId, "Comment should be linked to the correct user.");
        }

        [TestMethod]
        public async Task ReplyToCommentAsync_ShouldAddComment()
        {
            await _userService.CreateUserAsync("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPostAsync("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            await _commentService.AddCommentAsync("Sample Comment", post.Id, user.Id);

            var parentComment = await _context.Comments.FirstAsync();

            await _commentService.ReplyToCommentAsync("Reply Comment", parentComment.Id, post.Id, user.Id);

            var replyComment = await _secondContext.Comments.FirstOrDefaultAsync(c => c.Text == "Reply Comment");

            Assert.IsNotNull(replyComment, "Reply comment should be added to the database.");
            Assert.AreEqual(parentComment.Id, replyComment.ParentCommentId, "Reply comment should be linked to the correct parent comment.");
            Assert.AreEqual(post.Id, replyComment.PostId, "Reply comment should be linked to the correct post.");
            Assert.AreEqual(user.Id, replyComment.UserId, "Reply comment should be linked to the correct user.");
            Assert.AreEqual("Reply Comment", replyComment.Text, "Reply comment text should be correct.");
        }

        [TestMethod]
        public async Task ModifyCommentAsync_ShouldUpdateComment()
        {
            await _userService.CreateUserAsync("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPostAsync("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            await _commentService.AddCommentAsync("Sample Comment", post.Id, user.Id);

            var comment = await _context.Comments.FirstAsync();

            var newText = "newText";

            await _commentService.ModifyCommentAsync(newText, comment.Id, user.Id);

            var modifiedComment = await _secondContext.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id);

            Assert.IsNotNull(modifiedComment, "Comment should still exist in the database after modification.");
            Assert.AreEqual(newText, modifiedComment.Text, "Comment text should be modified as expected.");
            Assert.AreEqual(user.Id, modifiedComment.UserId, "Comment should still be associated with the same user.");
        }

        [TestMethod]
        public async Task DeleteCommentAsync_ShouldDeleteComment()
        {
            await _userService.CreateUserAsync("email@mail.com", "pas");

            var user = await _context.Users.FirstAsync();

            await _postService.AddPostAsync("test", "test", user.Id);

            var post = await _context.Posts.FirstAsync();

            await _commentService.AddCommentAsync("Sample Comment", post.Id, user.Id);

            var comment = await _context.Comments.FirstAsync();

            var initialModifiedDate = comment.ModifiedDate;

            await _commentService.DeleteCommentAsync(comment.Id, user.Id);

            var deletedComment = await _secondContext.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id);

            Assert.IsNotNull(deletedComment, "Comment should still exist in the database after deletion.");
            Assert.IsTrue(deletedComment.IsDeleted, "Comment should be marked as deleted.");
            Assert.AreEqual("Comment deleted", deletedComment.Text, "Comment text should be updated to 'Comment deleted'.");
            Assert.AreEqual(user.Id, deletedComment.UserId, "Comment should still be associated with the same user.");
            Assert.AreNotEqual(initialModifiedDate, deletedComment.ModifiedDate, "ModifiedDate should be updated after deletion.");
        }
    }
}
