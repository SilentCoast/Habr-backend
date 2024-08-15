using Habr.DataAccess;
using Habr.Services;
using Habr.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Habr.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Configure services
            var serviceProvider = new ServiceCollection()
                .AddDbContext<DataContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("HabrDBConnection")))
                .AddScoped<IUserService, UserService>()
                .AddScoped<IPostService, PostService>()
                .AddScoped<ICommentService, CommentService>()
                .AddSingleton<IPasswordHasher, PasswordHasher>()
                .AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Information);
                })
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<Program>>();

            using var context = serviceProvider.GetService<DataContext>();

            logger.LogInformation("Loading database...");
            // Ensure database is in valid state
            if (context != null)
            {
                await context.Database.MigrateAsync();
                logger.LogInformation("Database is valid");
            }
            else
            {
                logger.LogError("Failed to connect to Database.");
            }

            Console.ReadKey();
        }
    }
}
