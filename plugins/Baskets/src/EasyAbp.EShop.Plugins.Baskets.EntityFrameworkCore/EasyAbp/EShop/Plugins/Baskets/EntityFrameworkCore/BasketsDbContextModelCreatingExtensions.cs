using EasyAbp.EShop.Plugins.Baskets.BasketItems;
using System;
using EasyAbp.EShop.Plugins.Baskets.ProductUpdates;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Plugins.Baskets.EntityFrameworkCore
{
    public static class BasketsDbContextModelCreatingExtensions
    {
        public static void ConfigureEShopPluginsBaskets(
            this ModelBuilder builder,
            Action<BasketsModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new BasketsModelBuilderConfigurationOptions(
                BasketsDbProperties.DbTablePrefix,
                BasketsDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */


            builder.Entity<BasketItem>(b =>
            {
                b.ToTable(options.TablePrefix + "BasketItems", options.Schema);
                b.ConfigureByConvention(); 

                /* Configure more properties here */

                b.HasIndex(x => x.UserId);
                b.Property(x => x.UnitPrice).HasColumnType("decimal(20,8)");
                b.Property(x => x.TotalPrice).HasColumnType("decimal(20,8)");
                b.Property(x => x.TotalDiscount).HasColumnType("decimal(20,8)");
            });


            builder.Entity<ProductUpdate>(b =>
            {
                b.ToTable(options.TablePrefix + "ProductUpdates", options.Schema);
                b.ConfigureByConvention(); 
                
                /* Configure more properties here */

                b.HasIndex(x => x.ProductSkuId);
            });
        }
    }
}
