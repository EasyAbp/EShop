using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.EntityFrameworkCore
{
    [ConnectionStringName(PluginsDbProperties.ConnectionStringName)]
    public class PluginsDbContext : AbpDbContext<PluginsDbContext>, IPluginsDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public PluginsDbContext(DbContextOptions<PluginsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureEShopPlugins();
        }
    }
}