using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.FlashSales;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EShopPluginsFlashSalesHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class FlashSalesConsoleApiClientModule : AbpModule
{

}
