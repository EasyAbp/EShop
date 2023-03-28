using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Promotions;

[DependsOn(
    typeof(EShopPluginsPromotionsApplicationModule),
    typeof(PromotionsDomainTestModule)
    )]
public class PromotionsApplicationTestModule : AbpModule
{

}
