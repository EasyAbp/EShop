using EasyAbp.EShop.Payments.ObjectExtending;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.Refunds;
using EasyAbp.EShop.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain.Entities.Events.Distributed;
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
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            EShopPaymentsObjectExtensions.Configure();

            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.EtoMappings.Add<Payment, EShopPaymentEto>(typeof(EShopPaymentsDomainModule));
                options.EtoMappings.Add<Refund, EShopRefundEto>(typeof(EShopPaymentsDomainModule));

                options.AutoEventSelectors.Add<Payment>();
                options.AutoEventSelectors.Add<Refund>();
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<EShopPaymentsDomainModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<PaymentsDomainAutoMapperProfile>(validate: true);
            });
        }
    }
}