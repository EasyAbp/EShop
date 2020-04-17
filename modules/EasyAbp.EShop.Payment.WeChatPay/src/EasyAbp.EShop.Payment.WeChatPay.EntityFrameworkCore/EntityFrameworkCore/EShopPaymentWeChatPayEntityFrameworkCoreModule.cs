using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payment.WeChatPay.EntityFrameworkCore
{
    [DependsOn(
        typeof(EShopPaymentWeChatPayDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class EShopPaymentWeChatPayEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<WeChatPayDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            });
        }
    }
}