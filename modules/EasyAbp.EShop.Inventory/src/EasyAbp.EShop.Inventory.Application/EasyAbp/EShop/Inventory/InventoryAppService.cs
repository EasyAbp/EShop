using EasyAbp.EShop.Inventory.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Inventory
{
    public abstract class InventoryAppService : ApplicationService
    {
        protected InventoryAppService()
        {
            LocalizationResource = typeof(InventoryResource);
            ObjectMapperContext = typeof(InventoryApplicationModule);
        }
    }
}
