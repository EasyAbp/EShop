using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products.Plugins.FlashSales;

[DependsOn(
    typeof(EShopProductsHttpApiModule),
    typeof(EShopProductsPluginsFlashSalesApplicationContractsModule)
)]
public class EShopProductsPluginsFlashSalesHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(EShopProductsPluginsFlashSalesHttpApiModule).Assembly);
        });
    }
}
