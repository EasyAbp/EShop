using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Stores
{
    [DependsOn(
        typeof(EShopStoresApplicationModule),
        typeof(EShopStoresDomainTestModule)
        )]
    public class EShopStoresApplicationTestModule : AbpModule
    {

    }
}
