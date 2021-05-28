using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Inventory.MongoDB
{
    public class InventoryMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public InventoryMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}