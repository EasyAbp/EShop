using EasyAbp.EShop.Plugins.StoreApproval.StoreApplications;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.StoreApproval.EntityFrameworkCore
{
    [DependsOn(
        typeof(StoreApprovalDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class StoreApprovalEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<StoreApprovalDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */

                options.AddRepository<StoreApplication, EfCoreStoreApplicationRepository>();
            });
        }
    }
}