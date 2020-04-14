using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payment
{
    [DependsOn(
        typeof(EShopPaymentDomainSharedModule)
        )]
    public class EShopPaymentDomainModule : AbpModule
    {

    }
}
