using Volo.Abp.Settings;

namespace EasyAbp.EShop.Inventory.Settings
{
    public class InventorySettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(new SettingDefinition(InventorySettings.DefaultWarehouseName, "默认仓库"));
        }
    }
}