using EasyAbp.EShop.Stores.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Stores
{
    [Area(EShopStoresRemoteServiceConsts.ModuleName)]
    public abstract class StoresController : AbpControllerBase
    {
        protected StoresController()
        {
            LocalizationResource = typeof(StoresResource);
        }
    }
}
