using Habr.Services;
using Habr.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Habr.WebApp.MinimalApi.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/api/auth/login", async ([FromBody] UserLoginModel model, IUserService userService, JwtService jwtService, CancellationToken cancellationToken = default) =>
            {
                var userId = await userService.LogIn(model.Email, model.Password, cancellationToken);
                var token = jwtService.GenerateToken(userId);
                return Results.Ok(token);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status408RequestTimeout);

            app.MapPost("/api/auth/confirm-email", [Authorize] async ([FromBody] string email, HttpContext httpContext, IUserService userService, CancellationToken cancellationToken = default) =>
            {
                await userService.ConfirmEmail(email, httpContext.GetCurrentUserId(), cancellationToken);
                return Results.Ok();
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout);
        }
    }
}
