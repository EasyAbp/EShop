using EasyAbp.EShop.Stores;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Orders
{
    [DependsOn(
        typeof(EShopOrdersDomainSharedModule),
        typeof(EShopStoresDomainSharedModule)
        )]
    public class EShopOrdersDomainModule : AbpModule
    {

    }
}
