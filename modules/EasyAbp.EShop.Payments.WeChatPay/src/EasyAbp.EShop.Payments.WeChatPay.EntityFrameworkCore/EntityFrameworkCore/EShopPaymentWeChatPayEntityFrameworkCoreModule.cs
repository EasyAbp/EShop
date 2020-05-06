using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments.WeChatPay.EntityFrameworkCore
{
    [DependsOn(
        typeof(EShopPaymentsWeChatPayDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class EShopPaymentsWeChatPayEntityFrameworkCoreModule : AbpModule
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