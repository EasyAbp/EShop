using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments
{
    [DependsOn(
        typeof(EShopPaymentsApplicationModule),
        typeof(EShopPaymentsDomainTestModule)
        )]
    public class EShopPaymentsApplicationTestModule : AbpModule
    {

    }
}
