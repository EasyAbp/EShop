using EasyAbp.EShop.Baskets.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Baskets
{
    public abstract class BasketsAppService : ApplicationService
    {
        protected BasketsAppService()
        {
            LocalizationResource = typeof(BasketsResource);
            ObjectMapperContext = typeof(EShopBasketsApplicationModule);
        }
    }
}
