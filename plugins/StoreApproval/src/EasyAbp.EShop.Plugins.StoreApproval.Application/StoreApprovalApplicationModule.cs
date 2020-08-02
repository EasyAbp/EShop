using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace EasyAbp.EShop.Plugins.StoreApproval
{
    [DependsOn(
        typeof(StoreApprovalDomainModule),
        typeof(StoreApprovalApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class StoreApprovalApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<StoreApprovalApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<StoreApprovalApplicationModule>(validate: true);
            });
        }
    }
}
