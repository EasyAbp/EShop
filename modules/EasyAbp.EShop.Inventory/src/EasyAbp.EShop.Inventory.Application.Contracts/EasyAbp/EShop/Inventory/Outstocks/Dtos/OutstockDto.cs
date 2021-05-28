namespace EasyAbp.EShop.Inventory.Outstocks.Dtos
{
    using EasyAbp.EShop.Stores.Stores;
    using System;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 出库数据模型
    /// </summary>
    [Serializable]
    public class OutstockDto : FullAuditedEntityDto<Guid>, IMultiStore, IOutstock
    {
        /// <summary>
        /// 出库时间
        /// </summary>
        public DateTime OutstockTime { get; set; }

        /// <summary>
        /// 商品SKU ID
        /// </summary>
        public Guid ProductSkuId { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Units { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        public Guid WarehouseId { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        public Guid StoreId { get; set; }

        public Guid ProductId { get; set; }
    }
}
