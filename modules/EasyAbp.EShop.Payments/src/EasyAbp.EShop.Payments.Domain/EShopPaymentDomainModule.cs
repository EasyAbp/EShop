using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments
{
    [DependsOn(
        typeof(EShopPaymentsDomainSharedModule),
        typeof(EShopStoresDomainSharedModule)
        )]
    public class EShopPaymentsDomainModule : AbpModule
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var resolver = context.ServiceProvider.GetService<IPaymentServiceResolver>();

            resolver.TryRegisterProviderAsync("Free", typeof(FreePaymentServiceProvider));
        }
    }
}
