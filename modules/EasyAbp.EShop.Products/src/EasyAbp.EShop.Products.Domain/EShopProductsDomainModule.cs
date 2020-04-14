using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products
{
    [DependsOn(
        typeof(EShopProductsDomainSharedModule)
        )]
    public class EShopProductsDomainModule : AbpModule
    {

    }
}
