using EasyAbp.EShop.Plugins.Coupons.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Coupons
{
    public abstract class CouponsAppService : ApplicationService
    {
        protected CouponsAppService()
        {
            LocalizationResource = typeof(CouponsResource);
            ObjectMapperContext = typeof(EShopPluginsCouponsApplicationModule);
        }
    }
}
