using Habr.DataAccess;
using Habr.Services;
using Habr.Services.AutoMapperProfiles;
using Habr.WebApp.Endpoints;
using Habr.WebApp.ExceptionHandle;
using Habr.WebApp.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Habr.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("HabrDBConnection")));

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IJwtService, JwtService>();

            builder.Services.AddAutoMapper(typeof(PostProfile).Assembly);

            builder.Services.AddGlobalExceptionHandler();

            builder.Host.AddSerilogLogging(configuration);

            builder.Services.Configure<JsonSerializerOptions>(options =>
            {
                options.ReferenceHandler = ReferenceHandler.Preserve;
            });

            builder.Services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.ConfigureSwagger();

            builder.ConfigureAuthentication();
            builder.Services.AddAuthorization();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseGlobalExceptionHandler();

            app.MapAuthEndpoints();
            app.MapCommentEndpoints();
            app.MapPostEndpoints();
            app.MapUserEndpoints();

            app.Run();
        }
    }
}
