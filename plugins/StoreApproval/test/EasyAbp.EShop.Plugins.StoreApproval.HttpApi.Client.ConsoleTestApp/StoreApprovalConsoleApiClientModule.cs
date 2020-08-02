using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.StoreApproval
{
    [DependsOn(
        typeof(StoreApprovalHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class StoreApprovalConsoleApiClientModule : AbpModule
    {
        
    }
}
