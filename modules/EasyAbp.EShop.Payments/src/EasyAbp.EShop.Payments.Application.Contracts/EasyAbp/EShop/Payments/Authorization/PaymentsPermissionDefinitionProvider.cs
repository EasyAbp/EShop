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
            
            var payment = moduleGroup.AddPermission(PaymentsPermissions.Payments.Default, L("Permission:Payment"));
            payment.AddChild(PaymentsPermissions.Payments.Manage, L("Permission:Manage"));
            payment.AddChild(PaymentsPermissions.Payments.CrossStore, L("Permission:CrossStore"));
            payment.AddChild(PaymentsPermissions.Payments.Create, L("Permission:Create"));
            
            var refund = moduleGroup.AddPermission(PaymentsPermissions.Refunds.Default, L("Permission:Refund"));
            refund.AddChild(PaymentsPermissions.Refunds.Manage, L("Permission:Manage"));
            refund.AddChild(PaymentsPermissions.Refunds.CrossStore, L("Permission:CrossStore"));
            refund.AddChild(PaymentsPermissions.Refunds.Create, L("Permission:Create"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<PaymentsResource>(name);
        }
    }
}