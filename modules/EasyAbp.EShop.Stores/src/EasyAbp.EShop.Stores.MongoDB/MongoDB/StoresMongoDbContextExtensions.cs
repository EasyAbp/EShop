using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Stores.MongoDB
{
    public static class StoresMongoDbContextExtensions
    {
        public static void ConfigureStores(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new StoresMongoModelBuilderConfigurationOptions(
                StoresDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}