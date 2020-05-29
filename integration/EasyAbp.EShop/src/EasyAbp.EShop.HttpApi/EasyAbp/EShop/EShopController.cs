using EasyAbp.EShop.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop
{
    public abstract class EShopController : AbpController
    {
        protected EShopController()
        {
            LocalizationResource = typeof(EShopResource);
        }
    }
}
