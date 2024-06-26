﻿using EasyAbp.EShop.Products;
using EasyAbp.EShop.Products.Plugins.FlashSales;
using EasyAbp.EShop.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
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
    typeof(AbpAutoMapperModule),
    typeof(AbpCachingModule)
    )]
public class EShopPluginsFlashSalesApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<EShopPluginsFlashSalesApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<EShopPluginsFlashSalesApplicationModule>(validate: true);
        });
    }
}
