using Habr.Services.Interfaces;
using Habr.WebApp.Models;
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
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags("Authentication")
            .WithDescription("Logs in a user and returns an Access and Refresh tokens.")
            .WithOpenApi();

            app.MapPost("/api/auth/register", async ([FromBody] UserCreateModel model, IUserService userService,
                CancellationToken cancellationToken = default) =>
            {
                await userService.CreateUser(model.Email, model.Password, model.Name, cancellationToken);
                var tokens = await userService.LogIn(model.Email, model.Password, cancellationToken);

                return Results.Created(string.Empty, tokens);
            })
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags("Users")
            .WithDescription("Registers a new user. Returns Refresh and Access tokens")
            .WithOpenApi();

            app.MapPost("/api/auth/refresh", async ([FromBody] string refreshToken, IJwtService jwtService,
                CancellationToken cancellationToken = default) =>
            {
                var accessToken = await jwtService.RefreshAccessToken(refreshToken, cancellationToken);
                return Results.Ok(accessToken);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags("Authentication")
            .WithDescription("Refreshes the Access token.")
            .WithOpenApi();

            app.MapPost("/api/auth/confirm-email", async ([FromBody] string email, HttpContext httpContext,
                IUserService userService, CancellationToken cancellationToken = default) =>
            {
                await userService.ConfirmEmail(email, httpContext.GetUserId(), cancellationToken);
                return Results.Ok();
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags("Authentication")
            .WithDescription("Confirms a user's email address.")
            .WithOpenApi();

            app.MapPost("/api/auth/revoke", async ([FromBody] int userId, ITokenRevocationService tokenRevocationService,
                CancellationToken cancellationToken = default) =>
            {
                await tokenRevocationService.RevokeAllUserTokens(userId, cancellationToken);
                return Results.Ok();
            })
            .RequireAuthorization(AuthPolicies.AdminPolicy)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status403Forbidden)
            .WithTags("Authentication")
            .WithDescription("Revokes all user Access and Refresh tokens")
            .WithOpenApi();
        }
    }
}
