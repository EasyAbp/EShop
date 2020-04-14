using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Products.EntityFrameworkCore
{
    public class ProductsModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public ProductsModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}