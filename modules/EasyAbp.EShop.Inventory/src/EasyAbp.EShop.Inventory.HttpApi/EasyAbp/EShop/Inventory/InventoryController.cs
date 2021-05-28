using EasyAbp.EShop.Inventory.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Inventory
{
    public abstract class InventoryController : AbpController
    {
        protected InventoryController()
        {
            LocalizationResource = typeof(InventoryResource);
        }
    }
}
