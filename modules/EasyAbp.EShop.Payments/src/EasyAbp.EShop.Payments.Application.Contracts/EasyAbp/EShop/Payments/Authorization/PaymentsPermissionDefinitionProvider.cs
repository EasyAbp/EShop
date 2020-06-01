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
            paymentPermission.AddChild(PaymentsPermissions.Payments.Create, L("Permission:Create"));
            paymentPermission.AddChild(PaymentsPermissions.Payments.Update, L("Permission:Update"));
            paymentPermission.AddChild(PaymentsPermissions.Payments.Delete, L("Permission:Delete"));

            var refundPermission = moduleGroup.AddPermission(PaymentsPermissions.Refunds.Default, L("Permission:Refunds"));
            refundPermission.AddChild(PaymentsPermissions.Refunds.Create, L("Permission:Create"));
            refundPermission.AddChild(PaymentsPermissions.Refunds.Update, L("Permission:Update"));
            refundPermission.AddChild(PaymentsPermissions.Refunds.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<PaymentsResource>(name);
        }
    }
}
