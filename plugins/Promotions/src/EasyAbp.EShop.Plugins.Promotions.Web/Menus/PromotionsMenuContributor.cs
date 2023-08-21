using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Promotions.Localization;
using EasyAbp.EShop.Plugins.Promotions.Permissions;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.EShop.Plugins.Promotions.Web.Menus;

public class PromotionsMenuContributor : IMenuContributor
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
        var l = context.GetLocalizer<PromotionsResource>();

        var promotionMenuItem = new ApplicationMenuItem(PromotionsMenus.Prefix, l["Menu:PromotionManagement"]);

        var uiDefaultStoreProvider = context.ServiceProvider.GetRequiredService<IUiDefaultStoreProvider>();

        var defaultStore = (await uiDefaultStoreProvider.GetAsync())?.Id;

        if (await context.IsGrantedAsync(PromotionsPermissions.Promotion.Default))
        {
            promotionMenuItem.AddItem(
                new ApplicationMenuItem(PromotionsMenus.Promotion, l["Menu:Promotion"],
                    "/EShop/Plugins/Promotions/Promotions/Promotion?storeId=" + defaultStore)
            );
        }

        if (!promotionMenuItem.Items.IsNullOrEmpty())
        {
            var eShopMenuItem = context.Menu.GetAdministration().Items.GetOrAdd(i => i.Name == PromotionsMenus.ModuleGroupPrefix,
                () => new ApplicationMenuItem(PromotionsMenus.ModuleGroupPrefix, l["Menu:EasyAbpEShop"], icon: "fa fa-shopping-bag"));

            eShopMenuItem.Items.Add(promotionMenuItem);
        }
    }
}