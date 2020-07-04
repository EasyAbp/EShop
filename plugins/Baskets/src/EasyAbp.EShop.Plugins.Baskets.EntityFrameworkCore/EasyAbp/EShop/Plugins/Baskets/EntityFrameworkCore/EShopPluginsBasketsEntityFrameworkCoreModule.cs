﻿using EasyAbp.EShop.Plugins.Baskets.BasketItems;
using EasyAbp.EShop.Plugins.Baskets.ProductUpdates;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Baskets.EntityFrameworkCore
{
    [DependsOn(
        typeof(EShopPluginsBasketsDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class EShopPluginsBasketsEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<BasketsDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddRepository<BasketItem, BasketItemRepository>();
                options.AddRepository<ProductUpdate, ProductUpdateRepository>();
            });
        }
    }
}
