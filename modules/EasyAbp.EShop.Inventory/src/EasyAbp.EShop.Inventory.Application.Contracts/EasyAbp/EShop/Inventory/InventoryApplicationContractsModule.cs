using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.EShop.Inventory
{
    [DependsOn(
        typeof(InventoryDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class InventoryApplicationContractsModule : AbpModule
    {

    }
}
