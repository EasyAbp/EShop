using EasyAbp.EShop.Stores;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payment
{
    [DependsOn(
        typeof(EShopPaymentDomainSharedModule),
        typeof(EShopStoresDomainSharedModule)
        )]
    public class EShopPaymentDomainModule : AbpModule
    {

    }
}
