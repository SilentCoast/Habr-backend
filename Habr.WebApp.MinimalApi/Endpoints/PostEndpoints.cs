using Habr.Services;
using Habr.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Habr.WebApp.MinimalApi.Endpoints
{
    public static class PostEndpoints
    {
        public static void MapPostEndpoints(this WebApplication app)
        {
            app.MapGet("/api/posts/{id}", async ([FromRoute] int id, IPostService postService, CancellationToken cancellationToken) =>
            {
                var post = await postService.GetPostView(id, cancellationToken);
                return Results.Ok(post);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status408RequestTimeout);

            app.MapGet("/api/posts/published", async (IPostService postService, CancellationToken cancellationToken) =>
            {
                var posts = await postService.GetPublishedPosts(cancellationToken);
                return Results.Ok(posts);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status408RequestTimeout);

            app.MapGet("/api/posts/drafted", [Authorize] async (HttpContext httpContext, IPostService postService, 
                CancellationToken cancellationToken) =>
            {
                var userId = httpContext.GetCurrentUserId();
                var posts = await postService.GetDraftedPosts(userId, cancellationToken);
                if (posts.Any())
                {
                    return Results.Ok(posts);
                }
                else
                {
                    return Results.NoContent();
                }
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout);

            app.MapPost("/api/posts", [Authorize] async ([FromBody] PostCreateModel model, HttpContext httpContext, 
                IPostService postService, CancellationToken cancellationToken) =>
            {
                var userId = httpContext.GetCurrentUserId();
                await postService.AddPost(model.Title, model.Text, userId, model.IsPublishedNow, cancellationToken);
                return Results.StatusCode(StatusCodes.Status201Created);
            })
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout);

            app.MapPut("/api/posts/{id}", [Authorize] async ([FromRoute] int id, [FromBody] PostUpdateModel model, 
                HttpContext httpContext, IPostService postService, CancellationToken cancellationToken) =>
            {
                var userId = httpContext.GetCurrentUserId();
                await postService.UpdatePost(id, userId, model.NewTitle, model.NewText, cancellationToken);
                return Results.Ok();
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status408RequestTimeout);

            app.MapPut("/api/posts/{id}/publish", [Authorize] async ([FromRoute] int id, HttpContext httpContext, 
                IPostService postService, CancellationToken cancellationToken) =>
            {
                var userId = httpContext.GetCurrentUserId();
                await postService.PublishPost(id, userId, cancellationToken);
                return Results.Ok();
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status408RequestTimeout);

            app.MapPut("/api/posts/{id}/unpublish", [Authorize] async ([FromRoute] int id, HttpContext httpContext, 
                IPostService postService, CancellationToken cancellationToken) =>
            {
                var userId = httpContext.GetCurrentUserId();
                await postService.UnpublishPost(id, userId, cancellationToken);
                return Results.Ok();
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status408RequestTimeout);

            app.MapDelete("/api/posts/{id}", [Authorize] async ([FromRoute] int id, HttpContext httpContext, 
                IPostService postService, CancellationToken cancellationToken) =>
            {
                var userId = httpContext.GetCurrentUserId();
                await postService.DeletePost(id, userId, cancellationToken);
                return Results.Ok();
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status408RequestTimeout);
        }
    }
}
