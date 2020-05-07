using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Payments.EntityFrameworkCore
{
    public class PaymentsModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public PaymentsModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}