using EasyAbp.EShop.Plugins.EntityFrameworkCore;
using EasyAbp.EShop.Orders.EntityFrameworkCore;
using EasyAbp.EShop.Payments.EntityFrameworkCore;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using EasyAbp.EShop.Stores.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.EntityFrameworkCore
{
    [DependsOn(
        typeof(EShopDomainModule),
        typeof(AbpEntityFrameworkCoreModule),
        typeof(EShopOrdersEntityFrameworkCoreModule),
        typeof(EShopPaymentsEntityFrameworkCoreModule),
        typeof(EShopPluginsEntityFrameworkCoreModule),
        typeof(EShopProductsEntityFrameworkCoreModule),
        typeof(EShopStoresEntityFrameworkCoreModule)
    )]
    public class EShopEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<EShopDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            });
        }
    }
}