using EasyAbp.EShop.Payments.Refunds;
using EasyAbp.EShop.Payments.Payments;
using System;
using EasyAbp.PaymentService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Payments.EntityFrameworkCore
{
    public static class PaymentsDbContextModelCreatingExtensions
    {
        public static void ConfigureEShopPayments(
            this ModelBuilder builder,
            Action<PaymentsModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new PaymentsModelBuilderConfigurationOptions(
                PaymentsDbProperties.DbTablePrefix,
                PaymentsDbProperties.DbSchema
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
            
            builder.ConfigurePaymentService();

            builder.Entity<Payment>(b =>
            {
                b.ToTable(options.TablePrefix + "Payments", options.Schema);
                b.ConfigureByConvention(); 
                /* Configure more properties here */
            });

            builder.Entity<Refund>(b =>
            {
                b.ToTable(options.TablePrefix + "Refunds", options.Schema);
                b.ConfigureByConvention(); 
                /* Configure more properties here */
            });
        }
    }
}
