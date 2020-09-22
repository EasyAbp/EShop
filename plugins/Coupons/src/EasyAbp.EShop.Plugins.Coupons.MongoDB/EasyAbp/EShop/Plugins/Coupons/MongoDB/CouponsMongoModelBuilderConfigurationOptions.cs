using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.Coupons.MongoDB
{
    public class CouponsMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public CouponsMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}