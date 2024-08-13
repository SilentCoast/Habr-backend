using Habr.Services.Interfaces;

namespace Habr.WebApp.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            app.MapGet("/api/users/name", async (HttpContext httpContext, IUserService userService,
                CancellationToken cancellationToken = default) =>
            {
                var name = await userService.GetName(httpContext.GetUserId(), cancellationToken);
                return Results.Ok(name);
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithTags("Users")
            .WithDescription("Retrieves name of authorized user")
            .WithOpenApi();
        }
    }
}
