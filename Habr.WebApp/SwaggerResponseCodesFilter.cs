using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Habr.WebApp
{
    public class SwaggerResponseCodesFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Responses.TryGetValue("200", out OpenApiResponse okResponse))
            {
                okResponse.Description = "OK - The request was successful.";
            }

            if (operation.Responses.TryGetValue("201", out OpenApiResponse createdResponse))
            {
                createdResponse.Description = "Created - The resource was successfully created.";
            }

            if (operation.Responses.TryGetValue("204", out OpenApiResponse noContentResponse))
            {
                noContentResponse.Description = "No content - No content for requested filters.";
            }

            if (operation.Responses.TryGetValue("400", out OpenApiResponse badRequestResponse))
            {
                badRequestResponse.Description = "Bad request - The request is malformed or contains invalid data.";
            }

            if (operation.Responses.TryGetValue("403", out OpenApiResponse forbiddenResponse))
            {
                forbiddenResponse.Description = "Forbidden - User doesn't have permissions to requested resource";
            }

            if (operation.Responses.TryGetValue("404", out OpenApiResponse notFoundResponse))
            {
                notFoundResponse.Description = "Not found - The requested resource does not exist.";
            }

            if (operation.Responses.TryGetValue("408", out OpenApiResponse timeOutResponse))
            {
                timeOutResponse.Description = "Request Timeout - Request timed out or operation was cancelled.";
            }
        }
    }
}