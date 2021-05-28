using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Inventory.Instocks.Dtos
{
    public class GetInstockListInput : PagedAndSortedResultRequestDto
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
        /// 供应商ID
        /// </summary>
        public Guid? SupplierId { get; set; }

        /// <summary>
        /// 创建时间开始
        /// </summary>
        public DateTime? CreationStartTime { get; set; }

        /// <summary>
        /// 创建时间结束
        /// </summary>
        public DateTime? CreationEndTime { get; set; }

        /// <summary>
        /// 入库时间开始
        /// </summary>
        public DateTime? InstockStartTime { get; set; }

        /// <summary>
        /// 入库时间结束
        /// </summary>
        public DateTime? InstockEndTime { get; set; }
    }
}
