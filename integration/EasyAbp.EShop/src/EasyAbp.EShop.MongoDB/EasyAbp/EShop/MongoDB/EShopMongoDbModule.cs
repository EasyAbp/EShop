using EasyAbp.EShop.Plugins.MongoDB;
using EasyAbp.EShop.Orders.MongoDB;
using EasyAbp.EShop.Payments.MongoDB;
using EasyAbp.EShop.Products.MongoDB;
using EasyAbp.EShop.Stores.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.MongoDB
{
    [DependsOn(
        typeof(EShopDomainModule),
        typeof(AbpMongoDbModule),
        typeof(EShopOrdersMongoDbModule),
        typeof(EShopPaymentsMongoDbModule),
        typeof(EShopPluginsMongoDbModule),
        typeof(EShopProductsMongoDbModule),
        typeof(EShopStoresMongoDbModule)
    )]
    public class EShopMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<EShopMongoDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
            });
        }
    }
}
