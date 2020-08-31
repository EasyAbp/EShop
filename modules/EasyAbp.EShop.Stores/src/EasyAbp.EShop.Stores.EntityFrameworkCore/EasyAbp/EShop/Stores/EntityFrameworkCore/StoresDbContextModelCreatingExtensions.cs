using EasyAbp.EShop.Stores.Transactions;
using EasyAbp.EShop.Stores.Stores;
using System;
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


            builder.Entity<Transaction>(b =>
            {
                b.ToTable(options.TablePrefix + "Transactions", options.Schema);
                b.ConfigureByConvention();
                /* Configure more properties here */
                b.Property(x => x.Amount).HasColumnType("decimal(20,8)");
            });
        }
    }
}
