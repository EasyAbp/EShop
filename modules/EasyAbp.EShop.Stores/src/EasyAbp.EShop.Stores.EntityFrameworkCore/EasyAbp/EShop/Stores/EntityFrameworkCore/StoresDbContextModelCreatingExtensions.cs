using EasyAbp.EShop.Stores.Stores;
using System;
using EasyAbp.EShop.Stores.StoreOwners;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Stores.EntityFrameworkCore
{
    public static class StoresDbContextModelCreatingExtensions
    {
        public static void ConfigureEShopStores(
            this ModelBuilder builder,
            Action<StoresModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new StoresModelBuilderConfigurationOptions(
                StoresDbProperties.DbTablePrefix,
                StoresDbProperties.DbSchema
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

            builder.Entity<Store>(b =>
            {
                b.ToTable(options.TablePrefix + "Stores", options.Schema);
                b.ConfigureByConvention(); 
                /* Configure more properties here */
            });

            builder.Entity<StoreOwner>(b =>
            {
                b.ToTable(options.TablePrefix + "StoreOwners", options.Schema);
                b.ConfigureByConvention();
                /* Configure more properties here */
            });
        }
    }
}
