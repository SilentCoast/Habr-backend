using Habr.DataAccess.Entities;
using Habr.Services;
using Habr.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Habr.WebApp.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/api/auth/login", async ([FromBody] UserLoginModel model, IUserService userService,
                IJwtService jwtService, IOptions<JsonSerializerOptions> jsonOptions,
                CancellationToken cancellationToken = default) =>
            {
                var tokensDTO = await userService.LogIn(model.Email, model.Password, cancellationToken);

                var json = JsonSerializer.Serialize(tokensDTO, jsonOptions.Value);
                return Results.Content(json, "application/json");
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status408RequestTimeout);

            app.MapPost("/api/auth/refresh", async ([FromBody] RefreshToken refreshToken, IJwtService jwtService,
                CancellationToken cancellationToken = default) =>
            {
                await jwtService.ValidateRefreshToken(refreshToken, cancellationToken);

                var accessToken = await jwtService.GenerateAccessToken(refreshToken.UserId);
                return Results.Ok(accessToken);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout);

            app.MapPost("/api/auth/confirm-email", [Authorize] async ([FromBody] string email, HttpContext httpContext,
                IUserService userService, CancellationToken cancellationToken = default) =>
            {
                await userService.ConfirmEmail(email, httpContext.GetUserId(), cancellationToken);
                return Results.Ok();
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout);
        }
    }
}
