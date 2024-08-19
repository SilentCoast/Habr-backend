using Asp.Versioning;
using Habr.Services.Exceptions;
using Habr.Services.Interfaces;
using Habr.Services.Resources;
using Habr.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Habr.WebApp.Endpoints
{
    public static class PostEndpoints
    {
        public const string Tag = "Posts";
        public static void MapPostEndpoints(this WebApplication app)
        {
            var apiVersion1 = new ApiVersion(1, 0);
            var apiVersion2 = new ApiVersion(2, 0);

            var apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(apiVersion1)
            .HasApiVersion(apiVersion2)
            .ReportApiVersions()
            .Build();

            app.MapGet("/api/posts/{id}", async ([FromRoute] int id, IPostService postService,
                IOptions<JsonSerializerOptions> jsonOptions, CancellationToken cancellationToken) =>
            {
                var post = await postService.GetPostView(id, cancellationToken);

                var json = JsonSerializer.Serialize(post, jsonOptions.Value);
                return Results.Content(json, "application/json");
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Retrieves a specific published post by its ID.")
            .WithOpenApi();

            app.MapGet("/api/posts/published", async (IPostService postService,
                CancellationToken cancellationToken) =>
            {
                var posts = await postService.GetPublishedPosts(cancellationToken);
                return Results.Ok(posts);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Retrieves all published posts. Version 1.0")
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(apiVersion1)
            .HasDeprecatedApiVersion(apiVersion1)
            .WithOpenApi();

            app.MapGet("/api/v{version:apiVersion}/posts/published", async (IPostService postService,
                CancellationToken cancellationToken) =>
            {
                var posts = await postService.GetPublishedPostsV2(cancellationToken);
                return Results.Ok(posts);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Retrieves all published posts. Version 2.0")
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(apiVersion2)
            .WithOpenApi();

            app.MapGet("/api/posts/published/paginated", async ([FromQuery] int pageNumber, [FromQuery] int pageSize,
                IPostService postService, CancellationToken cancellationToken) =>
            {
                //TODO: maybe put validation into service class
                Validator.ValidateIntMoreThan0(pageNumber, ExceptionMessage.PageNumberLessThan1);
                Validator.ValidateIntMoreThan0(pageSize, ExceptionMessage.PageSizeLessThan1);

                try
                {
                    var paginatedDto = await postService.GetPublishedPostsPaginated(pageNumber, pageSize, cancellationToken);

                    var json = JsonSerializer.Serialize(paginatedDto);
                    return Results.Content(json, "application/json");
                }
                catch (NothingToPaginateException)
                {
                    return Results.NoContent();
                }
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Retrieves published posts by page.");

            app.MapGet("/api/posts/drafted", async (HttpContext httpContext, IPostService postService,
                CancellationToken cancellationToken) =>
            {
                var posts = await postService.GetDraftedPosts(httpContext.GetUserId(), cancellationToken);
                if (posts.Any())
                {
                    return Results.Ok(posts);
                }
                else
                {
                    return Results.NoContent();
                }
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Retrieves all drafted posts.")
            .WithOpenApi();

            app.MapGet("/api/posts/drafted/paginated", async ([FromQuery] int pageNumber, [FromQuery] int pageSize,
                HttpContext httpContext, IPostService postService, CancellationToken cancellationToken) =>
            {
                //TODO: maybe put validation into service class
                Validator.ValidateIntMoreThan0(pageNumber, ExceptionMessage.PageNumberLessThan1);
                Validator.ValidateIntMoreThan0(pageSize, ExceptionMessage.PageSizeLessThan1);

                try
                {
                    var paginatedDto = await postService.GetDraftedPostsPaginated(httpContext.GetUserId(),
                        pageNumber, pageSize, cancellationToken);

                    return Results.Ok(paginatedDto);
                }
                catch (NothingToPaginateException)
                {
                    return Results.NoContent();
                }
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Retrieves all drafted posts by page.");

            app.MapPost("/api/posts", async ([FromBody] PostCreateModel model, HttpContext httpContext,
                IPostService postService, CancellationToken cancellationToken) =>
            {
                await postService.AddPost(model.Title, model.Text, httpContext.GetUserId(), model.IsPublishedNow, cancellationToken);
                return Results.StatusCode(StatusCodes.Status201Created);
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Creates a new post.")
            .WithOpenApi();

            app.MapPut("/api/posts/{id}", async ([FromRoute] int id, [FromBody] PostUpdateModel model,
                HttpContext httpContext, IPostService postService, CancellationToken cancellationToken) =>
            {
                await postService.UpdatePost(id, httpContext.GetUserId(), httpContext.GetUserRole(),
                    model.NewTitle, model.NewText, cancellationToken);

                return Results.Ok();
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Updates a specific post by its ID.")
            .WithOpenApi();

            app.MapPut("/api/posts/{id}/publish", async ([FromRoute] int id, HttpContext httpContext,
                IPostService postService, CancellationToken cancellationToken) =>
            {
                await postService.PublishPost(id, httpContext.GetUserId(),
                    httpContext.GetUserRole(), cancellationToken);

                return Results.Ok();
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Publishes a specific post by its ID.")
            .WithOpenApi();

            app.MapPut("/api/posts/{id}/unpublish", async ([FromRoute] int id, HttpContext httpContext,
                IPostService postService, CancellationToken cancellationToken) =>
            {
                await postService.UnpublishPost(id, httpContext.GetUserId(),
                    httpContext.GetUserRole(), cancellationToken);

                return Results.Ok();
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Unpublishes a specific post by its ID.")
            .WithOpenApi();

            app.MapDelete("/api/posts/{id}", async ([FromRoute] int id, HttpContext httpContext,
                IPostService postService, CancellationToken cancellationToken) =>
            {
                await postService.DeletePost(id, httpContext.GetUserId(), httpContext.GetUserRole(), cancellationToken);
                return Results.Ok();
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Deletes a specific post by its ID.")
            .WithOpenApi();

            app.MapPost("/api/posts/{id}/rate", async ([FromRoute] int id, [FromBody] PostRatingModel model, HttpContext httpContext,
                IPostRatingService postRatingService, CancellationToken cancelationToken) =>
            {
                await postRatingService.AddOrUpdatePostRating(model.RatingStars, id, httpContext.GetUserId(),
                    model.Description, cancelationToken);
                return Results.Created();
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription($"Adds or Updates post rating")
            .WithOpenApi();
        }
    }
}
