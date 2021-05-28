using EasyAbp.EShop.Inventory.Warehouses;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Inventory.StockHistories
{
    public class StockHistory : CreationAuditedEntity<Guid>, IStockHistory, IMultiWarehouse, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual int LockedQuantity { get; protected set; }

        public virtual Guid ProductSkuId { get; protected set; }

        public virtual int Quantity { get; protected set; }

        public virtual int AdjustedQuantity { get; protected set; }

        public virtual string Description { get; protected set; }

        public virtual Guid WarehouseId { get; protected set; }

        public virtual Guid StoreId { get; protected set; }

        public Guid ProductId { get; set; }

        protected StockHistory()
        {
        }

        public StockHistory(
            Guid id,
            Guid? tenantId,
            int lockedQuantity,
            Guid productSkuId,
            Guid productId,
            int quantity,
            int adjustedQuantity,
            string description,
            Guid warehouseId,
            Guid storeId
        ) : base(id)
        {
            TenantId = tenantId;
            LockedQuantity = lockedQuantity;
            ProductSkuId = productSkuId;
            ProductId = productId;
            Quantity = quantity;
            AdjustedQuantity = adjustedQuantity;
            Description = description;
            WarehouseId = warehouseId;
            StoreId = storeId;
        }
    }
}
