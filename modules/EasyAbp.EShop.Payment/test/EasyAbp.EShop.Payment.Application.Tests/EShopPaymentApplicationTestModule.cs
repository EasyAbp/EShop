using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payment
{
    [DependsOn(
        typeof(EShopPaymentApplicationModule),
        typeof(EShopPaymentDomainTestModule)
        )]
    public class EShopPaymentApplicationTestModule : AbpModule
    {

    }
}
