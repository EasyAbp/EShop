using EasyAbp.EShop.Plugins.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins
{
    public abstract class PluginsController : AbpController
    {
        protected PluginsController()
        {
            LocalizationResource = typeof(PluginsResource);
        }
    }
}
