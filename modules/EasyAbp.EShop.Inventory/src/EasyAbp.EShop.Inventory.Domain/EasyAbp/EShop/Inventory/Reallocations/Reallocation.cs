namespace EasyAbp.EShop.Inventory.Reallocations
{
    using System;
    using Volo.Abp.Domain.Entities.Auditing;
    using Volo.Abp.MultiTenancy;

    /// <summary>
    /// 库存调拨实体
    /// </summary>
    public class Reallocation : FullAuditedEntity<Guid>, IReallocation, IMultiTenant
    {
        /// <summary>
        /// 商品SKU ID
        /// </summary>
        public Guid ProductSkuId { get; protected set; }

        /// <summary>
        /// 调出仓库
        /// </summary>
        public Guid SourceWarehouseId { get; protected set; }

        /// <summary>
        /// 调入仓库
        /// </summary>
        public Guid DestinationWarehouseId { get; protected set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Units { get; protected set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string OperatorName { get; protected set; }

        /// <summary>
        /// 调拨时间
        /// </summary>
        public DateTime ReallocationTime { get; protected set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        public Guid StoreId { get; protected set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; protected set; }

        public Guid ProductId { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Reallocation"/> class.
        /// </summary>
        protected Reallocation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Reallocation"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="productSkuId">The productSkuId<see cref="Guid"/>.</param>
        /// <param name="sourceWarehouseId">The sourceWarehouseId<see cref="Guid"/>.</param>
        /// <param name="destinationWarehouseId">The destinationWarehouseId<see cref="Guid"/>.</param>
        /// <param name="units">The units<see cref="decimal"/>.</param>
        /// <param name="operatorName">The operatorName<see cref="string"/>.</param>
        /// <param name="reallocationTime">The reallocationTime<see cref="DateTime"/>.</param>
        /// <param name="description">The description<see cref="string"/>.</param>
        /// <param name="storeId">The storeId<see cref="Guid"/>.</param>
        /// <param name="tenantId">The tenantId<see cref="Guid?"/>.</param>
        public Reallocation(
            Guid id,
            Guid productSkuId,
            Guid productId,
            Guid sourceWarehouseId,
            Guid destinationWarehouseId,
            int units,
            string operatorName,
            DateTime reallocationTime,
            string description,
            Guid storeId,
            Guid? tenantId
        ) : base(id)
        {
            ProductId = productId;
            ProductSkuId = productSkuId;
            SourceWarehouseId = sourceWarehouseId;
            DestinationWarehouseId = destinationWarehouseId;
            Units = units;
            OperatorName = operatorName;
            ReallocationTime = reallocationTime;
            Description = description;
            StoreId = storeId;
            TenantId = tenantId;
        }
    }
}
