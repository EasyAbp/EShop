using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Baskets.EntityFrameworkCore
{
    public class BasketsModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public BasketsModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}