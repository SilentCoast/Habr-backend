using Habr.DataAccess;
using Habr.DataAccess.Entities;
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



            //TODO: remove tests or put into test project

            var userService = serviceProvider.GetRequiredService<IUserService>();
            var postService = serviceProvider.GetRequiredService<IPostService>();
            var commentService = serviceProvider.GetRequiredService<ICommentService>();

            // Create user (Registration)
            string name = "John Doe";
            string email = "john.doe@example.com";
            string password = "SecurePassword123";

            try
            {
                await userService.CreateUserAsync(name, email, password);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error creating user: {ex.Message}");
            }

            // Login
            try
            {
                await userService.LogIn(email, password);
                logger.LogInformation("Login successful!");
            }
            catch (UnauthorizedAccessException)
            {
                logger.LogError("Wrong email or password");
            }
            catch (Exception ex)
            {
                logger.LogError($"Error logging in: {ex.Message}");
            }

            User user = null;
            try
            {
                user = context.Users.Single(p => p.Email == email);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching user: {ex.Message}");
            }

            // Add post
            try
            {
                await postService.AddPostAsync("New Post Title", "New Post Text", user.Id);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error adding post: {ex.Message}");
            }

            // Update post
            try
            {
                var postToUpdate = await context.Posts.FirstOrDefaultAsync(); // Get a post from the database
                await postService.UpdatePostAsync(postToUpdate.Id, user.Id, "Updated Title", "Updated Text");
            }
            catch (Exception ex)
            {
                logger.LogError($"Error updating post: {ex.Message}");
            }

            // Get posts
            try
            {
                var posts = await postService.GetPostsAsync();
                string message = "";
                foreach (var thePost in posts)
                {
                    message += $"\nPost Id: {thePost.Id}, Title: {thePost.Title}, Text: {thePost.Text}";
                }
                logger.LogInformation("Get Posts:" + message);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error getting posts: {ex.Message}");
            }

            Post post = null;
            try
            {
                post = await context.Posts.Include(p => p.Comments).FirstAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error getting post: {ex.Message}");
            }

            // Add comment under post
            try
            {
                await commentService.AddCommentAsync("Comment under post", post.Id, user.Id);
                await commentService.AddCommentAsync("Comment to delete", post.Id, user.Id);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error adding comment: {ex.Message}");
            }

            Comment comment = null;
            Comment comment2 = null;
            try
            {
                comment = post.Comments.OrderBy(p => p.Id).First();
                comment2 = post.Comments.OrderBy(p => p.Id).Skip(1).First();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching comments: {ex.Message}");
            }

            // Add comment under comment
            try
            {
                await commentService.ReplyToCommentAsync("Comment under comment", comment.Id, post.Id, user.Id);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error adding reply to comment: {ex.Message}");
            }

            // Modify comment
            try
            {
                await commentService.ModifyCommentAsync("Updated comment under post", comment.Id, user.Id);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error modifying comment: {ex.Message}");
            }

            // Soft delete comment
            try
            {
                await commentService.DeleteCommentAsync(comment2.Id, user.Id);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error deleting comment: {ex.Message}");
            }

            logger.LogInformation("Test cases executed successfully.");
            Console.ReadKey();
        }
    }
}
