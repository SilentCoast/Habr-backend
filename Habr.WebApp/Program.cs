using Habr.DataAccess;
using Habr.Services.AutoMapperProfiles;
using Habr.WebApp.Endpoints;
using Habr.WebApp.Extensions;
using Habr.WebApp.GlobalExceptionHandler;
using Habr.WebApp.Middleware;
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

            builder.Services.RegisterServices(configuration);

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

            builder.Services.ConfigureApiVersioning();

            builder.ConfigureAuth();

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
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseGlobalExceptionHandler();
            app.UseMiddleware<TokenValidationMiddleware>();

            var apiVersionSet = app.ConfigureApiVersionSet();

            app.MapAuthEndpoints();
            app.MapCommentEndpoints();
            app.MapPostEndpoints(apiVersionSet);
            app.MapUserEndpoints();

            await app.RunAsync();
        }
    }
}
