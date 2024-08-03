using Microsoft.AspNetCore.Mvc;

namespace Habr.WebApp.GlobalExceptionHandler
{
    public interface IExceptionMapper
    {
        ProblemDetails Map(Exception exception);
    }
}
