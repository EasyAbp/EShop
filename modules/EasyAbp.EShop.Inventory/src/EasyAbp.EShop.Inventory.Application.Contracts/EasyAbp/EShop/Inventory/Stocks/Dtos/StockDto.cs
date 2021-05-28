using EasyAbp.EShop.Stores.Stores;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Inventory.Stocks.Dtos
{
    [Serializable]
    public class StockDto : FullAuditedEntityDto<Guid>, IMultiStore, IStock
    {
        public int LockedQuantity { get; set; }

        public Guid ProductSkuId { get; set; }

        public int Quantity { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsEnabled { get; set; }

        public string Description { get; set; }

        public Guid WarehouseId { get; set; }

        public Guid StoreId { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; set; }

        public Guid ProductId { get; set; }
    }
}