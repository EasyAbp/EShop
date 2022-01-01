using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Plugins.EntityFrameworkCore
{
    public class PluginsModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public PluginsModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}