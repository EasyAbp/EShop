using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Inventory.Reallocations.Dtos
{
    public class GetReallocationListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 商品SKU ID
        /// </summary>
        public Guid? ProductSkuId { get; set; }

        /// <summary>
        /// 调出仓库ID
        /// </summary>
        public Guid? SourceWarehouseId { get; set; }

        /// <summary>
        /// 调入仓库ID
        /// </summary>
        public Guid? DestinationWarehouseId { get; set; }

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

        /// <summary>
        /// 调拨时间开始
        /// </summary>
        public DateTime? ReallocationStartTime { get; set; }

        /// <summary>
        /// 调拨时间结束
        /// </summary>
        public DateTime? ReallocationEndTime { get; set; }
    }
}
