using EasyAbp.EShop.Inventory.Inventories;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Data;
using Address = EasyAbp.EShop.Inventory.Inventories.Address;

namespace EasyAbp.EShop.Inventory.Warehouses
{
    public interface IWarehouse : IHasExtraProperties, IMultiStore
    {
        string Name { get; }

        Address Address { get; }

        string Description { get; }

        string ContactName { get; }

        string ContactPhoneNumber { get; }
    }
}