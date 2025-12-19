using EasyAbp.Abp.Trees;
using EasyAbp.EShop.Products.Options;
using EasyAbp.EShop.Products.Options.ProductGroups;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products
{
    [DependsOn(
        typeof(EShopProductsDomainSharedModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpCachingModule),
        typeof(AbpTreesDomainModule)
    )]
    public class EShopProductsDomainModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.EtoMappings.Add<Product, ProductEto>(typeof(EShopProductsDomainModule));

                options.AutoEventSelectors.Add<Product>();
            });

            Configure<EShopProductsOptions>(options =>
            {
                options.Groups.Configure<DefaultProductGroup>(group =>
                {
                    group.DisplayName = ProductsConsts.DefaultProductGroupDisplayName;
                    group.Description = ProductsConsts.DefaultProductGroupDescription;
                });

                options.InventoryProviders.Configure(
                    DefaultProductInventoryProvider.DefaultProductInventoryProviderName, provider =>
                    {
                        provider.DisplayName =
                            DefaultProductInventoryProvider.DefaultProductInventoryProviderDisplayName;
                        provider.Description =
                            DefaultProductInventoryProvider.DefaultProductInventoryProviderDescription;
                        provider.ProviderType = typeof(DefaultProductInventoryProvider);
                    });
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<EShopProductsDomainModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<ProductsDomainAutoMapperProfile>(validate: true);
            });
        }
    }
}