using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Baskets.MongoDB
{
    public static class BasketsMongoDbContextExtensions
    {
        public static void ConfigureBaskets(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new BasketsMongoModelBuilderConfigurationOptions(
                BasketsDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}