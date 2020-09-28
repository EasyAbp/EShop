using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Plugins.Coupons.EntityFrameworkCore
{
    public class CouponsModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public CouponsModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}