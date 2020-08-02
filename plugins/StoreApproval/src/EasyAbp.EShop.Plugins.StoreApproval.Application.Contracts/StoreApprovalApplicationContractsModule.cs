using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.EShop.Plugins.StoreApproval
{
    [DependsOn(
        typeof(StoreApprovalDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class StoreApprovalApplicationContractsModule : AbpModule
    {

    }
}
