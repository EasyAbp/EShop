using EasyAbp.Abp.Trees;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products
{
    [DependsOn(
        typeof(EShopProductsDomainSharedModule),
        typeof(EShopStoresDomainSharedModule),
        typeof(AbpTreesDomainModule)
    )]
    public class EShopProductsDomainModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.EtoMappings.Add<Product, ProductEto>();
                
                options.AutoEventSelectors.Add<Product>();
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
