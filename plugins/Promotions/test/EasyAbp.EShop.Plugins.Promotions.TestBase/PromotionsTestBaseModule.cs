using EasyAbp.EShop.Plugins.Promotions.Options;
using EasyAbp.EShop.Plugins.Promotions.PromotionTypes.MinQuantityOrderDiscount;
using EasyAbp.EShop.Plugins.Promotions.PromotionTypes.SimpleProductDiscount;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace EasyAbp.EShop.Plugins.Promotions;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpTestBaseModule),
    typeof(AbpAuthorizationModule),
    typeof(EShopPluginsPromotionsDomainModule)
)]
public class PromotionsTestBaseModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAlwaysAllowAuthorization();

        context.Services.Configure<EShopPluginsPromotionsOptions>(options =>
        {
            options.PromotionTypes.AddSimpleProductDiscountPromotionType();
            options.PromotionTypes.AddMinQuantityOrderDiscountPromotionType();
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
            }
        });
    }
}