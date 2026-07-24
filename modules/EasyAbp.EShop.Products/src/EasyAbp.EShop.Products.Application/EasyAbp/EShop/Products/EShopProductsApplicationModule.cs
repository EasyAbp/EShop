using EasyAbp.Abp.Trees;
using EasyAbp.EShop.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace EasyAbp.EShop.Products
{
    [DependsOn(
        typeof(EShopProductsDomainModule),
        typeof(EShopProductsApplicationContractsModule),
        typeof(EShopStoresDomainSharedModule),
        typeof(EShopStoresApplicationSharedModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpMapperlyModule),
        typeof(AbpTreesApplicationModule)
    )]
    public class EShopProductsApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMapperlyObjectMapper<EShopProductsApplicationModule>();
        }
    }
}
