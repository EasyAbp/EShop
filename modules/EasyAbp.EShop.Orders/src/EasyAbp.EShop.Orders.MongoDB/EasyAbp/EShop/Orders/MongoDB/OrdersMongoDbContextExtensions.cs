using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Orders.MongoDB
{
    public static class OrdersMongoDbContextExtensions
    {
        public static void ConfigureEShopOrders(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new OrdersMongoModelBuilderConfigurationOptions(
                OrdersDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}