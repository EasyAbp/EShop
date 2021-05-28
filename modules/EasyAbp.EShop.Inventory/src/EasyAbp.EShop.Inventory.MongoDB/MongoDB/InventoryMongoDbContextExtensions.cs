using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Inventory.MongoDB
{
    public static class InventoryMongoDbContextExtensions
    {
        public static void ConfigureInventory(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new InventoryMongoModelBuilderConfigurationOptions(
                InventoryDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}