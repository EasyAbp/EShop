using System;
using EasyAbp.EShop.Baskets.MongoDB;
using EasyAbp.EShop.Orders.MongoDB;
using EasyAbp.EShop.Payments.MongoDB;
using EasyAbp.EShop.Products.MongoDB;
using EasyAbp.EShop.Stores.MongoDB;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.MongoDB
{
    public static class EShopMongoDbContextExtensions
    {
        public static void ConfigureEShop(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new EShopMongoModelBuilderConfigurationOptions(
                EShopDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
            
            builder.ConfigureEShopBaskets();
            builder.ConfigureEShopOrders();
            builder.ConfigureEShopPayments();
            builder.ConfigureEShopProducts();
            builder.ConfigureEShopStores();
        }
    }
}