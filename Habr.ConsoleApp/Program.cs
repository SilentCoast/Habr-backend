using Habr.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Habr.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set up configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Set up DI
            var serviceProvider = new ServiceCollection()
                .AddDbContext<DataContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("HabrDBConnection")))
                .BuildServiceProvider();

            using var context = serviceProvider.GetService<DataContext>();
            // Ensure database is created
            if (context != null)
            {
                context.Database.Migrate();
                Console.WriteLine("Database created.");
            }
            else
            {
                Console.WriteLine("Failed to connect to Database.");
            }
            Console.ReadKey();

        }
    }
}
