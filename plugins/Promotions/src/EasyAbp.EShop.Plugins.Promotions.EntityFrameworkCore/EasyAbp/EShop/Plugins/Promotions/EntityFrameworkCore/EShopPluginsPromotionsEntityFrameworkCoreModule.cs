using EasyAbp.EShop.Plugins.Promotions.Promotions;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Promotions.EntityFrameworkCore;

[DependsOn(
    typeof(EShopPluginsPromotionsDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class EShopPluginsPromotionsEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<PromotionsDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            options.AddRepository<Promotion, PromotionRepository>();
        });
    }
}
