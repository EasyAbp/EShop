using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products
{
    [DependsOn(
        typeof(EShopProductsHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class EShopProductsConsoleApiClientModule : AbpModule
    {
        
    }
}
