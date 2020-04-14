using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Orders
{
    [DependsOn(
        typeof(EShopOrdersDomainSharedModule)
        )]
    public class EShopOrdersDomainModule : AbpModule
    {

    }
}
