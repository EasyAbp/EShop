using EasyAbp.EShop.Inventory.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Inventory.Permissions
{
    public class InventoryPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(InventoryPermissions.GroupName, L("Permission:Inventory"));

            var warehousePermission = myGroup.AddPermission(InventoryPermissions.Warehouse.Default, L("Permission:Warehouse"));
            warehousePermission.AddChild(InventoryPermissions.Warehouse.Create, L("Permission:Create"));
            warehousePermission.AddChild(InventoryPermissions.Warehouse.Update, L("Permission:Update"));
            warehousePermission.AddChild(InventoryPermissions.Warehouse.Delete, L("Permission:Delete"));
            warehousePermission.AddChild(InventoryPermissions.Warehouse.CrossStore, L("Permission:CrossStore"));

            var stockPermission = myGroup.AddPermission(InventoryPermissions.Stock.Default, L("Permission:Stock"));
            stockPermission.AddChild(InventoryPermissions.Stock.Create, L("Permission:Create"));
            stockPermission.AddChild(InventoryPermissions.Stock.Update, L("Permission:Update"));
            stockPermission.AddChild(InventoryPermissions.Stock.Delete, L("Permission:Delete"));
            stockPermission.AddChild(InventoryPermissions.Stock.CrossStore, L("Permission:CrossStore"));

            var supplierPermission = myGroup.AddPermission(InventoryPermissions.Supplier.Default, L("Permission:Supplier"));
            supplierPermission.AddChild(InventoryPermissions.Supplier.Create, L("Permission:Create"));
            supplierPermission.AddChild(InventoryPermissions.Supplier.Update, L("Permission:Update"));
            supplierPermission.AddChild(InventoryPermissions.Supplier.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<InventoryResource>(name);
        }
    }
}
