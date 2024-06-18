using Habr.DataAccess;
using Habr.DataAccess.Entities;
using Habr.DataAccess.Repositories;
using Habr.Services;
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
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IPostRepository, PostRepository>()
                .AddScoped<IPostService, PostService>()
                .AddScoped<ICommentRepository, CommentRepository>()
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
                logger.LogInformation("Failed to connect to Database.");
            }



            //TODO: remove tests or put into test project

            var userService = serviceProvider.GetRequiredService<IUserService>();
            var postService = serviceProvider.GetRequiredService<IPostService>();
            var commentService = serviceProvider.GetRequiredService<ICommentService>();

            //Create user (Registration)
            string name = "John Doe";
            string email = "john.doe@example.com";
            string password = "SecurePassword123";

            await userService.CreateUserAsync(name, email, password);

            //Login
            try
            {
                await userService.LogIn(email, password);
            }
            catch(UnauthorizedAccessException)
            {
                logger.LogInformation("Wrong email or password");
                return;
            }

            logger.LogInformation("Login successful!");

            User user = context.Users.Single(p => p.Email == email);

            //add post
            await postService.AddPostAsync("New Post Title", "New Post Text", user.Id);

            //update post
            var postToUpdate = await context.Posts.FirstOrDefaultAsync(); // Get a post from the database
            await postService.UpdatePostAsync(postToUpdate.Id, user.Id, "Updated Title", "Updated Text");

            //get posts
            var posts = await postService.GetPostsAsync();
            string message = "";
            foreach (var thePost in posts)
            {
                message += $"\nPost Id: {thePost.Id}, Title: {thePost.Title}, Text: {thePost.Text}";
            }
            logger.LogInformation("Get Posts:" + message);


            Post post = await postService.GetPostAsync(includeComments: true);

            //add comment under post
            await commentService.AddCommentAsync("Comment under post", post.Id, user.Id);
            await commentService.AddCommentAsync("Comment to delete", post.Id, user.Id);

            Comment comment = post.Comments.OrderBy(p => p.Id).First();
            Comment comment2 = post.Comments.OrderBy(p => p.Id).Skip(1).First();
            //add comment under comment
            await commentService.ReplyToCommentAsync("Comment under comment", comment.Id, post.Id, user.Id);

            //modify comment
            await commentService.ModifyCommentAsync("Updated comment under post", comment.Id, user.Id);

            //soft delete comment
            await commentService.DeleteCommentAsync(comment2.Id, user.Id);

            logger.LogInformation("Test cases executed successfully.");
            Console.ReadKey();
        }
    }
}
