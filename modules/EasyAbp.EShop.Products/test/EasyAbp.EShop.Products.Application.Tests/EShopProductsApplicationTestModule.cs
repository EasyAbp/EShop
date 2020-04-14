using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products
{
    [DependsOn(
        typeof(EShopProductsApplicationModule),
        typeof(EShopProductsDomainTestModule)
        )]
    public class EShopProductsApplicationTestModule : AbpModule
    {

    }
}
