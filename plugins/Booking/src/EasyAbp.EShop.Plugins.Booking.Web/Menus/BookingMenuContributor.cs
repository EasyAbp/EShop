using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.Localization;
using EasyAbp.EShop.Plugins.Booking.Permissions;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.EShop.Plugins.Booking.Web.Menus;

public class BookingMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<BookingResource>();
        
        //Add main menu items.
        
        var bookingMenuItem = new ApplicationMenuItem(BookingMenus.Prefix, l["Menu:Booking"]);

        
        if (await context.IsGrantedAsync(BookingPermissions.GrantedStore.Default))
        {
            bookingMenuItem.AddItem(
                new ApplicationMenuItem(BookingMenus.GrantedStore, l["Menu:GrantedStore"],
                    "/EShop/Plugins/Booking/GrantedStores/GrantedStore")
            );
        }

        if (await context.IsGrantedAsync(BookingPermissions.ProductAsset.Default))
        {
            bookingMenuItem.AddItem(
                new ApplicationMenuItem(BookingMenus.ProductAsset, l["Menu:ProductAsset"],
                    "~/EShop/Plugins/Booking/ProductAssets/ProductAsset")
            );
        }

        if (await context.IsGrantedAsync(BookingPermissions.ProductAssetCategory.Default))
        {
            bookingMenuItem.AddItem(
                new ApplicationMenuItem(BookingMenus.ProductAssetCategory, l["Menu:ProductAssetCategory"],
                    "~/EShop/Plugins/Booking/ProductAssetCategories/ProductAssetCategory")
            );
        }

        if (!bookingMenuItem.Items.IsNullOrEmpty())
        {
            var eShopMenuItem = context.Menu.GetAdministration().Items.GetOrAdd(i => i.Name == BookingMenus.ModuleGroupPrefix,
                () => new ApplicationMenuItem(BookingMenus.ModuleGroupPrefix, l["Menu:EasyAbpEShop"], icon: "fa fa-shopping-bag"));
                
            eShopMenuItem.Items.Add(bookingMenuItem);
        }
    }
}
