using EasyAbp.EShop.Payments.WeChatPay.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Payments.WeChatPay.Authorization
{
    public class WeChatPayPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            //var moduleGroup = context.AddGroup(WeChatPayPermissions.GroupName, L("Permission:WeChatPay"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<WeChatPayResource>(name);
        }
    }
}