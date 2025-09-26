using Microsoft.EntityFrameworkCore;
using SecilStore.Data.Model;

namespace SecilStore.Data.Context
{
    public class ConfigDbContext : DbContext
    {
        public ConfigDbContext() { }
        public ConfigDbContext(DbContextOptions<ConfigDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public virtual DbSet<ConfigurationItem> ConfigurationItem { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
