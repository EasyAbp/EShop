using EasyAbp.EShop.Plugins.Coupons.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.Coupons
{
    [Area(EShopPluginsCouponsRemoteServiceConsts.ModuleName)]
    public abstract class CouponsController : AbpControllerBase
    {
        protected CouponsController()
        {
            LocalizationResource = typeof(CouponsResource);
        }
    }
}
