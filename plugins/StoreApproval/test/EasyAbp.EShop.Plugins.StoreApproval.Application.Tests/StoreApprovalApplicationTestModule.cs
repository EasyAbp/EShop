using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.StoreApproval
{
    [DependsOn(
        typeof(StoreApprovalApplicationModule),
        typeof(StoreApprovalDomainTestModule)
        )]
    public class StoreApprovalApplicationTestModule : AbpModule
    {

    }
}
