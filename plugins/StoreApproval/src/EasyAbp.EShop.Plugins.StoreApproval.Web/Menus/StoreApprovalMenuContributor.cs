using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.StoreApproval.Localization;
using EasyAbp.EShop.Plugins.StoreApproval.Permissions;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.EShop.Plugins.StoreApproval.Web.Menus
{
    public class StoreApprovalMenuContributor : IMenuContributor
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
            var l = context.GetLocalizer<StoreApprovalResource>();
             //Add main menu items.

            if (await context.IsGrantedAsync(StoreApprovalPermissions.StoreApplication.Default))
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem("StoreApplication", l["Menu:StoreApplication"], "/StoreApproval/StoreApplications/StoreApplication")
                );
            }
        }
    }
}
