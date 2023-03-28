using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.EShop.Plugins.Promotions;

[DependsOn(
    typeof(EShopPluginsPromotionsDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class EShopPluginsPromotionsApplicationContractsModule : AbpModule
{

}
