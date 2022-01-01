using EasyAbp.EShop.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop
{
    [Area(EShopRemoteServiceConsts.ModuleName)]
    public abstract class EShopController : AbpControllerBase
    {
        protected EShopController()
        {
            LocalizationResource = typeof(EShopResource);
        }
    }
}
