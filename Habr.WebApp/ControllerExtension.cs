using Microsoft.AspNetCore.Mvc;

namespace Habr.WebApp
{
    public static class ControllerExtension
    {
        /// <returns>userId from the JWT Token</returns>
        public static int GetCurrentUserId(this ControllerBase controller)
        {
            return int.Parse(controller.User.Claims.First(p => p.Type == "userId").Value);
        }
    }
}
