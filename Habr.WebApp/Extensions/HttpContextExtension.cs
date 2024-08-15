using Habr.DataAccess.Enums;
using Habr.Services.Resources;
using System.Security.Claims;

namespace Habr.WebApp
{
    public static class HttpContextExtensions
    {
        public static int GetUserId(this HttpContext httpContext)
        {
            var claim = httpContext.User.Claims.SingleOrDefault(p => p.Type == "userId")
                ?? throw new UnauthorizedAccessException(ExceptionMessage.TokenBreach);

            return int.Parse(claim.Value);
        }

        public static RoleType GetUserRole(this HttpContext httpContext)
        {
            var claim = httpContext.User.Claims.SingleOrDefault(p => p.Type == ClaimTypes.Role)
                ?? throw new UnauthorizedAccessException(ExceptionMessage.TokenBreach);

            if (Enum.TryParse(claim.Value, out RoleType roleType))
            {
                return roleType;
            }
            throw new InvalidOperationException(ExceptionMessage.InvalidRoleType);
        }
    }
}
