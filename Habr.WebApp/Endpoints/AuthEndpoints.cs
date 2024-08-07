﻿using Habr.Services.Interfaces;
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
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags("Authentication")
            .WithDescription("Logs in a user and returns an Access and Refresh tokens.");

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
            .WithDescription("Refreshes the Access token.");

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
            .WithDescription("Confirms a user's email address.");
        }
    }
}
