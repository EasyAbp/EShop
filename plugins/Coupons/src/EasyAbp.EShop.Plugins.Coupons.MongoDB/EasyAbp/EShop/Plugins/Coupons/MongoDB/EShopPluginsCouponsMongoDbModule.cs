using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.Coupons.MongoDB
{
    [DependsOn(
        typeof(EShopPluginsCouponsDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class EShopPluginsCouponsMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<CouponsMongoDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
            });
        }
    }
}
