using Habr.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Habr.DataAccess
{
    public class DataContext : DbContext
    {
<<<<<<< HEAD
        public static readonly ILoggerFactory ConsoleLoggerFactory =
            LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Information);
            });

=======
        public DbSet<User> Users { get; set; }
>>>>>>> NB2024-45; User, Post, Comment relationships configured;
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(ConsoleLoggerFactory);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
    }
}
