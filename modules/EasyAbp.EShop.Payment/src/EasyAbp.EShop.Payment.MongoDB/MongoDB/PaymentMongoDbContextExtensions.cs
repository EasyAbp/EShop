using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Payment.MongoDB
{
    public static class PaymentMongoDbContextExtensions
    {
        public static void ConfigurePayment(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new PaymentMongoModelBuilderConfigurationOptions(
                PaymentDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}