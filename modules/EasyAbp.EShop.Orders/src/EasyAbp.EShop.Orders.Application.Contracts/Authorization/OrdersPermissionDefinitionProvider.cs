using EasyAbp.EShop.Orders.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Orders.Authorization
{
    public class OrdersPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            //var moduleGroup = context.AddGroup(OrdersPermissions.GroupName, L("Permission:Orders"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<OrdersResource>(name);
        }
    }
}