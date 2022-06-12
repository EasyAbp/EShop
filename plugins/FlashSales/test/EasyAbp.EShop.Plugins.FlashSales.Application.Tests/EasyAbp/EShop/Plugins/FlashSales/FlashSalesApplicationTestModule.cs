using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.FlashSales;

[DependsOn(
    typeof(FlashSalesApplicationModule),
    typeof(FlashSalesDomainTestModule)
    )]
public class FlashSalesApplicationTestModule : AbpModule
{

}
