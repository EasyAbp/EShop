using System;
using EasyAbp.PaymentService.MongoDB;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Payments.MongoDB
{
    public static class PaymentsMongoDbContextExtensions
    {
        public static void ConfigureEShopPayments(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new PaymentsMongoModelBuilderConfigurationOptions(
                PaymentsDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
            
            builder.ConfigurePaymentService();
        }
    }
}