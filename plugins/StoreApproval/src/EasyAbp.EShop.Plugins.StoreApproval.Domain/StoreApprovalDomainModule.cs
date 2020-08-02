using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.StoreApproval
{
    [DependsOn(
        typeof(StoreApprovalDomainSharedModule)
        )]
    public class StoreApprovalDomainModule : AbpModule
    {

    }
}
