using Habr.Services;
using Habr.WebApp.Extensions;
using Habr.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Habr.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> LogIn([FromBody] UserLoginModel model, CancellationToken cancellationToken = default)
        {
            var token = await _userService.LogIn(model.Email, model.Password, cancellationToken);
            return Ok(token);
        }

        [HttpPost("confirm-email")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        public async Task<IActionResult> ConfirmEmail([FromBody][EmailAddress] string email, CancellationToken cancellationToken = default)
        {
            await _userService.ConfirmEmail(email, this.GetCurrentUserId(), cancellationToken);
            return Ok();
        }
    }
}
