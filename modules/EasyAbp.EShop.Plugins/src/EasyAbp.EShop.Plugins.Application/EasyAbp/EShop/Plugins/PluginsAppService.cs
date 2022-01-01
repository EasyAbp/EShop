using EasyAbp.EShop.Plugins.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins
{
    public abstract class PluginsAppService : ApplicationService
    {
        protected PluginsAppService()
        {
            LocalizationResource = typeof(PluginsResource);
            ObjectMapperContext = typeof(EShopPluginsApplicationModule);
        }
    }
}
