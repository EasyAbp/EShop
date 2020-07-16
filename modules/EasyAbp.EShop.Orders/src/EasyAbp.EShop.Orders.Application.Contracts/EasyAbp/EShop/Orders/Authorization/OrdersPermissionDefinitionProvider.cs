using EasyAbp.EShop.Orders.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Orders.Authorization
{
    public class OrdersPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var moduleGroup = context.AddGroup(OrdersPermissions.GroupName, L("Permission:Orders"));
            
            var order = moduleGroup.AddPermission(OrdersPermissions.Orders.Default, L("Permission:Order"));
            order.AddChild(OrdersPermissions.Orders.Manage, L("Permission:Manage"));
            order.AddChild(OrdersPermissions.Orders.CrossStore, L("Permission:CrossStore"));
            order.AddChild(OrdersPermissions.Orders.Create, L("Permission:Create"));
            order.AddChild(OrdersPermissions.Orders.Complete, L("Permission:Complete"));
            order.AddChild(OrdersPermissions.Orders.RequestCancellation, L("Permission:RequestCancellation"));
            order.AddChild(OrdersPermissions.Orders.Cancel, L("Permission:Cancel"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<OrdersResource>(name);
        }
    }
}