using EasyAbp.EShop.Baskets;
using Localization.Resources.AbpUi;
using EasyAbp.EShop.Localization;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Payments;
using EasyAbp.EShop.Products;
using EasyAbp.EShop.Stores;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.EShop
{
    [DependsOn(
        typeof(EShopApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(EShopBasketsHttpApiModule),
        typeof(EShopOrdersHttpApiModule),
        typeof(EShopPaymentsHttpApiModule),
        typeof(EShopProductsHttpApiModule),
        typeof(EShopStoresHttpApiModule)
    )]
    public class EShopHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EShopHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<EShopResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
