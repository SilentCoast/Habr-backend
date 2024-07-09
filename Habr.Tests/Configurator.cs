using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Habr.Tests
{
    public static class Configurator
    {
        private static IConfigurationRoot? configuration;
        public static IConfigurationRoot Configuration
        {
            get
            {
                configuration ??= new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json")
                       .Build();

                return configuration;
            }
        }

        public static void ConfigureDbContextOptions(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("HabrDBTestConnection"));
        }
    }
}
