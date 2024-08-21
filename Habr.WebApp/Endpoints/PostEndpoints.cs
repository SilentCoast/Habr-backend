using Asp.Versioning.Builder;
using FluentValidation;
using Habr.DataAccess.Constraints;
using Habr.DataAccess.DTOs;
using Habr.Services.Interfaces;
using Habr.Services.Pagination;
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
        public static void MapPostEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGet("/api/posts/published/{id}", async ([FromRoute] int id, IPostService postService,
                IOptions<JsonSerializerOptions> jsonOptions, CancellationToken cancellationToken) =>
            {
                var post = await postService.GetPostView(id, cancellationToken);

                var json = JsonSerializer.Serialize(post, jsonOptions.Value);
                return Results.Content(json, "application/json");
            })
            .Produces<PostViewDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Retrieves a specific published post by its ID.")
            .WithOpenApi();

            app.MapGet("/api/v{version:apiVersion}/posts/published", async (IPostService postService,
                CancellationToken cancellationToken) =>
            {
                var posts = await postService.GetPublishedPosts(cancellationToken);
                return Results.Ok(posts);
            })
            .Produces<IEnumerable<PublishedPostDto>>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Retrieves all published posts. Version 1.0")
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(ApiVersions.ApiVersion1)
            .HasDeprecatedApiVersion(ApiVersions.ApiVersion1)
            .WithOpenApi();

            app.MapGet("/api/v{version:apiVersion}/posts/published", async (IPostService postService,
                CancellationToken cancellationToken) =>
            {
                var posts = await postService.GetPublishedPostsV2(cancellationToken);
                return Results.Ok(posts);
            })
            .Produces<IEnumerable<PublishedPostV2Dto>>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Retrieves all published posts. Version 2.0")
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(ApiVersions.ApiVersion2)
            .WithOpenApi();

            app.MapGet("/api/posts/published/paginated", async ([AsParameters]PaginationParams model,
                IValidator<PaginationParams> validator,
                IPostService postService, CancellationToken cancellationToken) =>
            {
                var validationResult = await validator.ValidateAsync(model, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                    return Results.BadRequest(errors);
                }

                var paginatedDto = await postService.GetPublishedPostsPaginated(model.PageNumber, (int)model.PageSize, cancellationToken);
                
                var json = JsonSerializer.Serialize(paginatedDto);
                return Results.Content(json, "application/json");
            })
            .Produces<PaginatedDto<PublishedPostV2Dto>>()
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
            .Produces<IEnumerable<DraftedPostDto>>()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Retrieves all drafted posts.")
            .WithOpenApi();

            app.MapGet("/api/posts/drafted/{id}", async ([FromRoute] int id, HttpContext httpContext, IPostService postService,
                CancellationToken cancellationToken) =>
            {
                var post = await postService.GetDraftedPostView(id, httpContext.GetUserId(), cancellationToken);
                return Results.Ok(post);
            })
            .RequireAuthorization()
            .Produces<DraftedPostViewDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Retrieves a specific drafted post by its ID")
            .WithOpenApi();

            app.MapGet("/api/posts/drafted/paginated", async ([AsParameters] PaginationParams model,
                IValidator<PaginationParams> validator,
                HttpContext httpContext, IPostService postService, CancellationToken cancellationToken) =>
            {
                var validationResult = await validator.ValidateAsync(model, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                    return Results.BadRequest(errors);
                }

                var paginatedDto = await postService.GetDraftedPostsPaginated(httpContext.GetUserId(),
                        model.PageNumber, (int)model.PageSize, cancellationToken);

                return Results.Ok(paginatedDto);
            })
            .RequireAuthorization()
            .Produces<PaginatedDto<DraftedPostDto>>()
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

            app.MapPost("/api/posts/{id}/rate", async ([FromRoute] int id, [FromBody] int ratingStars, HttpContext httpContext,
                IPostRatingService postRatingService, CancellationToken cancelationToken) =>
            {
                if (ratingStars < ConstraintValue.PostRatingStarsMin || ratingStars > ConstraintValue.PostRatingStarsMax)
                {
                    return Results.BadRequest(string.Format(ExceptionMessageGeneric.RatingOutOfBounds,
                        ConstraintValue.PostRatingStarsMin, ConstraintValue.PostRatingStarsMax));
                }

                await postRatingService.AddOrUpdatePostRating(ratingStars, id, 
                    httpContext.GetUserId(), cancelationToken);
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
