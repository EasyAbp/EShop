using EasyAbp.EShop.Products.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Products.Permissions
{
    public class ProductsPluginsFlashSalesPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var moduleGroup = context.AddGroup(ProductsPluginsFlashSalesPermissions.GroupName, L("Permission:Products"));

            var flashSaleInventoryPermission = moduleGroup.AddPermission(ProductsPluginsFlashSalesPermissions.FlashSaleInventory.Default, L("Permission:FlashSaleInventory"))
                .WithProviders(ClientPermissionValueProvider.ProviderName);
            flashSaleInventoryPermission.AddChild(ProductsPluginsFlashSalesPermissions.FlashSaleInventory.Increase, L("Permission:InventoryIncrease"))
                .WithProviders(ClientPermissionValueProvider.ProviderName);
            flashSaleInventoryPermission.AddChild(ProductsPluginsFlashSalesPermissions.FlashSaleInventory.Reduce, L("Permission:InventoryReduce"))
                .WithProviders(ClientPermissionValueProvider.ProviderName);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ProductsResource>(name);
        }
    }
}
