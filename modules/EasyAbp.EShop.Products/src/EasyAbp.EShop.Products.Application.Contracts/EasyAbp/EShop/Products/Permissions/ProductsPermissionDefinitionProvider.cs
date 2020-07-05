using EasyAbp.EShop.Products.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.Permissions
{
    public class ProductsPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var moduleGroup = context.AddGroup(ProductsPermissions.GroupName, L("Permission:Products"));
            
            var productTypes = moduleGroup.AddPermission(ProductsPermissions.ProductTypes.Default, L("Permission:ProductType"));
            productTypes.AddChild(ProductsPermissions.ProductTypes.Create, L("Permission:Create"), MultiTenancySides.Host);
            productTypes.AddChild(ProductsPermissions.ProductTypes.Update, L("Permission:Update"), MultiTenancySides.Host);
            productTypes.AddChild(ProductsPermissions.ProductTypes.Delete, L("Permission:Delete"), MultiTenancySides.Host);
            
            var categories = moduleGroup.AddPermission(ProductsPermissions.Categories.Default, L("Permission:Category"));
            categories.AddChild(ProductsPermissions.Categories.CrossStore, L("Permission:CrossStore"));
            categories.AddChild(ProductsPermissions.Categories.Create, L("Permission:Create"));
            categories.AddChild(ProductsPermissions.Categories.Update, L("Permission:Update"));
            categories.AddChild(ProductsPermissions.Categories.Delete, L("Permission:Delete"));
            
            var product = moduleGroup.AddPermission(ProductsPermissions.Products.Default, L("Permission:Product"));
            product.AddChild(ProductsPermissions.Products.CrossStore, L("Permission:CrossStore"));
            product.AddChild(ProductsPermissions.Products.Create, L("Permission:Create"));
            product.AddChild(ProductsPermissions.Products.Update, L("Permission:Update"));
            product.AddChild(ProductsPermissions.Products.Delete, L("Permission:Delete"));

            var productInventoryPermission = moduleGroup.AddPermission(ProductsPermissions.ProductInventory.Default, L("Permission:ProductInventory"));
            productInventoryPermission.AddChild(ProductsPermissions.ProductInventory.CrossStore, L("Permission:CrossStore"));
            productInventoryPermission.AddChild(ProductsPermissions.ProductInventory.Update, L("Permission:Update"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ProductsResource>(name);
        }
    }
}
