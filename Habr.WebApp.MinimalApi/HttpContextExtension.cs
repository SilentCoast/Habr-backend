namespace Habr.WebApp.MinimalApi
{
    public static class HttpContextExtensions
    {
        public static int GetCurrentUserId(this HttpContext httpContext)
        {
            return int.Parse(httpContext.User.Claims.First(p => p.Type == "userId").Value);
        }
    }
}
