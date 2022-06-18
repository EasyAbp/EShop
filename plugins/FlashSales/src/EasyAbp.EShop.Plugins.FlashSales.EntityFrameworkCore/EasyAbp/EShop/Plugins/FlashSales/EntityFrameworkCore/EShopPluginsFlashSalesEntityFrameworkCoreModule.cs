using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.FlashSales.EntityFrameworkCore;

[DependsOn(
    typeof(EShopPluginsFlashSalesDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class EShopPluginsFlashSalesEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<FlashSalesDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
        });
    }
}
