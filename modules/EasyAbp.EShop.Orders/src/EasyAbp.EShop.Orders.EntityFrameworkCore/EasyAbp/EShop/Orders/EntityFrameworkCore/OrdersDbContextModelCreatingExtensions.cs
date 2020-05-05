using EasyAbp.EShop.Orders.Orders;
using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Orders.EntityFrameworkCore
{
    public static class OrdersDbContextModelCreatingExtensions
    {
        public static void ConfigureOrders(
            this ModelBuilder builder,
            Action<OrdersModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new OrdersModelBuilderConfigurationOptions(
                OrdersDbProperties.DbTablePrefix,
                OrdersDbProperties.DbSchema
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

            builder.Entity<Order>(b =>
            {
                b.ToTable(options.TablePrefix + "Orders", options.Schema);
                b.ConfigureByConvention(); 
                /* Configure more properties here */
            });
            
            builder.Entity<OrderLine>(b =>
            {
                b.ToTable(options.TablePrefix + "OrderLines", options.Schema);
                b.ConfigureByConvention(); 
                /* Configure more properties here */
            });
        }
    }
}
