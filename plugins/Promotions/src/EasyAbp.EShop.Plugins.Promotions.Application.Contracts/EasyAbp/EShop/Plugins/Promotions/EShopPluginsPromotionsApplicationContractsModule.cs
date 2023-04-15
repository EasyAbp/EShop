using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Products;
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.EShop.Plugins.Promotions;

[DependsOn(
    typeof(EShopPluginsPromotionsDomainSharedModule),
    typeof(EShopProductsDomainSharedModule),
    typeof(EShopOrdersDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
)]
public class EShopPluginsPromotionsApplicationContractsModule : AbpModule
{
}