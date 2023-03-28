using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Promotions;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(EShopPluginsPromotionsDomainSharedModule)
)]
public class EShopPluginsPromotionsDomainModule : AbpModule
{

}
