using EasyAbp.EShop.Plugins.FlashSales;
using EasyAbp.EShop.Products;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Mapperly;
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
        context.Services.AddMapperlyObjectMapper<EShopOrdersPluginsFlashSalesApplicationModule>();
    }
}
