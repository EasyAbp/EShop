using EasyAbp.EShop.Stores.Stores;
using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Inventory.StockHistories.Dtos
{
    [Serializable]
    public class StockHistoryDto : CreationAuditedEntityDto<Guid>, IMultiStore, IStockHistory
    {
        public int LockedQuantity { get; set; }

        public Guid ProductSkuId { get; set; }

        public int Quantity { get; set; }

        public int AdjustedQuantity { get; set; }

        public string Description { get; set; }

        public Guid WarehouseId { get; set; }

        public Guid StoreId { get; set; }

        public Guid ProductId { get; set; }
    }
}