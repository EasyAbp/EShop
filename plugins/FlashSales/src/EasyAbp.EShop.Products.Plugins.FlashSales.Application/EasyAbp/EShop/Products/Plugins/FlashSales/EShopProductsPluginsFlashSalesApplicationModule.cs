using EasyAbp.EShop.Plugins.FlashSales;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products.Plugins.FlashSales;

[DependsOn(
    typeof(EShopProductsApplicationModule),
    typeof(EShopPluginsFlashSalesApplicationContractsModule)
)]
public class EShopProductsPluginsFlashSalesApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<EShopProductsPluginsFlashSalesApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.Configurators.Add(abpAutoMapperConfigurationContext =>
            {
                var profile = abpAutoMapperConfigurationContext.ServiceProvider
                    .GetRequiredService<EShopProductsPluginsFlashSalesApplicationAutoMapperProfile>();

                abpAutoMapperConfigurationContext.MapperConfiguration.AddProfile(profile);
            });
        });
    }
}
