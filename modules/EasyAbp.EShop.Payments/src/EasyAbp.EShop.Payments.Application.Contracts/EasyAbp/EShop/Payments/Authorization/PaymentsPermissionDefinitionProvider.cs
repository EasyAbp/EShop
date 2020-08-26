using EasyAbp.EShop.Payments.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Payments.Authorization
{
    public class PaymentsPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var moduleGroup = context.AddGroup(PaymentsPermissions.GroupName, L("Permission:Payments"));

            var paymentPermission = moduleGroup.AddPermission(PaymentsPermissions.Payments.Default, L("Permission:Payments"));
            paymentPermission.AddChild(PaymentsPermissions.Payments.Manage, L("Permission:Manage"));
            paymentPermission.AddChild(PaymentsPermissions.Payments.CrossStore, L("Permission:CrossStore"));
            paymentPermission.AddChild(PaymentsPermissions.Payments.Create, L("Permission:Create"));

            var refundPermission = moduleGroup.AddPermission(PaymentsPermissions.Refunds.Default, L("Permission:Refunds"));
            refundPermission.AddChild(PaymentsPermissions.Refunds.Manage, L("Permission:Manage"));
            refundPermission.AddChild(PaymentsPermissions.Refunds.CrossStore, L("Permission:CrossStore"));
            refundPermission.AddChild(PaymentsPermissions.Refunds.Create, L("Permission:Create"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<PaymentsResource>(name);
        }
    }
}
