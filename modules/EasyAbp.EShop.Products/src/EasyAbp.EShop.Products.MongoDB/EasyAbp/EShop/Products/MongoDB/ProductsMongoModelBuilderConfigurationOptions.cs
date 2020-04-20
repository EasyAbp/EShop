using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Products.MongoDB
{
    public class ProductsMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public ProductsMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}