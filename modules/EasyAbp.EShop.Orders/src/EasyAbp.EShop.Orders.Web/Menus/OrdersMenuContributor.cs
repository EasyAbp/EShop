using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Authorization;
using EasyAbp.EShop.Orders.Localization;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.EShop.Orders.Web.Menus
{
    public class OrdersMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenu(context);
            }
        }

        private async Task ConfigureMainMenu(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<OrdersResource>();            //Add main menu items.

            var orderManagementMenuItem = new ApplicationMenuItem(OrdersMenus.Prefix, l["Menu:OrderManagement"]);

            if (await context.IsGrantedAsync(OrdersPermissions.Orders.Manage))
            {
                var uiDefaultStoreProvider = context.ServiceProvider.GetRequiredService<IUiDefaultStoreProvider>();

                var defaultStore = (await uiDefaultStoreProvider.GetAsync())?.Id;
                
                orderManagementMenuItem.AddItem(
                    new ApplicationMenuItem(OrdersMenus.Order, l["Menu:Order"], "/EShop/Orders/Orders/Order?storeId=" + defaultStore)
                );
            }
            
            if (!orderManagementMenuItem.Items.IsNullOrEmpty())
            {
                var eShopMenuItem = context.Menu.Items.GetOrAdd(i => i.Name == OrdersMenus.ModuleGroupPrefix,
                    () => new ApplicationMenuItem(OrdersMenus.ModuleGroupPrefix, l["Menu:EasyAbpEShop"], icon: "fa fa-shopping-bag"));
                
                eShopMenuItem.Items.Add(orderManagementMenuItem);
            }
        }
    }
}
