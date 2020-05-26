using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    public class ProductSkuDto : FullAuditedEntityDto<Guid>
    {
        public string SerializedAttributeOptionIds { get; set; }
        
        public string Code { get; set; }
        
        public string Currency { get; set; }
        
        public decimal? OriginalPrice { get; set; }
        
        public decimal Price { get; set; }
        
        public int Inventory { get; set; }
        
        public int Sold { get; set; }
        
        public int OrderMinQuantity { get; set; }
        
        public Guid? ProductDetailId { get; set; }
    }
}