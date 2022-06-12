using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.FlashSales;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(FlashSalesDomainSharedModule)
)]
public class FlashSalesDomainModule : AbpModule
{

}
