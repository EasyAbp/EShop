using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Baskets.Localization;
using EasyAbp.EShop.Plugins.Baskets.Permissions;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.EShop.Plugins.Baskets.Web.Menus
{
    public class BasketsMenuContributor : IMenuContributor
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
            var l = context.GetLocalizer<BasketsResource>(); //Add main menu items.

            var basketManagementMenuItem = new ApplicationMenuItem(BasketsMenus.Prefix, l["Menu:BasketManagement"]);

            if (await context.IsGrantedAsync(BasketsPermissions.BasketItem.Default))
            {
                basketManagementMenuItem.AddItem(
                    new ApplicationMenuItem(BasketsMenus.BasketItem, l["Menu:BasketItem"], $"/EShop/Plugins/Baskets/BasketItems/BasketItem?basketName={BasketsConsts.DefaultBasketName}&userId=")
                );
            }
            
            if (!basketManagementMenuItem.Items.IsNullOrEmpty())
            {
                var eShopMenuItem = context.Menu.Items.GetOrAdd(i => i.Name == BasketsMenus.ModuleGroupPrefix,
                    () => new ApplicationMenuItem(BasketsMenus.BasketItem, l["Menu:EasyAbpEShop"]));
                
                eShopMenuItem.Items.Add(basketManagementMenuItem);
            }
        }
    }
}
