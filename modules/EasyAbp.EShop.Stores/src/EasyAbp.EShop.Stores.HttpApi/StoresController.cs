using EasyAbp.EShop.Stores.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Stores
{
    public abstract class StoresController : AbpController
    {
        protected StoresController()
        {
            LocalizationResource = typeof(StoresResource);
        }
    }
}
