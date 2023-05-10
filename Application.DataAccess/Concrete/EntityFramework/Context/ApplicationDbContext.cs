using Application.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.DataAccess.Concrete.EntityFramework.Context
{
    /// <summary>
    /// DbContext contains database entities.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) { }

        public DbSet<Log> Logs { get; set; }
        public DbSet<App> Apps { get; set; }


        /// <summary>
        /// provides normally Npgsql.
        /// </summary>
        /// <param name="optionsBuilder">DbContextOptionsBuilder instance.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseNpgsql(_connectionString);
            }
        }
    }
}
