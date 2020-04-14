using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace EasyMall.EntityFrameworkCore
{
    [DependsOn(
        typeof(EasyMallEntityFrameworkCoreModule)
        )]
    public class EasyMallEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<EasyMallMigrationsDbContext>();
        }
    }
}
