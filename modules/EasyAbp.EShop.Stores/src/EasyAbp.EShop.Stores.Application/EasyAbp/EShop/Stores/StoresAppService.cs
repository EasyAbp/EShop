using EasyAbp.EShop.Stores.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Stores
{
    public abstract class StoresAppService : ApplicationService
    {
        protected StoresAppService()
        {
            LocalizationResource = typeof(StoresResource);
            ObjectMapperContext = typeof(EShopStoresApplicationModule);
        }
    }
}
