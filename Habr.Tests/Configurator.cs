using Habr.DataAccess;
using Habr.Services;
using Habr.Services.AutoMapperProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace Habr.Tests
{
    public static class Configurator
    {
        public static ServiceProvider ConfigureServiceProvider()
        {
            var mockJwtService = new Mock<IJwtService>();

            mockJwtService.Setup(service => service.GenerateToken(It.IsAny<int>())).Returns("mocked_token");

            return new ServiceCollection()
                .AddDbContext<DataContext>(options => ConfigureDbContextOptions(options))
                .AddScoped<IPostService, PostService>()
                .AddScoped<ICommentService, CommentService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped(_ => mockJwtService.Object)
                .AddScoped<IPasswordHasher, PasswordHasher>()
                .AddAutoMapper(typeof(PostProfile).Assembly)
                .AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Information);
                })
                .BuildServiceProvider();
        }

        public static void ConfigureDbContextOptions(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        }
    }
}
