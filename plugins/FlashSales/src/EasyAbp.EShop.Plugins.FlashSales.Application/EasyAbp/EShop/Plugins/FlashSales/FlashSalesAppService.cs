using EasyAbp.EShop.Plugins.FlashSales.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.FlashSales;

public abstract class FlashSalesAppService : ApplicationService
{
    protected FlashSalesAppService()
    {
        LocalizationResource = typeof(FlashSalesResource);
        ObjectMapperContext = typeof(EShopPluginsFlashSalesApplicationModule);
    }
}
