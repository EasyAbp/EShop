using EasyAbp.EShop.Stores;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products
{
    [DependsOn(
        typeof(EShopProductsDomainSharedModule),
        typeof(EShopStoresDomainSharedModule)
    )]
    public class EShopProductsDomainModule : AbpModule
    {

    }
}
