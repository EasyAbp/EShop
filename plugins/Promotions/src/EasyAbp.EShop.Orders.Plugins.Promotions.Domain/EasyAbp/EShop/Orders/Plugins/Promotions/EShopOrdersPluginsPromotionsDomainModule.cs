using EasyAbp.EShop.Plugins.Promotions;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Orders.Plugins.Promotions;

[DependsOn(
    typeof(EShopPluginsPromotionsApplicationContractsModule),
    typeof(EShopOrdersDomainModule)
)]
public class EShopOrdersPluginsPromotionsDomainModule : AbpModule
{
}