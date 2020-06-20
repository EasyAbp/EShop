using EasyAbp.Abp.Trees;
using EasyAbp.EShop.Stores;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products
{
    [DependsOn(
        typeof(EShopProductsDomainSharedModule),
        typeof(EShopStoresDomainSharedModule),
        typeof(AbpTreesDomainModule)
    )]
    public class EShopProductsDomainModule : AbpModule
    {

    }
}
