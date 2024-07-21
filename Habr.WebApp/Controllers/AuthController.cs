using Habr.Services;
using Habr.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Habr.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> LogIn([FromBody] UserLoginModel model, CancellationToken cancellationToken = default)
        {
            var userId = await _userService.LogIn(model.Email, model.Password, cancellationToken);
            return Ok(userId);
        }

        [HttpPost("confirm-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> ConfirmEmail([FromBody][EmailAddress] string email, [FromHeader] int userId, CancellationToken cancellationToken = default)
        {
            await _userService.ConfirmEmail(email, userId, cancellationToken);
            return Ok();
        }
    }
}
