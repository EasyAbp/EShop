using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;
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
            options.AddRepository<FlashSalesPlan, EfCoreFlashSalesPlanRepository>();
            options.AddRepository<FlashSalesResult, EfCoreFlashSalesResultRepository>();
        });
    }
}
