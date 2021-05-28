using System;

namespace EasyAbp.EShop.Inventory.Warehouses
{
    public interface IMultiWarehouse
    {
        Guid WarehouseId { get; }
    }
}