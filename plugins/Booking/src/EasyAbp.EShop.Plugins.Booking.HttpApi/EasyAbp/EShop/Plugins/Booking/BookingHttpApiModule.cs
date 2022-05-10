using Localization.Resources.AbpUi;
using EasyAbp.EShop.Plugins.Booking.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.EShop.Plugins.Booking;

[DependsOn(
    typeof(BookingApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class BookingHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(BookingHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<BookingResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
