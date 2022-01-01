using EasyAbp.EShop.Plugins.Baskets.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.Baskets
{
    [Area(EShopPluginsBasketsRemoteServiceConsts.ModuleName)]
    public abstract class BasketsController : AbpControllerBase
    {
        protected BasketsController()
        {
            LocalizationResource = typeof(BasketsResource);
        }
    }
}
