using Asp.Versioning;
using Asp.Versioning.Builder;

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

        public static ApiVersionSet ConfigureApiVersionSet(this WebApplication app)
        {
            return app.NewApiVersionSet()
                .HasApiVersion(ApiVersions.ApiVersion1)
                .HasApiVersion(ApiVersions.ApiVersion2)
                .ReportApiVersions()
                .Build();
        }
    }
}
