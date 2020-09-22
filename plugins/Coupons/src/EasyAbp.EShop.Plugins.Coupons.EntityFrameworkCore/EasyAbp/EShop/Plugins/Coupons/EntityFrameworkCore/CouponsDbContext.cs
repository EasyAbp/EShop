using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Coupons.EntityFrameworkCore
{
    [ConnectionStringName(CouponsDbProperties.ConnectionStringName)]
    public class CouponsDbContext : AbpDbContext<CouponsDbContext>, ICouponsDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public CouponsDbContext(DbContextOptions<CouponsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureEShopPluginsCoupons();
        }
    }
}