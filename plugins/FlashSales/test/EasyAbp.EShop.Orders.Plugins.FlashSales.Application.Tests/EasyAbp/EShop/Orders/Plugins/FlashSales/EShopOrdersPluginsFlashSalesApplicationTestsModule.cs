using EasyAbp.EShop.Plugins.FlashSales;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Orders.Plugins.FlashSales;

[DependsOn(
    typeof(EShopOrdersPluginsFlashSalesApplicationModule),
    typeof(EShopPluginsFlashSalesTestBaseModule)
    )]
public class EShopOrdersPluginsFlashSalesApplicationTestsModule : AbpModule
{

}
