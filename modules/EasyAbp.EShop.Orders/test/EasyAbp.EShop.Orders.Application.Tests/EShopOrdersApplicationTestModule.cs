using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Orders
{
    [DependsOn(
        typeof(EShopOrdersApplicationModule),
        typeof(EShopOrdersDomainTestModule)
        )]
    public class EShopOrdersApplicationTestModule : AbpModule
    {

    }
}
