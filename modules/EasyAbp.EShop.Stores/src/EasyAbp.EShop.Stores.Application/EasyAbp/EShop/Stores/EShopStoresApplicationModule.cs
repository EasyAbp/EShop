using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Stores
{
    [DependsOn(
        typeof(EShopStoresDomainModule),
        typeof(EShopStoresApplicationSharedModule),
        typeof(EShopStoresApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpUsersAbstractionModule),
        typeof(AbpMapperlyModule)
    )]
    public class EShopStoresApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMapperlyObjectMapper<EShopStoresApplicationModule>();
        }
    }
}
