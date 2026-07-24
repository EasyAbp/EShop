using EasyAbp.EShop.Products;
using EasyAbp.EShop.Products.Plugins.FlashSales;
using EasyAbp.EShop.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.Mapperly;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.FlashSales;

[DependsOn(
    typeof(EShopProductsApplicationContractsModule),
    typeof(EShopPluginsFlashSalesDomainModule),
    typeof(EShopPluginsFlashSalesApplicationContractsModule),
    typeof(EShopStoresApplicationSharedModule),
    typeof(EShopProductsPluginsFlashSalesAbstractionsModule),
    typeof(EShopProductsPluginsFlashSalesApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpMapperlyModule),
    typeof(AbpCachingModule)
    )]
public class EShopPluginsFlashSalesApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<EShopPluginsFlashSalesApplicationModule>();
    }
}
