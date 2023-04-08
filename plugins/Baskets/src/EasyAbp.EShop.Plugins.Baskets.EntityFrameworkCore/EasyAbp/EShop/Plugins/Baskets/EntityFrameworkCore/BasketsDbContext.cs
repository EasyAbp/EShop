using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Plugins.Baskets.BasketItems;
using EasyAbp.EShop.Plugins.Baskets.EntityFrameworkCore.ValueMappings;
using EasyAbp.EShop.Plugins.Baskets.ProductUpdates;
using EasyAbp.EShop.Products.Products;

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

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<List<ProductDiscountInfoModel>>()
                .HaveConversion<ProductDiscountsInfoValueConverter>();
            configurationBuilder.Properties<List<OrderDiscountPreviewInfoModel>>()
                .HaveConversion<OrderDiscountPreviewsInfoValueConverter>();
        }
    }
}