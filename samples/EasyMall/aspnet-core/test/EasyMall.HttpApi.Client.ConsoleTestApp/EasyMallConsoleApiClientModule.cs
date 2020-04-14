using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyMall.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(EasyMallHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class EasyMallConsoleApiClientModule : AbpModule
    {
        
    }
}
