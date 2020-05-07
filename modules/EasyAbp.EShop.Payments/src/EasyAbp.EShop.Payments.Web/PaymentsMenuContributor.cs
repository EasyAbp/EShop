using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using EasyAbp.EShop.Payments.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using EasyAbp.EShop.Payments.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using EasyAbp.EShop.Payments.Localization;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.EShop.Payments.Web
{
    public class PaymentsMenuContributor : IMenuContributor
    {
        public virtual async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenu(context);
            }
        }

        private async Task ConfigureMainMenu(MenuConfigurationContext context)
        {
            var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<PaymentsResource>>();            //Add main menu items.

            var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();

            var paymentManagementMenuItem = new ApplicationMenuItem("PaymentManagement", l["Menu:PaymentManagement"]);

            if (await authorizationService.IsGrantedAsync(PaymentsPermissions.Payments.Manage))
            {
                paymentManagementMenuItem.AddItem(
                    new ApplicationMenuItem("Payment", l["Menu:Payment"], "/EShop/Payments/Payments/Payment")
                );
            }
            
            if (await authorizationService.IsGrantedAsync(PaymentsPermissions.Refunds.Manage))
            {
                paymentManagementMenuItem.AddItem(
                    new ApplicationMenuItem("Refund", l["Menu:Refund"], "/EShop/Payments/Refunds/Refund")
                );
            }
            
            if (!paymentManagementMenuItem.Items.IsNullOrEmpty())
            {
                var eShopMenuItem = context.Menu.Items.GetOrAdd(i => i.Name == "EasyAbpEShop",
                    () => new ApplicationMenuItem("EasyAbpEShop", l["Menu:EasyAbpEShop"]));
                
                eShopMenuItem.Items.Add(paymentManagementMenuItem);
            }
        }
    }
}
