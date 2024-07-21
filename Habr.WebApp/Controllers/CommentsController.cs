using Habr.DataAccess.Constraints;
using Habr.Services;
using Habr.WebApp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Habr.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [Route("~/api/posts/{postId}/comments")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> AddComment([FromRoute] int postId,
            [FromBody][Required][MaxLength(ConstraintValue.CommentTextMaxLength)] string text,
            CancellationToken cancellationToken = default)
        {
            await _commentService.AddComment(text, postId, this.GetCurrentUserId(), cancellationToken);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost]
        [Route("~/api/posts/{postId}/comments/{commentId}/reply")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> ReplyToComment([FromRoute] int postId,
            [FromRoute] int commentId,
            [FromBody][Required][MaxLength(ConstraintValue.CommentTextMaxLength)] string text,
            CancellationToken cancellationToken = default)
        {
            await _commentService.ReplyToComment(text, commentId, postId, this.GetCurrentUserId(), cancellationToken);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> EditComment([FromBody] string newText, [FromRoute] int id, CancellationToken cancellationToken = default)
        {
            await _commentService.ModifyComment(newText, id, this.GetCurrentUserId(), cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> DeleteComment([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            await _commentService.DeleteComment(id, this.GetCurrentUserId(), cancellationToken);
            return Ok();
        }
    }
}
