using Habr.DataAccess;
using Habr.DataAccess.Entities;
using Habr.DataAccess.Enums;
using Habr.Services;
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

        public async Task Initilize()
        {
            ServiceProvider = Configurator.ConfigureServiceProvider();

            Context = ServiceProvider.GetRequiredService<DataContext>();
            CommentService = ServiceProvider.GetRequiredService<ICommentService>();
            UserService = ServiceProvider.GetRequiredService<IUserService>();
            PostService = ServiceProvider.GetRequiredService<IPostService>();

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
    }
}
