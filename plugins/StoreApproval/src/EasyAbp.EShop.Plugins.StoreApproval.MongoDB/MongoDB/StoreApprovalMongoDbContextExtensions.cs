using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.StoreApproval.MongoDB
{
    public static class StoreApprovalMongoDbContextExtensions
    {
        public static void ConfigureStoreApproval(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new StoreApprovalMongoModelBuilderConfigurationOptions(
                StoreApprovalDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}