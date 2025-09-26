using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SecilStore.Data.Context
{
    public class ConfigDbContextFactory
    {
        public ConfigDbContextFactory() { }

        public ConfigDbContext CreateDbContext()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ConfigDbContext>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("connstr"), options =>
                options.UseNetTopologySuite());

            return new ConfigDbContext(optionsBuilder.Options);
        }
    }
}
