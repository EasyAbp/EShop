using System;
using Volo.Abp.Settings;

namespace EasyAbp.EShop.Products.Settings
{
    public class ProductsSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            /* Define module settings here.
             * Use names from ProductsSettings class.
             */

            context.Add(
                new SettingDefinition(
                    ProductsSettings.ProductView.CacheDurationSeconds,
                    "60"
                ),
                new SettingDefinition(
                    ProductsSettings.Product.DefaultPaymentExpireIn,
                    TimeSpan.FromMinutes(15).ToString(),
                    isVisibleToClients: true
                )
            );
        }
    }
}