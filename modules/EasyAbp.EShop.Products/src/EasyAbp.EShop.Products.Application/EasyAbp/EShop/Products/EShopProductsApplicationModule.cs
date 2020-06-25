using EasyAbp.Abp.Trees;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace EasyAbp.EShop.Products
{
    [DependsOn(
        typeof(EShopProductsDomainModule),
        typeof(EShopProductsApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpTreesApplicationModule)
    )]
    public class EShopProductsApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.Configurators.Add(abpAutoMapperConfigurationContext =>
                {
                    var profile = abpAutoMapperConfigurationContext.ServiceProvider
                        .GetRequiredService<ProductsApplicationAutoMapperProfile>();
                    
                    abpAutoMapperConfigurationContext.MapperConfiguration.AddProfile(profile);
                });
            });
        }
    }
}
