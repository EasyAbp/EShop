using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Plugins.StoreApproval.EntityFrameworkCore
{
    public class StoreApprovalModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public StoreApprovalModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}