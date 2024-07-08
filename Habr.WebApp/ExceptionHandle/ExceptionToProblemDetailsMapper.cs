﻿using Habr.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Habr.WebApp.ExceptionHandle
{
    public class ExceptionToProblemDetailsMapper : IExceptionMapper
    {
        public ProblemDetails Map(Exception exception)
        {
            var problemDetails = new ProblemDetails()
            {
                Detail = exception.Message,

                Status = exception switch
                {
                    ArgumentException or InvalidOperationException
                    or ArgumentNullException or ArgumentOutOfRangeException => StatusCodes.Status400BadRequest,

                    LogInException => StatusCodes.Status401Unauthorized,

                    UnauthorizedAccessException => StatusCodes.Status403Forbidden,

                    OperationCanceledException => StatusCodes.Status408RequestTimeout,

                    _ => StatusCodes.Status500InternalServerError,
                }
            };

            return problemDetails;
        }
    }
}
