using EasyAbp.EShop.Plugins.Baskets.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.Baskets
{
    public abstract class BasketsController : AbpController
    {
        protected BasketsController()
        {
            LocalizationResource = typeof(BasketsResource);
        }
    }
}
