using Habr.Services.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Habr.WebApp.Extensions
{
    public static class ControllerExtension
    {
        /// <returns>userId from the JWT Token</returns>
        public static int GetCurrentUserId(this ControllerBase controller)
        {
            var claim = controller.User.Claims.FirstOrDefault(p => p.Type == "userId")
                ?? throw new UnauthorizedAccessException(ExceptionMessage.TokenBreach);

            return int.Parse(claim.Value);
        }
    }
}
