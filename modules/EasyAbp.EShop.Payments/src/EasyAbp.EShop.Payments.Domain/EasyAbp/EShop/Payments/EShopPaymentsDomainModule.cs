using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.Refunds;
using EasyAbp.EShop.Stores;
using EasyAbp.PaymentService.Payments;
using EasyAbp.PaymentService.Refunds;
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
