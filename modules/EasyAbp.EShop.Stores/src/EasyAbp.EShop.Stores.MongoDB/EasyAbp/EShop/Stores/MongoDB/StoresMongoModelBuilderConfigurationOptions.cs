using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Stores.MongoDB
{
    public class StoresMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public StoresMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}