using EasyAbp.EShop.Plugins.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins
{
    [Area(EShopPluginsRemoteServiceConsts.ModuleName)]
    public abstract class PluginsController : AbpControllerBase
    {
        protected PluginsController()
        {
            LocalizationResource = typeof(PluginsResource);
        }
    }
}
