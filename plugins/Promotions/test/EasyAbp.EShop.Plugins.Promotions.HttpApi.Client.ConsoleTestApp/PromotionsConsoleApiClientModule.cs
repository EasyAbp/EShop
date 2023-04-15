using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Promotions;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EShopPluginsPromotionsHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class PromotionsConsoleApiClientModule : AbpModule
{

}
