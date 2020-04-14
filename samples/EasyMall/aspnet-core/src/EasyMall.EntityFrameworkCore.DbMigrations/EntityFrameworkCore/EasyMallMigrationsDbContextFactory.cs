using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EasyMall.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class EasyMallMigrationsDbContextFactory : IDesignTimeDbContextFactory<EasyMallMigrationsDbContext>
    {
        public EasyMallMigrationsDbContext CreateDbContext(string[] args)
        {
            EasyMallEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<EasyMallMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new EasyMallMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
