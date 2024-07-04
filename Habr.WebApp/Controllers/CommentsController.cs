using Habr.DataAccess.Constraints;
using Habr.Services;
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> AddComment([FromRoute] int postId,
            [FromBody][Required][MaxLength(ConstraintValue.CommentTextMaxLength)] string text,
            [FromHeader] int userId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _commentService.AddComment(text, postId, userId, cancellationToken);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status408RequestTimeout);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("~/api/posts/{postId}/comments/{commentId}/reply")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> ReplyToComment([FromRoute] int postId,
            [FromRoute] int commentId,
            [FromBody][Required][MaxLength(ConstraintValue.CommentTextMaxLength)] string text,
            [FromHeader] int userId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _commentService.ReplyToComment(text, commentId, postId, userId, cancellationToken);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status408RequestTimeout);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> EditComment([FromBody] string newText, [FromRoute] int id, [FromHeader] int userId, CancellationToken cancellationToken = default)
        {
            try
            {
                await _commentService.ModifyComment(newText, id, userId, cancellationToken);
                return Ok();
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status408RequestTimeout);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> DeleteComment([FromRoute] int id, [FromHeader] int userId, CancellationToken cancellationToken = default)
        {
            try
            {
                await _commentService.DeleteComment(id, userId, cancellationToken);
                return Ok();
            }
            catch (UnauthorizedAccessException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status408RequestTimeout);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
