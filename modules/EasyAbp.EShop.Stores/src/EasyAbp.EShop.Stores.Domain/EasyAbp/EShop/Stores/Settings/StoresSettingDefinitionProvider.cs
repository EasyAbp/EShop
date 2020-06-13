using Volo.Abp.Settings;

namespace EasyAbp.EShop.Stores.Settings
{
    public class StoresSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            /* Define module settings here.
             * Use names from StoresSettings class.
             */

            context.Add(new SettingDefinition(StoresSettings.DefaultStoreName, "My store"));
        }
    }
}