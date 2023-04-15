using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Authorization;
using EasyAbp.EShop.Payments.Localization;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.EShop.Payments.Web.Menus
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

            var paymentManagementMenuItem = new ApplicationMenuItem(PaymentsMenus.Prefix, l["Menu:PaymentManagement"]);
             
            var uiDefaultStoreProvider = context.ServiceProvider.GetRequiredService<IUiDefaultStoreProvider>();

            var defaultStore = (await uiDefaultStoreProvider.GetAsync())?.Id;
            
            if (await context.IsGrantedAsync(PaymentsPermissions.Payments.Manage))
            {
                paymentManagementMenuItem.AddItem(
                    new ApplicationMenuItem(PaymentsMenus.Payment, l["Menu:Payment"], "/EShop/Payments/Payments/Payment?storeId=" + defaultStore)
                );
            }
            if (await context.IsGrantedAsync(PaymentsPermissions.Refunds.Manage))
            {
                paymentManagementMenuItem.AddItem(
                    new ApplicationMenuItem(PaymentsMenus.Refund, l["Menu:Refund"], "/EShop/Payments/Refunds/Refund?storeId=" + defaultStore)
                );
            }

            if (!paymentManagementMenuItem.Items.IsNullOrEmpty())
            {
                var eShopMenuItem = context.Menu.Items.GetOrAdd(i => i.Name == PaymentsMenus.ModuleGroupPrefix,
                    () => new ApplicationMenuItem(PaymentsMenus.ModuleGroupPrefix, l["Menu:EasyAbpEShop"], icon: "fa fa-shopping-bag"));
                
                eShopMenuItem.Items.Add(paymentManagementMenuItem);
            }
        }
    }
}
