using Microsoft.AspNetCore.Mvc;

namespace Habr.WebApp.ExceptionHandle
{
    public interface IExceptionMapper
    {
        ProblemDetails Map(Exception exception);
    }
}
