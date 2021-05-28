using EasyAbp.EShop.Inventory.StockHistories;
using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Inventory.Inventories
{
    public class StockHistoryEto : IStockHistory, IMultiTenant
    {
        public int LockedQuantity { get; set; }

        public Guid ProductSkuId { get; set; }

        public int Quantity { get; set; }

        public int AdjustedQuantity { get; set; }

        public string Description { get; set; }

        public Guid WarehouseId { get; set; }

        public Guid StoreId { get; set; }

        public Guid? TenantId { get; set; }

        public Guid ProductId { get; set; }
    }
}