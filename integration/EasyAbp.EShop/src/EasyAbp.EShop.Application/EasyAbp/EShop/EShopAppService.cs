using EasyAbp.EShop.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop
{
    public abstract class EShopAppService : ApplicationService
    {
        protected EShopAppService()
        {
            LocalizationResource = typeof(EShopResource);
            ObjectMapperContext = typeof(EShopApplicationModule);
        }
    }
}
