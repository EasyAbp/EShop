using EasyAbp.EShop.Plugins.Promotions.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.Promotions;

public abstract class PromotionsController : AbpControllerBase
{
    protected PromotionsController()
    {
        LocalizationResource = typeof(PromotionsResource);
    }
}
