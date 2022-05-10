using EasyAbp.EShop.Plugins.Booking.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Plugins.Booking.Permissions;

public class BookingPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BookingPermissions.GroupName, L("Permission:Booking"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookingResource>(name);
    }
}
