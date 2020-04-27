using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Stores
{
    [DependsOn(
        typeof(EShopStoresDomainSharedModule)
        )]
    public class EShopStoresDomainModule : AbpModule
    {

    }
}
