using Habr.DataAccess;
using Habr.DataAccess.Entities;
using Habr.DataAccess.Enums;
using Habr.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Habr.Tests
{
    public class DependencyObject
    {
        public DataContext Context { get; set; }
        public ServiceProvider ServiceProvider { get; set; }
        public ICommentService CommentService { get; set; }
        public IPostService PostService { get; set; }
        public IUserService UserService { get; set; }
        public ITokenRevocationService TokenRevocationService { get; set; }
        public IPostRatingService PostRatingService { get; set; }

        public async Task Initilize()
        {
            ServiceProvider = Configurator.ConfigureServiceProvider();

            Context = ServiceProvider.GetRequiredService<DataContext>();
            CommentService = ServiceProvider.GetRequiredService<ICommentService>();
            UserService = ServiceProvider.GetRequiredService<IUserService>();
            PostService = ServiceProvider.GetRequiredService<IPostService>();
            TokenRevocationService = ServiceProvider.GetRequiredService<ITokenRevocationService>();
            PostRatingService = ServiceProvider.GetRequiredService<IPostRatingService>();

            Context.Roles.AddRange
            (
                new Role { Id = 1, RoleType = RoleType.User, Name = "User" },
                new Role { Id = 2, RoleType = RoleType.Admin, Name = "Admin" }
            );
            await Context.SaveChangesAsync();
        }

        public async Task Dispose()
        {
            await Context.Database.EnsureDeletedAsync();

            await Context.DisposeAsync();
            await ServiceProvider.DisposeAsync();
        }

        public async Task<User> CreateUser(string email = "john.doe@example.com", string password = "password")
        {
            await UserService.CreateUser(email, password);
            return await Context.Users.FirstAsync();
        }
        public async Task<Post> CreatePost(int userId, bool isPublishedNow = false)
        {
            await PostService.AddPost("test", "test", userId, isPublishedNow);

            return await Context.Posts.FirstAsync();
        }
        public async Task<Comment> CreateComment(int userId, int postId)
        {
            await CommentService.AddComment("text", postId, userId);

            return await Context.Comments.FirstAsync();
        }
    }
}
