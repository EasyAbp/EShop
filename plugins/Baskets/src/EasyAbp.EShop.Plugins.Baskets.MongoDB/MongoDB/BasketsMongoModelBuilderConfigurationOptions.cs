using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.Baskets.MongoDB
{
    public class BasketsMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public BasketsMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}