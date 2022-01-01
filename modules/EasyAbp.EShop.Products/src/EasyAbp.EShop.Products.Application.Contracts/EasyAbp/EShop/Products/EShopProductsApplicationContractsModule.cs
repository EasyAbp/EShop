using EasyAbp.Abp.Trees;
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Authorization;
using Volo.Abp.Json;

namespace EasyAbp.EShop.Products
{
    [DependsOn(
        typeof(EShopProductsDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule),
        typeof(AbpJsonModule),
        typeof(AbpTreesApplicationContractsModule)
    )]
    public class EShopProductsApplicationContractsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }
    }
}
