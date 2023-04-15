using EasyAbp.EShop.Plugins.Promotions;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products.Plugins.Promotions;

[DependsOn(
    typeof(EShopPluginsPromotionsApplicationContractsModule),
    typeof(EShopProductsDomainModule)
)]
public class EShopProductsPluginsPromotionsDomainModule : AbpModule
{
}