using Application.Core.Entities.Concrete;
using Application.DataAccess.Entities;
using Application.DataAccess.Entities.Concrete;
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

        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<ApproveHistorie> ApproveHistories { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<EmployeeInfo> EmployeeInfos { get; set; }
        public DbSet<RemoteWork> RemoteWorks { get; set; }
        public DbSet<EmployeePermission> EmployeePermissions { get; set; }
        public DbSet<Day> Days { get; set; }
       


        /// <summary>
        /// provides normally Npgsql.
        /// </summary>
        /// <param name="optionsBuilder">DbContextOptionsBuilder instance.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }
        }
    }
}
