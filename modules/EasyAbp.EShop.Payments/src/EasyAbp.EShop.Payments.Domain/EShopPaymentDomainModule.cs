using EasyAbp.EShop.Stores;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments
{
    [DependsOn(
        typeof(EShopPaymentsDomainSharedModule),
        typeof(EShopStoresDomainSharedModule)
        )]
    public class EShopPaymentsDomainModule : AbpModule
    {

    }
}
