using EasyAbp.EShop.Stores.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Stores
{
    [DependsOn(
        typeof(EShopStoresDomainSharedModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpMapperlyModule)
    )]
    public class EShopStoresApplicationSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IAuthorizationHandler, BasicStorePermissionAuthorizationHandler>();
        }
    }
}