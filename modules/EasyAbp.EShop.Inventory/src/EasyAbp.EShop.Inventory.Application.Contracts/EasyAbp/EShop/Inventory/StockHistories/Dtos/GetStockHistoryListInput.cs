using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Inventory.StockHistories.Dtos
{
    public class GetStockHistoryListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 商品SKU ID
        /// </summary>
        public Guid? ProductSkuId { get; set; }

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
        /// 开始时间
        /// </summary>
        public DateTime? CreationStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? CreationEndTime { get; set; }
    }
}
