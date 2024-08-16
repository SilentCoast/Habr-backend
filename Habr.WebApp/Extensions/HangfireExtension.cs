using Hangfire;

namespace Habr.WebApp.Extensions
{
    public static class HangfireExtension
    {
        public static void ConfigureHangfire(this IServiceCollection services, string connectionString)
        {
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString));

            services.AddHangfireServer();
        }
    }
}
