using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Orders.EntityFrameworkCore
{
    public class OrdersModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public OrdersModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}