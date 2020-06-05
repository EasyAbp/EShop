using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using EasyAbp.EShop.Payments.Localization;
using Microsoft.AspNetCore.Authorization;
using EasyAbp.EShop.Payments.Authorization;
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
using EasyAbp.EShop.Stores.Stores;
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
            var l = context.GetLocalizer<PaymentsResource>();            //Add main menu items.

            var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();
            
            var paymentManagementMenuItem = new ApplicationMenuItem("PaymentManagement", l["Menu:PaymentManagement"]);
             
            var storeAppService = context.ServiceProvider.GetRequiredService<IStoreAppService>();

            var defaultStore = (await storeAppService.GetDefaultAsync())?.Id;
            
            if (await authorizationService.IsGrantedAsync(PaymentsPermissions.Payments.Manage))
            {
                paymentManagementMenuItem.AddItem(
                    new ApplicationMenuItem("Payments", l["Menu:Payments"], "/EShop/Payments/Payments/Payment?storeId=" + defaultStore)
                );
            }
            if (await authorizationService.IsGrantedAsync(PaymentsPermissions.Refunds.Manage))
            {
                paymentManagementMenuItem.AddItem(
                    new ApplicationMenuItem("Refunds", l["Menu:Refunds"], "/EShop/Payments/Refunds/Refund?storeId=" + defaultStore)
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
