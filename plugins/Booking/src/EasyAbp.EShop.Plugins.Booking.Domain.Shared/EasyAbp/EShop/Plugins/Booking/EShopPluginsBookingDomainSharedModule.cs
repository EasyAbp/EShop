using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.EShop.Plugins.Booking.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Plugins.Booking;

[DependsOn(
    typeof(AbpValidationModule)
)]
public class EShopPluginsBookingDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<EShopPluginsBookingDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<BookingResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("EasyAbp/EShop/Plugins/Booking/Localization");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("EasyAbp.EShop.Plugins.Booking", typeof(BookingResource));
        });
    }
}
