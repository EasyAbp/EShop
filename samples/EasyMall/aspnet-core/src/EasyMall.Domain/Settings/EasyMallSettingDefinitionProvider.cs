using Volo.Abp.Settings;

namespace EasyMall.Settings
{
    public class EasyMallSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(EasyMallSettings.MySetting1));
        }
    }
}
