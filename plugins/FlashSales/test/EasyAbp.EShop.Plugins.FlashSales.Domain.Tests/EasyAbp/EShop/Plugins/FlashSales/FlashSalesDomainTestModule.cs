using EasyAbp.EShop.Plugins.FlashSales.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.FlashSales;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(FlashSalesEntityFrameworkCoreTestModule)
    )]
public class FlashSalesDomainTestModule : AbpModule
{

}
