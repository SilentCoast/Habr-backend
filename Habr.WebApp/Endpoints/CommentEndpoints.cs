﻿using Habr.DataAccess.Constraints;
using Habr.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Habr.WebApp.Endpoints
{
    public static class CommentEndpoints
    {
        public const string Tag = "Comments";
        public static void MapCommentEndpoints(this WebApplication app)
        {
            app.MapPost("/api/posts/{postId}/comments", async ([FromRoute] int postId,
                [FromBody][Required][MaxLength(ConstraintValue.CommentTextMaxLength)] string text,
                HttpContext httpContext, ICommentService commentService, CancellationToken cancellationToken) =>
            {
                await commentService.AddComment(text, postId, httpContext.GetUserId(), cancellationToken);
                return Results.StatusCode(StatusCodes.Status201Created);
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Adds a new comment to a post.")
            .WithOpenApi();

            app.MapPost("/api/posts/{postId}/comments/{commentId}/reply", async ([FromRoute] int postId,
                [FromRoute] int commentId, [FromBody][Required][MaxLength(ConstraintValue.CommentTextMaxLength)] string text,
                HttpContext httpContext, ICommentService commentService, CancellationToken cancellationToken) =>
            {
                await commentService.ReplyToComment(text, commentId, postId, httpContext.GetUserId(), cancellationToken);
                return Results.StatusCode(StatusCodes.Status201Created);
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Replies to an existing comment on a post.")
            .WithOpenApi();

            app.MapPut("/api/comments/{id}", async ([FromRoute] int id, [FromBody] string newText,
                HttpContext httpContext, ICommentService commentService, CancellationToken cancellationToken) =>
            {
                await commentService.EditComment(newText, id, httpContext.GetUserId(),
                    httpContext.GetUserRole(), cancellationToken);

                return Results.Ok();
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout)
            .WithTags(Tag)
            .WithDescription("Updates an existing comment.")
            .WithOpenApi();

            app.MapDelete("/api/comments/{id}", async ([FromRoute] int id, HttpContext httpContext,
                ICommentService commentService, CancellationToken cancellationToken) =>
            {
                await commentService.DeleteComment(id, httpContext.GetUserId(),
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
            .WithDescription("Deletes a comment.")
            .WithOpenApi();
        }
    }
}
