using EasyAbp.EShop.Plugins;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Payments;
using EasyAbp.EShop.Products;
using EasyAbp.EShop.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace EasyAbp.EShop
{
    [DependsOn(
        typeof(EShopDomainModule),
        typeof(EShopApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(EShopOrdersApplicationModule),
        typeof(EShopPaymentsApplicationModule),
        typeof(EShopPluginsApplicationModule),
        typeof(EShopProductsApplicationModule),
        typeof(EShopStoresApplicationModule)
        )]
    public class EShopApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<EShopApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<EShopApplicationModule>(validate: true);
            });
        }
    }
}
