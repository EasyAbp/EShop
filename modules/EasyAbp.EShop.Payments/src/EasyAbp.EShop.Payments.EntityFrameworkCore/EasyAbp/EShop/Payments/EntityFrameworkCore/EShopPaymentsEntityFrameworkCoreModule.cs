using EasyAbp.EShop.Payments.Refunds;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.PaymentService.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments.EntityFrameworkCore
{
    [DependsOn(
        typeof(EShopPaymentsDomainModule),
        typeof(AbpEntityFrameworkCoreModule),
        typeof(PaymentServiceEntityFrameworkCoreModule)
    )]
    public class EShopPaymentsEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<PaymentsDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddRepository<Payment, PaymentRepository>();
                options.AddRepository<Refund, RefundRepository>();
            });
        }
    }
}
