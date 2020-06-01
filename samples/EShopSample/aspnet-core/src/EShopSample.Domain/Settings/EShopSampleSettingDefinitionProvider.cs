using Volo.Abp.Settings;

namespace EShopSample.Settings
{
    public class EShopSampleSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(EShopSampleSettings.MySetting1));
        }
    }
}
