using EasyAbp.EShop.Plugins.Baskets.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Baskets
{
    public abstract class BasketsAppService : ApplicationService
    {
        protected BasketsAppService()
        {
            LocalizationResource = typeof(BasketsResource);
            ObjectMapperContext = typeof(EShopPluginsBasketsApplicationModule);
        }
    }
}
