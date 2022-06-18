using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.FlashSales;

[DependsOn(
    typeof(EShopPluginsFlashSalesApplicationModule),
    typeof(FlashSalesDomainTestModule)
    )]
public class FlashSalesApplicationTestModule : AbpModule
{

}
