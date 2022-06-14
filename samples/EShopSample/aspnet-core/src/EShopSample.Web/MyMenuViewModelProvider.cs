using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Layout;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Themes.LeptonXLite.Components.Menu;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;
using Volo.Abp.UI.Navigation;

namespace EShopSample.Web;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(MenuViewModelProvider))]
public class MyMenuViewModelProvider : MenuViewModelProvider
{
    public MyMenuViewModelProvider(IMenuManager menuManager, IPageLayout pageLayout,
        IObjectMapper<AbpAspNetCoreMvcUiLeptonXLiteThemeModule> objectMapper) : base(menuManager, pageLayout,
        objectMapper)
    {
    }

    protected override bool SetActiveMenuItems(IList<MenuItemViewModel> items, string activeMenuItemName)
    {
        foreach (var item in items)
        {
            if (SetActiveMenuItems(item.Items, activeMenuItemName) || item.MenuItem.Name == activeMenuItemName)
            {
                item.IsActive = true;
                return true;
            }
        }

        return false;
    }
}