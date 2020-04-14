using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Stores.EntityFrameworkCore
{
    public class StoresModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public StoresModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}