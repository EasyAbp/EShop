using EasyAbp.EShop.Plugins.Coupons.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.Coupons
{
    public abstract class CouponsController : AbpController
    {
        protected CouponsController()
        {
            LocalizationResource = typeof(CouponsResource);
        }
    }
}
