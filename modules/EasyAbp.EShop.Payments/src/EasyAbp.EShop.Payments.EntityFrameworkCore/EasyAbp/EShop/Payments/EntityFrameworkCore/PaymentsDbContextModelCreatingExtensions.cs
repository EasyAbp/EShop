using System;
using EasyAbp.PaymentService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

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
            
            // Configure dependent module
            
            builder.ConfigurePaymentService();
        }
    }
}
