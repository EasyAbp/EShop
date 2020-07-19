using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Authorization;

namespace EasyAbp.EShop.Stores
{
    [DependsOn(
        typeof(EShopStoresDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class EShopStoresApplicationContractsModule : AbpModule
    {

    }
}
