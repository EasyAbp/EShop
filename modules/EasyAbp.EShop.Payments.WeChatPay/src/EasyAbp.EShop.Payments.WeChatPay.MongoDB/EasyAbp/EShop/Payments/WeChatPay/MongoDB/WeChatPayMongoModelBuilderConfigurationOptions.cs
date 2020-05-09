using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Payments.WeChatPay.MongoDB
{
    public class WeChatPayMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public WeChatPayMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}