using System.Threading.Tasks;
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

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(BookingMenus.Prefix, displayName: "Booking", "~/Booking", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
