using EasyAbp.PaymentService.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Payments.MongoDB
{
    [DependsOn(
        typeof(EShopPaymentsDomainModule),
        typeof(AbpMongoDbModule),
        typeof(PaymentServiceMongoDbModule)
    )]
    public class EShopPaymentsMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<PaymentsMongoDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
            });
        }
    }
}
