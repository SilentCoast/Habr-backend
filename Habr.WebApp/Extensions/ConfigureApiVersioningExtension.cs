using Asp.Versioning;

namespace Habr.WebApp.Extensions
{
    public static class ConfigureApiVersioningExtension
    {
        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            var builder = services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            builder.AddApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });
        }
    }
}
