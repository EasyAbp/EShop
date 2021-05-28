namespace EasyAbp.EShop.Inventory.Stocks.Dtos
{
    using System;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 获取库存筛选条件
    /// </summary>
    public class GetStockListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 商品SKU ID
        /// </summary>
        public Guid? ProductSkuId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnabled { get; set; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        public Guid? WarehouseId { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        public Guid? StoreId { get; set; }

        /// <summary>
        /// 筛选
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 创建时间开始
        /// </summary>
        public DateTime? CreationStartTime { get; set; }

        /// <summary>
        /// 创建时间结束
        /// </summary>
        public DateTime? CreationEndTime { get; set; }
    }
}
