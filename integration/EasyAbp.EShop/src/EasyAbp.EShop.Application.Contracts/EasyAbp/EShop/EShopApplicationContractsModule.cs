using EasyAbp.EShop.Plugins;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Payments;
using EasyAbp.EShop.Products;
using EasyAbp.EShop.Stores;
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Authorization;

namespace EasyAbp.EShop
{
    [DependsOn(
        typeof(EShopDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule),
        typeof(EShopOrdersApplicationContractsModule),
        typeof(EShopPaymentsApplicationContractsModule),
        typeof(EShopPluginsApplicationContractsModule),
        typeof(EShopProductsApplicationContractsModule),
        typeof(EShopStoresApplicationContractsModule)
        )]
    public class EShopApplicationContractsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopApplicationContractsModule>("EasyAbp.EShop");
            });
        }
    }
}
