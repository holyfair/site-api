using Microsoft.EntityFrameworkCore;
using Repository.DatabaseModels;
using Repository.DataStoreConfigurations;

namespace Repository.Context
{
    public class AppDbContext : DbContext
    {
        internal virtual DbSet<User> Users { get; set; }
        internal virtual DbSet<SourceRecord> Sources { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SourceConfiguration());
        }
    }
}