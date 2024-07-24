using Habr.DataAccess;
using Habr.Services;
using Habr.Services.AutoMapperProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace Habr.Tests
{
    public static class Configurator
    {
        private static IConfigurationRoot? configuration;
        public static IConfigurationRoot Configuration
        {
            get
            {
                configuration ??= new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json")
                       .Build();

                return configuration;
            }
        }

        public static ServiceProvider ConfigureServiceProvider()
        {
            var mockJwtService = new Mock<IJwtService>();

            mockJwtService.Setup(service => service.GenerateToken(It.IsAny<int>())).Returns("mocked_token");

            return new ServiceCollection()
                //TODO: maybe remove configuration and use raw connectionString
                .AddSingleton<IConfiguration>(Configurator.Configuration)
                .AddDbContext<DataContext>(options => Configurator.ConfigureDbContextOptions(options))
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
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("HabrDBTestConnection"));
        }
    }
}
