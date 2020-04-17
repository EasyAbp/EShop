using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Payment.WeChatPay.MongoDB
{
    [DependsOn(
        typeof(EShopPaymentWeChatPayDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class EShopPaymentWeChatPayMongoDbModule : AbpModule
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
