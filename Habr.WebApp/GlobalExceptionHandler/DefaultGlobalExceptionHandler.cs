using Microsoft.AspNetCore.Diagnostics;

namespace Habr.WebApp.GlobalExceptionHandler
{
    public class DefaultGlobalExceptionHandler : IExceptionHandler
    {
        private readonly IExceptionMapper _mapper;

        public DefaultGlobalExceptionHandler(IExceptionMapper mapper)
        {
            _mapper = mapper;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/problem+json";

            var problemDetails = _mapper.Map(exception);

            await Results.Problem(problemDetails).ExecuteAsync(httpContext);

            return true;
        }
    }
}
