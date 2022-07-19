using EasyAbp.EShop.Orders.Plugins.FlashSales;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.FlashSales;

[DependsOn(
    typeof(EShopOrdersPluginsFlashSalesApplicationModule),
    typeof(EShopPluginsFlashSalesTestBaseModule)
    )]
public class EShopOrdersPluginsFlashSalesApplicationTestsModule : AbpModule
{

}
