using EasyAbp.EShop.Plugins.FlashSales.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.FlashSales;

public abstract class FlashSalesController : AbpControllerBase
{
    protected FlashSalesController()
    {
        LocalizationResource = typeof(FlashSalesResource);
    }
}
