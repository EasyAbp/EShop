using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Plugins.FlashSales;
using EasyAbp.EShop.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Orders.Plugins.FlashSales;

[DependsOn(
    typeof(EShopOrdersApplicationModule),
    typeof(EShopProductsApplicationContractsModule),
    typeof(EShopPluginsFlashSalesApplicationContractsModule)
)]
public class EShopOrdersPluginsFlashSalesApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<EShopOrdersPluginsFlashSalesApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<EShopOrdersPluginsFlashSalesApplicationModule>(validate: true);
        });
    }
}
