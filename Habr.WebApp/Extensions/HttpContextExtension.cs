using Habr.Services.Resources;

namespace Habr.WebApp
{
    public static class HttpContextExtensions
    {
        public static int GetCurrentUserId(this HttpContext httpContext)
        {
            var claim = httpContext.User.Claims.FirstOrDefault(p => p.Type == "userId")
                ?? throw new UnauthorizedAccessException(ExceptionMessage.TokenBreach);

            return int.Parse(claim.Value);
        }
    }
}
