using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductInventories.Dtos
{
    [Serializable]
    public class ProductInventoryDto : FullAuditedEntityDto<Guid>
    {
        public Guid ProductId { get; set; }

        public Guid ProductSkuId { get; set; }

        public int Inventory { get; set; }

        public long Sold { get; set; }
    }
}