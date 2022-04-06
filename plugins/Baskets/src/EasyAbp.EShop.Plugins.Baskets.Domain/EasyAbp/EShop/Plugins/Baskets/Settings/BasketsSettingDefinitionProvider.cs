using Volo.Abp.Settings;

namespace EasyAbp.EShop.Plugins.Baskets.Settings
{
    public class BasketsSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            /* Define module settings here.
             * Use names from BasketsSettings class.
             */
            context.Add(new SettingDefinition(
                BasketsSettings.EnableServerSideBasketsName,
                true.ToString(),
                isVisibleToClients: true));
        }
    }
}