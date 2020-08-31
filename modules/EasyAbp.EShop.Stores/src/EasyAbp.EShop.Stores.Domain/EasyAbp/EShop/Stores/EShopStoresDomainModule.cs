using EasyAbp.EShop.Orders;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Stores
{
    [DependsOn(
        typeof(EShopStoresDomainSharedModule),
        typeof(EShopOrdersDomainSharedModule)
    )]
    public class EShopStoresDomainModule : AbpModule
    {

    }
}
