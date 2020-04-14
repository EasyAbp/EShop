using EasyAbp.EShop.Products.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Products.Authorization
{
    public class ProductsPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            //var moduleGroup = context.AddGroup(ProductsPermissions.GroupName, L("Permission:Products"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ProductsResource>(name);
        }
    }
}