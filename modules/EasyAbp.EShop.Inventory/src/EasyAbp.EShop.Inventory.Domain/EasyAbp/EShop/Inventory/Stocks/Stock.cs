using EasyAbp.EShop.Inventory.Inventories;
using EasyAbp.EShop.Inventory.Warehouses;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Inventory.Stocks
{
    public class Stock : FullAuditedAggregateRoot<Guid>, IStock, IMultiWarehouse, IMultiTenant
    {
        public int LockedQuantity { get; protected set; }

        public Guid ProductSkuId { get; protected set; }

        public int Quantity { get; protected set; }

        public int DisplayOrder { get; protected set; }

        public bool IsEnabled { get; protected set; }

        public string Description { get; protected set; }

        public Guid WarehouseId { get; protected set; }

        public Guid StoreId { get; protected set; }

        public Guid? TenantId { get; protected set; }

        public Guid ProductId { get; set; }
        protected Stock()
        {
        }

        public Stock(
            Guid id,
            int lockedQuantity,
            Guid productSkuId,
            Guid productId,
            int quantity,
            int displayOrder,
            bool isEnabled,
            string description,
            Guid warehouseId,
            Guid storeId,
            Guid? tenantId
        ) : base(id)
        {
            LockedQuantity = lockedQuantity;
            ProductSkuId = productSkuId;
            ProductId = productId;
            Quantity = quantity;
            DisplayOrder = displayOrder;
            IsEnabled = isEnabled;
            Description = description;
            WarehouseId = warehouseId;
            StoreId = storeId;
            TenantId = tenantId;

            //AddLocalEvent(new StockChangedEto
            //{
            //    AdjustedQuantity = 0,
            //    Description = "初始化库存",
            //    LockedQuantity = LockedQuantity,
            //    ProductSkuId = ProductSkuId,
            //    Quantity = Quantity,
            //    StoreId = StoreId,
            //    TenantId = TenantId,
            //    WarehouseId = WarehouseId
            //});
        }

        public void SetWarehouseId(Guid warehouseId)
        {
            WarehouseId = warehouseId;
        }

        public void AdjustQuantity(int adjustedQuantity, string description = null)
        {
            Quantity += adjustedQuantity;

            AddLocalEvent(new StockChangedEto { 
                AdjustedQuantity = adjustedQuantity,
                Description = description,
                LockedQuantity = LockedQuantity,
                ProductId = ProductId,
                ProductSkuId = ProductSkuId,
                Quantity = Quantity,
                StoreId = StoreId,
                TenantId = TenantId,
                WarehouseId = WarehouseId
            });
        }
    }
}
