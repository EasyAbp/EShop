using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.FlashSales;

[DependsOn(
    typeof(EShopOrdersApplicationModule),
    typeof(EShopProductsApplicationContractsModule),
    typeof(EShopPluginsFlashSalesApplicationContractsModule)
)]
public class EShopOrdersPluginsFlashSalesApplicationModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IAuthorizationHandler, FlashSalesOrderCreationAuthorizationHandler>();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<EShopOrdersPluginsFlashSalesApplicationAutoMapperProfile>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.Configurators.Add(abpAutoMapperConfigurationContext =>
            {
                var profile = abpAutoMapperConfigurationContext.ServiceProvider
                    .GetRequiredService<EShopOrdersPluginsFlashSalesApplicationAutoMapperProfile>();

                abpAutoMapperConfigurationContext.MapperConfiguration.AddProfile(profile);
            });
        });
    }
}
