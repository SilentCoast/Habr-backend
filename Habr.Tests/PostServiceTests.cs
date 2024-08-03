using Habr.DataAccess.Entities;
using Habr.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;

namespace Habr.Tests
{
    public class PostServiceTests : IAsyncLifetime
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
        public async Task GetPostView_PostPublished_ShouldReturnCorrectStructure()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);
            var comment = await CreateComment(user.Id, post.Id);
            var text = "reply";
            await _dObject.CommentService.ReplyToComment(text, comment.Id, post.Id, user.Id);

            var postView = await _dObject.PostService.GetPostView(post.Id);

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

            await Assert.ThrowsAsync<ArgumentException>(async () => await _dObject.PostService.GetPostView(post.Id));
        }

        [Fact]
        public async Task GetPublishedPosts_ShouldReturnPublishedPosts()
        {
            await SeedPostRange();
            var result = await _dObject.PostService.GetPublishedPosts();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetDraftedPosts_ShouldReturnOwnedDraftedPosts()
        {
            var user = await CreateUser();

            await SeedPostRange(user.Id);

            var result = await _dObject.PostService.GetDraftedPosts(user.Id);

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

            await _dObject.PostService.AddPost(title, text, user.Id, isPublishedNow);
            var post = await _dObject.Context.Posts.FirstOrDefaultAsync();

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
            
            await _dObject.PostService.PublishPost(post.Id, user.Id, RoleType.User);

            Assert.True(post.IsPublished);
            Assert.NotNull(post.PublishedDate);
        }

        [Fact]
        public async Task PublishPost_UnauthorizedUser_ShouldThrowUnauthorizedAccessException()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id);
            var unauthorizedUserId = user.Id + 1;

            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await _dObject.PostService.PublishPost(post.Id, unauthorizedUserId, RoleType.User));
        }

        [Fact]
        public async Task PublishPost_AdminUser_ShouldBypassAccessCheck()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id);
            var unauthorizedUserId = user.Id + 1;

            await _dObject.PostService.PublishPost(post.Id, unauthorizedUserId, RoleType.Admin);

            Assert.True(post.IsPublished);
            Assert.NotNull(post.PublishedDate);
        }

        [Fact]
        public async Task UnpublishPost_Success()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);

            await _dObject.PostService.UnpublishPost(post.Id, user.Id, RoleType.User);

            Assert.False(post.IsPublished, "Post should be in draft state");
            Assert.Null(post.PublishedDate);
        }

        [Fact]
        public async Task UnpublishPost_PostNotFound_ShouldThrowArgumentException()
        {
            var nonExistentPostId = -99;
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await _dObject.PostService.UnpublishPost(nonExistentPostId, 1, RoleType.User));
        }

        [Fact]
        public async Task UnpublishPost_UnauthorizedUser_ShouldThrowUnauthorizedAccessException()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id);
            var unauthorizedUserId = user.Id + 1;

            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await _dObject.PostService.UnpublishPost(post.Id, unauthorizedUserId, RoleType.User));
        }

        [Fact]
        public async Task UnpublishPost_AdminUser_ShouldBypassAccessCheck()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id, true);
            var unauthorizedUserId = user.Id + 1;

            await _dObject.PostService.UnpublishPost(post.Id, unauthorizedUserId, RoleType.Admin);

            Assert.False(post.IsPublished, "Post should be in draft state");
            Assert.Null(post.PublishedDate);
        }

        [Fact]
        public async Task UnpublishPost_PostHasComments_ShouldThrowInvalidOperationException()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id);
            await CreateComment(user.Id, post.Id);

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _dObject.PostService.UnpublishPost(post.Id, user.Id, RoleType.User));
        }

        [Fact]
        public async Task UpdatePost_ShouldUpdatePost()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id);

            var newTitle = "Updated Title";
            var newText = "Updated Text";

            await _dObject.PostService.UpdatePost(post.Id, user.Id, RoleType.User, newTitle, newText);

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

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _dObject.PostService.UpdatePost(post.Id, user.Id, RoleType.User, newTitle, newText));
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
                await _dObject.PostService.UpdatePost(post.Id, unauthorizedUserId, RoleType.User, newTitle, newText));
        }

        [Fact]
        public async Task UpdatePost_AdminUser_ShouldBypassAccessCheck()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id);

            var newTitle = "Updated Title";
            var newText = "Updated Text";
            var unauthorizedUserId = user.Id + 1;

            await _dObject.PostService.UpdatePost(post.Id, unauthorizedUserId, RoleType.Admin, newTitle, newText);

            Assert.NotNull(post.ModifiedDate);
            Assert.Equal(newTitle, post.Title);
            Assert.Equal(newText, post.Text);
        }

        [Fact]
        public async Task DeletePost_ShouldDeletePost()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id);

            await _dObject.PostService.DeletePost(post.Id, user.Id, RoleType.User);

            post = await _dObject.Context.Posts.FirstOrDefaultAsync();

            Assert.Null(post);
        }

        [Fact]
        public async Task DeletePost_PostDoesNotExist_ShouldThrowArgumentException()
        {
            var user = await CreateUser();
            var nonExistentPostId = -1;

            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await _dObject.PostService.DeletePost(nonExistentPostId, user.Id, RoleType.User));
        }

        [Fact]
        public async Task DeletePost_UnauthorizedUser_ShouldThrowUnauthorizedAccessException()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id);
            var unauthorizedUserId = user.Id + 1;

            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await _dObject.PostService.DeletePost(post.Id, unauthorizedUserId, RoleType.User));
        }

        [Fact]
        public async Task DeletePost_AdminUser_ShouldBypassAccessCheck()
        {
            var user = await CreateUser();
            var post = await CreatePost(user.Id);
            var unauthorizedUserId = user.Id + 1;

            await _dObject.PostService.DeletePost(post.Id, unauthorizedUserId, RoleType.Admin);

            post = await _dObject.Context.Posts.FirstOrDefaultAsync();

            Assert.Null(post);
        }

        private async Task<User> CreateUser(string email = "john.doe@example.com")
        {
            await _dObject.UserService.CreateUser(email, "password");
            return await _dObject.Context.Users.FirstAsync(p => p.Email == email);
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
        private async Task SeedPostRange(int? userId = null)
        {
            if (userId == null)
            {
                var user = await CreateUser();
                userId = user.Id;
            }

            _dObject.Context.Posts.AddRange(new List<Post>
            {
                new Post { Title = "Post 0", Text = "Text 0", UserId = (int)userId },
                new Post { Title = "Post 1", Text = "Text 1", UserId = (int)userId },
                new Post { Title = "Post 2", Text = "Text 2", UserId = (int)userId, IsPublished = true, PublishedDate = DateTime.UtcNow },
                new Post { Title = "Post 3", Text = "Text 3", UserId = (int)userId, IsPublished = true, PublishedDate = DateTime.UtcNow },
                new Post { Title = "Post 5", Text = "Text 5", UserId = (int)userId + 1, PublishedDate = DateTime.UtcNow }
            });
            await _dObject.Context.SaveChangesAsync();
        }
    }
}
