using EasyAbp.EShop.Payments.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Payments.Authorization
{
    public class PaymentsPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            // var moduleGroup = context.AddGroup(PaymentsPermissions.GroupName, L("Permission:Payments"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<PaymentsResource>(name);
        }
    }
}