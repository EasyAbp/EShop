using EasyAbp.EShop.Plugins.Promotions.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Promotions;

public abstract class EShopPluginsPromotionsAppService : ApplicationService
{
    protected EShopPluginsPromotionsAppService()
    {
        LocalizationResource = typeof(PromotionsResource);
        ObjectMapperContext = typeof(EShopPluginsPromotionsApplicationModule);
    }
}
