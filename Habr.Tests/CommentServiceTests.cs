using Habr.DataAccess.Entities;
using Habr.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;

namespace Habr.Tests
{
    public class CommentServiceTests : IAsyncLifetime
    {
        private DependencyObject _dObject;

        public async Task InitializeAsync()
        {
            _dObject = new DependencyObject();
            await _dObject.Initilize();
        }

        public async Task DisposeAsync()
        {
            await _dObject.Dispose();
        }

        [Fact]
        public async Task AddComment_ShouldAddComment()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);

            await _dObject.CommentService.AddComment("Sample Comment", post.Id, user.Id);

            var comment = await _dObject.Context.Comments.FirstOrDefaultAsync();

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
                await _dObject.CommentService.AddComment("Sample Comment", nonExistentPostId, user.Id));
        }

        [Fact]
        public async Task ReplyToComment_ShouldAddComment()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);
            var parentComment = await CreateComment(user.Id, post.Id);

            await _dObject.CommentService.ReplyToComment("Reply Comment", parentComment.Id, post.Id, user.Id);
            var replyComment = await _dObject.Context.Comments.FirstOrDefaultAsync(c => c.Text == "Reply Comment");

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
                await _dObject.CommentService.ReplyToComment("Reply Comment", nonExistentCommentId, post.Id, user.Id));
        }

        [Fact]
        public async Task EditComment_ShouldUpdateComment()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);
            var comment = await CreateComment(user.Id, post.Id);
            var newText = "newText";

            await _dObject.CommentService.EditComment(newText, comment.Id, user.Id, RoleType.User);

            Assert.Equal(newText, comment.Text);
        }

        [Fact]
        public async Task EditComment_CommentDeleted_ShouldThrowArgumentException()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);
            var comment = await CreateComment(user.Id, post.Id);
            await _dObject.CommentService.DeleteComment(comment.Id, user.Id, RoleType.User);
            var newText = "newText";

            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await _dObject.CommentService.EditComment(newText, comment.Id, user.Id, RoleType.User));
        }

        [Fact]
        public async Task EditComment_UnauthorizedUser_ShouldThrowUnauthorizedAccessException()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);
            var comment = await CreateComment(user.Id, post.Id);
            var newText = "newText";
            var unauthorizedUserId = user.Id + 1;

            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await _dObject.CommentService.EditComment(newText, comment.Id, unauthorizedUserId, RoleType.User));
        }

        [Fact]
        public async Task EditComment_AdminUser_ShouldBypassAccessCheck()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);
            var comment = await CreateComment(user.Id, post.Id);
            var newText = "newText";
            var unauthorizedUserId = user.Id + 1;

            await _dObject.CommentService.EditComment(newText, comment.Id, unauthorizedUserId, RoleType.Admin);

            Assert.Equal(newText, comment.Text);
        }

        [Fact]
        public async Task DeleteComment_ShouldDeleteComment()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);
            var comment = await CreateComment(user.Id, post.Id);

            await _dObject.CommentService.DeleteComment(comment.Id, user.Id, RoleType.User);

            Assert.NotNull(comment);
            Assert.True(comment.IsDeleted);
            Assert.Equal("Comment deleted", comment.Text);
            Assert.Equal(user.Id, comment.UserId);
        }

        [Fact]
        public async Task DeleteComment_UnauthorizedUser_ShouldThrowUnauthorizedAccessException()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);
            var comment = await CreateComment(user.Id, post.Id);
            var unauthorizedUserId = user.Id + 1;

            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await _dObject.CommentService.DeleteComment(comment.Id, unauthorizedUserId, RoleType.User));
        }

        [Fact]
        public async Task DeleteComment_AdminUser_ShouldBypassAccessCheck()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);
            var comment = await CreateComment(user.Id, post.Id);
            var unauthorizedUserId = user.Id + 1;

            await _dObject.CommentService.DeleteComment(comment.Id, unauthorizedUserId, RoleType.Admin);

            Assert.True(comment.IsDeleted);
            Assert.Equal("Comment deleted", comment.Text);
        }

        private async Task<User> CreateUser()
        {
            await _dObject.UserService.CreateUser("john.doe@example.com", "password");
            return await _dObject.Context.Users.FirstAsync();
        }
        private async Task<Post> CreatePost(int userId, bool isPublishedNow = false)
        {
            await _dObject.PostService.AddPost("test", "test", userId, isPublishedNow);

            return await _dObject.Context.Posts.FirstAsync();
        }
        private async Task<Comment> CreateComment(int userId, int postId)
        {
            await _dObject.CommentService.AddComment("text", postId, userId);

            return await _dObject.Context.Comments.FirstAsync();
        }
    }
}
