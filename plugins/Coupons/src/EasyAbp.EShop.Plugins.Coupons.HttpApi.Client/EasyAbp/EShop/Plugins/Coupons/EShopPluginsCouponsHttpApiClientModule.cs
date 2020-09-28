using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Coupons
{
    [DependsOn(
        typeof(EShopPluginsCouponsApplicationContractsModule),
        typeof(AbpHttpClientModule),
        typeof(AbpAutoMapperModule)
    )]
    public class EShopPluginsCouponsHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "EasyAbpEShopPluginsCoupons";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<EShopPluginsCouponsHttpApiClientModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<EShopPluginsCouponsHttpApiClientModule>(validate: true);
            });
            
            context.Services.AddHttpClientProxies(
                typeof(EShopPluginsCouponsApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
