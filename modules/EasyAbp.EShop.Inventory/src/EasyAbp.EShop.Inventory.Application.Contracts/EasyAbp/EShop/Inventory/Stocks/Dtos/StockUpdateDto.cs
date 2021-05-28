using EasyAbp.EShop.Stores.Stores;
using System;
using System.ComponentModel;

namespace EasyAbp.EShop.Inventory.Stocks.Dtos
{
    [Serializable]
    public class StockUpdateDto : IMultiStore
    {
        [DisplayName("StockLockedQuantity")]
        public int LockedQuantity { get; set; }

        [DisplayName("StockProductSkuId")]
        public Guid ProductSkuId { get; set; }

        [DisplayName("StockQuantity")]
        public int Quantity { get; set; }

        [DisplayName("StockDisplayOrder")]
        public int DisplayOrder { get; set; }

        [DisplayName("StockIsEnabled")]
        public bool IsEnabled { get; set; }

        [DisplayName("StockDescription")]
        public string Description { get; set; }

        [DisplayName("StockWarehouseId")]
        public Guid WarehouseId { get; set; }

        [DisplayName("StockStoreId")]
        public Guid StoreId { get; set; }

    }
}