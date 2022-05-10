using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Booking;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BookingHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class BookingConsoleApiClientModule : AbpModule
{

}
