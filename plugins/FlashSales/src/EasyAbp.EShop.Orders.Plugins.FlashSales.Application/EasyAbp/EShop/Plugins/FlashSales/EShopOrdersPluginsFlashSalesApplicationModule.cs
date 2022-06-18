using EasyAbp.EShop.Orders;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.FlashSales;

[DependsOn(
    typeof(EShopOrdersApplicationModule),
    typeof(EShopPluginsFlashSalesApplicationContractsModule)
)]
public class EShopOrdersPluginsFlashSalesApplicationModule : AbpModule
{
}
