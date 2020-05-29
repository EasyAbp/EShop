using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.MongoDB
{
    public class EShopMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public EShopMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}