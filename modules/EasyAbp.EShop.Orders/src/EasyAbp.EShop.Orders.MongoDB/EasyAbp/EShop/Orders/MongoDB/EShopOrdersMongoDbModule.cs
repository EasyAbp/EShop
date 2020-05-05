using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Orders.MongoDB
{
    [DependsOn(
        typeof(EShopOrdersDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class EShopOrdersMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<OrdersMongoDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
            });
        }
    }
}
