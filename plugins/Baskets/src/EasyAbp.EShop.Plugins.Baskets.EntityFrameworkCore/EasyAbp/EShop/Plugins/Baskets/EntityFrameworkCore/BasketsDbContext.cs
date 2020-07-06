using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Plugins.Baskets.BasketItems;
using EasyAbp.EShop.Plugins.Baskets.ProductUpdates;

namespace EasyAbp.EShop.Plugins.Baskets.EntityFrameworkCore
{
    [ConnectionStringName(BasketsDbProperties.ConnectionStringName)]
    public class BasketsDbContext : AbpDbContext<BasketsDbContext>, IBasketsDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<ProductUpdate> ProductUpdates { get; set; }

        public BasketsDbContext(DbContextOptions<BasketsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureEShopPluginsBaskets();
        }
    }
}
