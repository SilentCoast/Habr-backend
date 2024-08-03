﻿using Habr.DataAccess.Constraints;
using Habr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Habr.WebApp.Endpoints
{
    public static class CommentEndpoints
    {
        public static void MapCommentEndpoints(this WebApplication app)
        {
            app.MapPost("/api/posts/{postId}/comments", [Authorize] async ([FromRoute] int postId,
                [FromBody][Required][MaxLength(ConstraintValue.CommentTextMaxLength)] string text,
                HttpContext httpContext, ICommentService commentService, CancellationToken cancellationToken) =>
            {
                await commentService.AddComment(text, postId, httpContext.GetUserId(), cancellationToken);
                return Results.StatusCode(StatusCodes.Status201Created);
            })
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout);

            app.MapPost("/api/posts/{postId}/comments/{commentId}/reply", [Authorize] async ([FromRoute] int postId,
                [FromRoute] int commentId, [FromBody][Required][MaxLength(ConstraintValue.CommentTextMaxLength)] string text,
                HttpContext httpContext, ICommentService commentService, CancellationToken cancellationToken) =>
            {
                await commentService.ReplyToComment(text, commentId, postId, httpContext.GetUserId(), cancellationToken);
                return Results.StatusCode(StatusCodes.Status201Created);
            })
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout);

            app.MapPut("/api/comments/{id}", [Authorize] async ([FromRoute] int id, [FromBody] string newText,
                HttpContext httpContext, ICommentService commentService, CancellationToken cancellationToken) =>
            {
                await commentService.EditComment(newText, id, httpContext.GetUserId(),
                    httpContext.GetUserRole(), cancellationToken);

                return Results.Ok();
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status408RequestTimeout);

            app.MapDelete("/api/comments/{id}", [Authorize] async ([FromRoute] int id, HttpContext httpContext,
                ICommentService commentService, CancellationToken cancellationToken) =>
            {
                await commentService.DeleteComment(id, httpContext.GetUserId(),
                    httpContext.GetUserRole(), cancellationToken);

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
