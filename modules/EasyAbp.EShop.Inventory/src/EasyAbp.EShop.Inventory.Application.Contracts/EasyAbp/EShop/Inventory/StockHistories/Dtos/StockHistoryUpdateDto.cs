using EasyAbp.EShop.Stores.Stores;
using System;
using System.ComponentModel;

namespace EasyAbp.EShop.Inventory.StockHistories.Dtos
{
    [Serializable]
    public class StockHistoryUpdateDto : IMultiStore
    {
        [DisplayName("StockHistoryLockedQuantity")]
        public int LockedQuantity { get; set; }

        [DisplayName("StockHistoryProductSkuId")]
        public Guid ProductSkuId { get; set; }

        [DisplayName("StockHistoryQuantity")]
        public int Quantity { get; set; }

        [DisplayName("StockHistoryAdjustedQuantity")]
        public int AdjustedQuantity { get; set; }

        [DisplayName("StockHistoryDescription")]
        public string Description { get; set; }

        [DisplayName("StockHistoryWarehouseId")]
        public Guid WarehouseId { get; set; }

        [DisplayName("StockHistoryStoreId")]
        public Guid StoreId { get; set; }
    }
}