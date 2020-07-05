using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Localization;
using EasyAbp.EShop.Stores.Permissions;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.EShop.Stores.Web.Menus
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

            var storeManagementMenuItem = new ApplicationMenuItem(StoresMenus.Prefix, l["Menu:StoreManagement"]);

            if (await context.IsGrantedAsync(StoresPermissions.Stores.Default))
            {
                storeManagementMenuItem.AddItem(
                    new ApplicationMenuItem(StoresMenus.Store, l["Menu:Store"], "/EShop/Stores/Stores/Store")
                );
            }
            
            if (!storeManagementMenuItem.Items.IsNullOrEmpty())
            {
                var eShopMenuItem = context.Menu.Items.GetOrAdd(i => i.Name == StoresMenus.ModuleGroupPrefix,
                    () => new ApplicationMenuItem(StoresMenus.ModuleGroupPrefix, l["Menu:EasyAbpEShop"]));
                
                eShopMenuItem.Items.Add(storeManagementMenuItem);
            }
        }
    }
}
