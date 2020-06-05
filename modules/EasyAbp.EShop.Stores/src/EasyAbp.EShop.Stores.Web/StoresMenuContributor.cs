using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using EasyAbp.EShop.Stores.Localization;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.EShop.Stores.Web
{
    public class StoresMenuContributor : IMenuContributor
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
            var l = context.GetLocalizer<StoresResource>();            //Add main menu items.

            var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();
            
            var storeManagementMenuItem = new ApplicationMenuItem("StoreManagement", l["Menu:StoreManagement"]);

            if (await authorizationService.IsGrantedAsync(StoresPermissions.Stores.Default))
            {
                storeManagementMenuItem.AddItem(
                    new ApplicationMenuItem("Store", l["Menu:Store"], "/EShop/Stores/Stores/Store")
                );
            }
            
            if (!storeManagementMenuItem.Items.IsNullOrEmpty())
            {
                var eShopMenuItem = context.Menu.Items.GetOrAdd(i => i.Name == "EasyAbpEShop",
                    () => new ApplicationMenuItem("EasyAbpEShop", l["Menu:EasyAbpEShop"]));
                
                eShopMenuItem.Items.Add(storeManagementMenuItem);
            }
        }
    }
}
