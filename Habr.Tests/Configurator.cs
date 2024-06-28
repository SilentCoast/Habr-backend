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
                       .SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"../../../../Habr.ConsoleApp")))
                       .AddJsonFile("appsettings.json")
                       .Build();

                return configuration;
            }
        }

        public static void ConfigureDbContextOptions(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("HabrDBConnection"));
        }
    }
}
