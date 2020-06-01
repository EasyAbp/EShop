using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EShopSample.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(EShopSampleHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class EShopSampleConsoleApiClientModule : AbpModule
    {
        
    }
}
