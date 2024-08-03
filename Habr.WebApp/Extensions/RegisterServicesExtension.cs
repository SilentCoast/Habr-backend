using Habr.Services;

namespace Habr.WebApp.Extensions
{
    public static class RegisterServicesExtension
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}
