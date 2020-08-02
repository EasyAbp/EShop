using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.StoreApproval.MongoDB
{
    public class StoreApprovalMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public StoreApprovalMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}