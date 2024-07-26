using Habr.DataAccess;
using Habr.Services;
using Habr.Services.AutoMapperProfiles;
using Habr.WebApp.ExceptionHandle;
using Habr.WebApp.Extensions;
using Habr.WebApp.MinimalApi.Endpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace Habr.WebApp.MinimalApi
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
            builder.Services.AddScoped<JwtService>();

            builder.Services.AddAutoMapper(typeof(PostProfile).Assembly);

            builder.Services.AddGlobalExceptionHandler();

            builder.Host.AddSerilogLogging(configuration);

            builder.Services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            builder.Services.Configure<JsonOptions>(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
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
