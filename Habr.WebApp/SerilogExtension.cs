using Serilog;

namespace Habr.WebApp
{
    public static class SerilogExtension
    {
        public static void AddSerilogLogging(this ConfigureHostBuilder host, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            host.UseSerilog();
        }
    }
}
