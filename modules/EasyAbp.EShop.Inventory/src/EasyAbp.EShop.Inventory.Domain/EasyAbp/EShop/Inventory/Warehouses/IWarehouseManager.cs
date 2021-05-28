using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Inventory.Warehouses
{
    public interface IWarehouseManager
    {
        Task<Warehouse> GetDefaultWarehouse(Guid storeId);
    }
}
