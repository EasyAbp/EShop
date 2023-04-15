using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Products;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Promotions;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(EShopPluginsPromotionsDomainSharedModule),
    typeof(EShopProductsDomainSharedModule),
    typeof(EShopOrdersDomainSharedModule)
)]
public class EShopPluginsPromotionsDomainModule : AbpModule
{
}