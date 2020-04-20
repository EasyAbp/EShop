﻿using Localization.Resources.AbpUi;
using EasyAbp.EShop.Products.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.EShop.Products
{
    [DependsOn(
        typeof(EShopProductsApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class EShopProductsHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EShopProductsHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<ProductsResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
