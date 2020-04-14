using EasyAbp.EShop.Baskets.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Baskets
{
    public abstract class BasketsController : AbpController
    {
        protected BasketsController()
        {
            LocalizationResource = typeof(BasketsResource);
        }
    }
}
