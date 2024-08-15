using Habr.Services.Interfaces;

namespace Habr.WebApp.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITokenRevocationService tokenService)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                await tokenService.CheckTokenRevocation(context.User);
            }

            await _next(context);
        }
    }
}
