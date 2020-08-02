using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.StoreApproval
{
    [DependsOn(
        typeof(StoreApprovalApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class StoreApprovalHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "StoreApproval";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(StoreApprovalApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
