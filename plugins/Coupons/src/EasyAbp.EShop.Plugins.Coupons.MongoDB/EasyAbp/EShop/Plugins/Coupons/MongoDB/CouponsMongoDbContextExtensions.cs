using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.Coupons.MongoDB
{
    public static class CouponsMongoDbContextExtensions
    {
        public static void ConfigureEShopPluginsCoupons(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new CouponsMongoModelBuilderConfigurationOptions(
                CouponsDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}