using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.EShop.Plugins.Booking;

[DependsOn(
    typeof(EShopPluginsBookingDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class EShopPluginsBookingApplicationContractsModule : AbpModule
{

}
