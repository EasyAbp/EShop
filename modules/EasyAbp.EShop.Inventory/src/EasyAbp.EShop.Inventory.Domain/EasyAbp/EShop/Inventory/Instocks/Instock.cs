using EasyAbp.EShop.Inventory.Warehouses;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Inventory.Instocks
{
    public class Instock : FullAuditedEntity<Guid>, IInstock, IMultiWarehouse, IMultiTenant
    {
        public DateTime InstockTime { get; protected set; }

        public Guid ProductSkuId { get; protected set; }

        public string Description { get; protected set; }

        public decimal UnitPrice { get; protected set; }

        public int Units { get; protected set; }

        public string OperatorName { get; protected set; }

        public Guid WarehouseId { get; protected set; }

        public Guid StoreId { get; protected set; }

        public Guid? TenantId { get; protected set; }

        public Guid SupplierId { get; protected set; }

        public Guid ProductId { get; protected set; }

        protected Instock()
        {
        }

        public Instock(
            Guid id,
            DateTime instockTime,
            Guid productSkuId,
            Guid productId,
            string description,
            decimal unitPrice,
            int units,
            string operatorName,
            Guid supplierId,
            Guid warehouseId,
            Guid storeId,
            Guid? tenantId
        ) : base(id)
        {
            SupplierId = supplierId;
            InstockTime = instockTime;
            ProductSkuId = productSkuId;
            ProductId = productId;
            Description = description;
            UnitPrice = unitPrice;
            Units = units;
            OperatorName = operatorName;
            WarehouseId = warehouseId;
            StoreId = storeId;
            TenantId = tenantId;
        }
    }
}