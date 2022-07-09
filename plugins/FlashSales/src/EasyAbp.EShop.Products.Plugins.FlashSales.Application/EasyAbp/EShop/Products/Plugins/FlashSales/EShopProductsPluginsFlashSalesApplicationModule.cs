using EasyAbp.EShop.Plugins.FlashSales;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products.Plugins.FlashSales;

[DependsOn(
    typeof(EShopProductsApplicationModule),
    typeof(EShopPluginsFlashSalesApplicationContractsModule),
    typeof(EShopProductsPluginsFlashSalesApplicationContractsModule),
    typeof(EShopProductsPluginsFlashSalesAbstractionsModule)
)]
public class EShopProductsPluginsFlashSalesApplicationModule : AbpModule
{
}
