using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Payments.WeChatPay.MongoDB
{
    [DependsOn(
        typeof(EShopPaymentsWeChatPayDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class EShopPaymentsWeChatPayMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<WeChatPayMongoDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
            });
        }
    }
}
