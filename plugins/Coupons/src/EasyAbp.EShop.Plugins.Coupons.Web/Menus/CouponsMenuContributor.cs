using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Coupons.Localization;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.EShop.Plugins.Coupons.Web.Menus
{
    public class CouponsMenuContributor : IMenuContributor
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
            var l = context.GetLocalizer<CouponsResource>(); //Add main menu items.

            var couponManagementMenuItem = new ApplicationMenuItem(CouponsMenus.Prefix, l["Menu:CouponManagement"]);
            
            if (!couponManagementMenuItem.Items.IsNullOrEmpty())
            {
                var eShopMenuItem = context.Menu.Items.GetOrAdd(i => i.Name == CouponsMenus.ModuleGroupPrefix,
                    () => new ApplicationMenuItem(CouponsMenus.ModuleGroupPrefix, l["Menu:EasyAbpEShop"]));
                
                eShopMenuItem.Items.Add(couponManagementMenuItem);
            }
        }
    }
}