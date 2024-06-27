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
        private ServiceProvider _serviceProvider;
        private ICommentService _commentService;

        [TestInitialize]
        public async Task InitializeAsync()
        {
            _serviceProvider = new ServiceCollection()
                .AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
                })
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
            var userService = _serviceProvider.GetRequiredService<IUserService>();
            var postService = _serviceProvider.GetRequiredService<IPostService>();

            await userService.CreateUserAsync("john.doe@example.com", "password");
            await userService.CreateUserAsync("johnsecond.doe@example.com", "passwordsecure");

            await postService.AddPostAsync("post 1", "post 1 text", 1);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.DisposeAsync();
            await _serviceProvider.DisposeAsync();
        }

        [TestMethod]
        public async Task AddCommentAsync_ShouldAddComment()
        {
            int postId = 1;
            int userId = 1;
            await _commentService.AddCommentAsync("Sample Comment", postId, userId);

            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Text == "Sample Comment");

            Assert.IsNotNull(comment, "Comment should be added to the database.");
            Assert.AreEqual(postId, comment.PostId, "Comment should be linked to the correct post.");
            Assert.AreEqual(userId, comment.UserId, "Comment should be linked to the correct user.");
        }

        [TestMethod]
        public async Task ReplyToCommentAsync_ShouldAddComment()
        {
            int postId = 1;
            int userId = 1;

            await _commentService.AddCommentAsync("Sample Comment", postId, userId);

            var parentComment = _context.Comments.First();

            await _commentService.ReplyToCommentAsync("Reply Comment", parentComment.Id, postId, userId);

            var replyComment = await _context.Comments.FirstOrDefaultAsync(c => c.Text == "Reply Comment");

            Assert.IsNotNull(replyComment, "Reply comment should be added to the database.");
            Assert.AreEqual(parentComment.Id, replyComment.ParentCommentId, "Reply comment should be linked to the correct parent comment.");
            Assert.AreEqual(postId, replyComment.PostId, "Reply comment should be linked to the correct post.");
            Assert.AreEqual(userId, replyComment.UserId, "Reply comment should be linked to the correct user.");
            Assert.AreEqual("Reply Comment", replyComment.Text, "Reply comment text should be correct.");
        }

        [TestMethod]
        public async Task ModifyCommentAsync_ShouldUpdateComment()
        {
            int postId = 1;
            int userId = 1;

            await _commentService.AddCommentAsync("Sample Comment", postId, userId);

            int commentId = 1;
            string newText = "newText";

            await _commentService.ModifyCommentAsync(newText, commentId, userId);

            var modifiedComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);

            Assert.IsNotNull(modifiedComment, "Comment should still exist in the database after modification.");
            Assert.AreEqual(newText, modifiedComment.Text, "Comment text should be modified as expected.");
            Assert.AreEqual(userId, modifiedComment.UserId, "Comment should still be associated with the same user.");
        }

        [TestMethod]
        public async Task DeleteCommentAsync_ShouldDeleteComment()
        {
            int postId = 1;
            int userId = 1;

            await _commentService.AddCommentAsync("Sample Comment", postId, userId);

            var comment = _context.Comments.First();
            int commentId = comment.Id;
            DateTime? initialModifiedDate = comment.ModifiedDate;

            await _commentService.DeleteCommentAsync(commentId, userId);

            var deletedComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);

            Assert.IsNotNull(deletedComment, "Comment should still exist in the database after deletion.");
            Assert.IsTrue(deletedComment.IsDeleted, "Comment should be marked as deleted.");
            Assert.AreEqual("Comment deleted", deletedComment.Text, "Comment text should be updated to 'Comment deleted'.");
            Assert.AreEqual(userId, deletedComment.UserId, "Comment should still be associated with the same user.");
            Assert.AreNotEqual(initialModifiedDate, deletedComment.ModifiedDate, "ModifiedDate should be updated after deletion.");
        }
    }
}
