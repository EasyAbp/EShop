using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.FlashSales;

[DependsOn(
    typeof(EShopPluginsFlashSalesApplicationModule),
    typeof(EShopPluginsFlashSalesDomainTestModule)
    )]
public class EShopPluginsFlashSalesApplicationTestModule : AbpModule
{

}
