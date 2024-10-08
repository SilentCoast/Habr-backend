﻿using Habr.Services;
using Habr.Services.Interfaces;
using FluentValidation;
using Habr.WebApp.Validation;

namespace Habr.WebApp.Extensions
{
    public static class RegisterServicesExtension
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostRatingService, PostRatingService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<ITokenRevocationService, TokenRevocationService>();

            services.AddValidatorsFromAssemblyContaining<PaginationParamsValidator>();
        }
    }
}
