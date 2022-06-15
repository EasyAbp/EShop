using EasyAbp.EShop.Orders.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace EasyAbp.EShop.Orders.Settings
{
    public class OrdersSettingDefinitionProvider : SettingDefinitionProvider
    {
        public static string DefaultCurrency { get; set; } = "USD";

        public override void Define(ISettingDefinitionContext context)
        {
            /* Define module settings here.
             * Use names from OrdersSettings class.
             */

            context.Add(
                new SettingDefinition(
                    OrdersSettings.CurrencyCode,
                    DefaultCurrency,
                    L($"DisplayName:{OrdersSettings.CurrencyCode}"),
                    L($"Description:{OrdersSettings.CurrencyCode}"),
                    isVisibleToClients: true
                )
            );
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<OrdersResource>(name);
        }
    }
}