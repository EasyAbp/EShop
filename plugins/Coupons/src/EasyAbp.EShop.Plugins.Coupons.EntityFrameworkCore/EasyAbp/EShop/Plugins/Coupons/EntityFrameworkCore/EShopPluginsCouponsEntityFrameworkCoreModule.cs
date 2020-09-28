using EasyAbp.EShop.Plugins.Coupons.Coupons;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Coupons.EntityFrameworkCore
{
    [DependsOn(
        typeof(EShopPluginsCouponsDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class EShopPluginsCouponsEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<CouponsDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddRepository<CouponTemplate, CouponTemplateRepository>();
                options.AddRepository<Coupon, CouponRepository>();
            });
        }
    }
}
