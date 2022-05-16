using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Booking.EntityFrameworkCore;

[DependsOn(
    typeof(EShopPluginsBookingDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class EShopPluginsBookingEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<BookingDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddRepository<ProductAsset, ProductAssetRepository>();
                options.AddRepository<ProductAssetCategory, ProductAssetCategoryRepository>();
        });
    }
}
