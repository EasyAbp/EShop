﻿using EasyAbp.EShop.Products.Options;
using EasyAbp.EShop.Products.Options.ProductGroups;
using EasyAbp.EShop.Products.Products;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace EasyAbp.EShop.Products
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpTestBaseModule),
        typeof(AbpAuthorizationModule),
        typeof(EShopProductsDomainModule)
        )]
    public class ProductsTestBaseModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAlwaysAllowAuthorization();
            
            Configure<EShopProductsOptions>(options =>
            {
                options.InventoryProviders.Configure(
                    "Fake", provider =>
                    {
                        provider.DisplayName = "Fake";
                        provider.Description = "For tests";
                        provider.ProviderType = typeof(FakeProductInventoryProvider);
                    });
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            SeedTestData(context);
        }

        private static void SeedTestData(ApplicationInitializationContext context)
        {
            AsyncHelper.RunSync(async () =>
            {
                using (var scope = context.ServiceProvider.CreateScope())
                {
                    await scope.ServiceProvider
                        .GetRequiredService<IDataSeeder>()
                        .SeedAsync();

                    await scope.ServiceProvider
                        .GetRequiredService<ProductsTestDataBuilder>()
                        .BuildAsync();
                }
            });
        }
    }
}
