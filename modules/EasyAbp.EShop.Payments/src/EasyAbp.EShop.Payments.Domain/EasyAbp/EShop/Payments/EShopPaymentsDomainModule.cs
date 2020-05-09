using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp;
using Volo.Abp.AutoMapper;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(EShopPaymentsDomainSharedModule),
        typeof(EShopStoresDomainSharedModule)
        )]
    public class EShopPaymentsDomainModule : AbpModule
    {
        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.TryAddTransient<IPaymentServiceProvider, FreePaymentServiceProvider>();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var resolver = context.ServiceProvider.GetService<IPaymentServiceResolver>();

            resolver.TryRegisterProviderAsync(FreePaymentServiceProvider.PaymentMethod, typeof(FreePaymentServiceProvider));
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<EShopPaymentsDomainModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<PaymentsDomainAutoMapperProfile>(validate: true);
            });

            Configure<AbpDistributedEventBusOptions>(options =>
            {
                options.EtoMappings.Add<Payment, PaymentEto>(typeof(EShopPaymentsDomainModule));
            });
        }
    }
}
