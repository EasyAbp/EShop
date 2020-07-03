using System;
using EasyAbp.EShop.Plugins.EntityFrameworkCore;
using EasyAbp.EShop.Orders.EntityFrameworkCore;
using EasyAbp.EShop.Payments.EntityFrameworkCore;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using EasyAbp.EShop.Stores.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace EasyAbp.EShop.EntityFrameworkCore
{
    public static class EShopDbContextModelCreatingExtensions
    {
        public static void ConfigureEShop(
            this ModelBuilder builder,
            Action<EShopModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new EShopModelBuilderConfigurationOptions(
                EShopDbProperties.DbTablePrefix,
                EShopDbProperties.DbSchema
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

            builder.ConfigureEShopOrders();
            builder.ConfigureEShopPayments();
            builder.ConfigureEShopPlugins();
            builder.ConfigureEShopProducts();
            builder.ConfigureEShopStores();
        }
    }
}