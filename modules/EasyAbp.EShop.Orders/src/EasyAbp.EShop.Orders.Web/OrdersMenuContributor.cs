using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using EasyAbp.EShop.Orders.Localization;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.EShop.Orders.Web
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

            var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();
            
            var orderManagementMenuItem = new ApplicationMenuItem("OrderManagement", l["Menu:OrderManagement"]);

            if (await authorizationService.IsGrantedAsync(OrdersPermissions.Orders.Manage))
            {
                var storeAppService = context.ServiceProvider.GetRequiredService<IStoreAppService>();

                var defaultStore = (await storeAppService.GetDefaultAsync())?.Id;
                
                orderManagementMenuItem.AddItem(
                    new ApplicationMenuItem("Order", l["Menu:Order"], "/EShop/Orders/Orders/Order?storeId=" + defaultStore)
                );
            }
            
            if (!orderManagementMenuItem.Items.IsNullOrEmpty())
            {
                var eShopMenuItem = context.Menu.Items.GetOrAdd(i => i.Name == "EasyAbpEShop",
                    () => new ApplicationMenuItem("EasyAbpEShop", l["Menu:EasyAbpEShop"]));
                
                eShopMenuItem.Items.Add(orderManagementMenuItem);
            }
        }
    }
}
