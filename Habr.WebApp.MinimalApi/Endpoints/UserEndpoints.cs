using Habr.Services;
using Habr.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Habr.WebApp.MinimalApi.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            app.MapGet("/api/users/name", [Authorize] async (HttpContext httpContext, IUserService userService) =>
            {
                var userId = httpContext.GetCurrentUserId();
                var name = await userService.GetName(userId);
                return Results.Ok(name);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/api/users", async ([FromBody] UserCreateModel model, IUserService userService) =>
            {
                await userService.CreateUser(model.Email, model.Password, model.Name);
                return Results.StatusCode(StatusCodes.Status201Created);
            })
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
        }
    }
}
