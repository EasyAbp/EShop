using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.EntityFrameworkCore
{
    public class EShopModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public EShopModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}