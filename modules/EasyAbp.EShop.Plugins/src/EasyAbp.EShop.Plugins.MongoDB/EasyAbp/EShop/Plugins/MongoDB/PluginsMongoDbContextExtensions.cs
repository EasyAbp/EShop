using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.MongoDB
{
    public static class PluginsMongoDbContextExtensions
    {
        public static void ConfigureEShopPlugins(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new PluginsMongoModelBuilderConfigurationOptions(
                PluginsDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}