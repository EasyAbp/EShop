using EasyAbp.EShop.Inventory.Suppliers;
using EasyAbp.EShop.Inventory.Instocks;
using EasyAbp.EShop.Inventory.Outstocks;
using EasyAbp.EShop.Inventory.Reallocations;
using EasyAbp.EShop.Inventory.StockHistories;
using EasyAbp.EShop.Inventory.Stocks;
using EasyAbp.EShop.Inventory.Warehouses;
using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Inventory.EntityFrameworkCore
{
    public static class InventoryDbContextModelCreatingExtensions
    {
        public static void ConfigureInventory(
            this ModelBuilder builder,
            Action<InventoryModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new InventoryModelBuilderConfigurationOptions(
                InventoryDbProperties.DbTablePrefix,
                InventoryDbProperties.DbSchema
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

            builder.Entity<Instock>(b =>
            {
                b.ToTable(options.TablePrefix + "Instocks", options.Schema);
                b.ConfigureByConvention();

                /* Configure more properties here */
            });

            builder.Entity<Outstock>(b =>
            {
                b.ToTable(options.TablePrefix + "Outstocks", options.Schema);
                b.ConfigureByConvention();

                /* Configure more properties here */
            });

            builder.Entity<Reallocation>(b =>
            {
                b.ToTable(options.TablePrefix + "Reallocations", options.Schema);
                b.ConfigureByConvention();

                /* Configure more properties here */
            });

            builder.Entity<StockHistory>(b =>
            {
                b.ToTable(options.TablePrefix + "InventoryHistories", options.Schema);
                b.ConfigureByConvention();

                /* Configure more properties here */
            });

            builder.Entity<Warehouse>(b =>
            {
                b.ToTable(options.TablePrefix + "Warehouses", options.Schema);
                b.ConfigureByConvention();

                b.OwnsOne(b => b.Address);

                /* Configure more properties here */
            });

            builder.Entity<Stock>(b =>
            {
                b.ToTable(options.TablePrefix + "Stocks", options.Schema);
                b.ConfigureByConvention();

                /* Configure more properties here */
            });

            builder.Entity<StockHistory>(b =>
            {
                b.ToTable(options.TablePrefix + "StockHistories", options.Schema);
                b.ConfigureByConvention();

                /* Configure more properties here */
            });


            builder.Entity<Supplier>(b =>
            {
                b.ToTable(options.TablePrefix + "Suppliers", options.Schema);
                b.ConfigureByConvention(); 
                

                /* Configure more properties here */
            });
        }
    }
}
