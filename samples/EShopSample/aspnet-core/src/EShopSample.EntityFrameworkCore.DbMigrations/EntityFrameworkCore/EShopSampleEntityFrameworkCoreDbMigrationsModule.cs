using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace EShopSample.EntityFrameworkCore
{
    [DependsOn(
        typeof(EShopSampleEntityFrameworkCoreModule)
        )]
    public class EShopSampleEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<EShopSampleMigrationsDbContext>();
        }
    }
}
