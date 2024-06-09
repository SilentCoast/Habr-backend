using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Habr.DataAccess
{
    /// <summary>
    /// Used for design-time calls in Package Manager Console
    /// </summary>
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<DataContext>();
            var connectionString = configuration.GetConnectionString("HabrDBConnection");
            builder.UseSqlServer(connectionString);

            return new DataContext(builder.Options);
        }
    }
}
