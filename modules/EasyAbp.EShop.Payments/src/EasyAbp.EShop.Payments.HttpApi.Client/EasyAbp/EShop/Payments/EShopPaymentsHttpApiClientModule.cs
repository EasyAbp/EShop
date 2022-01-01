using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Payments
{
    [DependsOn(
        typeof(EShopPaymentsApplicationContractsModule),
        typeof(AbpHttpClientModule)
    )]
    public class EShopPaymentsHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = EShopPaymentsRemoteServiceConsts.RemoteServiceName;

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EShopPaymentsApplicationContractsModule).Assembly,
                RemoteServiceName
            );
            
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopPaymentsApplicationContractsModule>();
            });
        }
    }
}
