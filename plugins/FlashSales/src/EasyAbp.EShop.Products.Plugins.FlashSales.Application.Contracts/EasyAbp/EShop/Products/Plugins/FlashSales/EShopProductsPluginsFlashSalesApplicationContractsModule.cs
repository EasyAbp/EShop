using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products.Plugins.FlashSales;

[DependsOn(
    typeof(EShopProductsApplicationContractsModule)
)]
public class EShopProductsPluginsFlashSalesApplicationContractsModule : AbpModule
{
}
