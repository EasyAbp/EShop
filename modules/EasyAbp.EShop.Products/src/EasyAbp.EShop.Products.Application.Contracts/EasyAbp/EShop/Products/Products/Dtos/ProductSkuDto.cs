using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    [Serializable]
    public class ProductSkuDto : ExtensibleFullAuditedEntityDto<Guid>
    {
        public List<Guid> AttributeOptionIds { get; set; }
        
        public string Name { get; set; }
        
        public string Currency { get; set; }
        
        public decimal? OriginalPrice { get; set; }
        
        public decimal Price { get; set; }
        
        public decimal DiscountedPrice { get; set; }
        
        public int Inventory { get; set; }
        
        public long Sold { get; set; }
        
        public int OrderMinQuantity { get; set; }
        
        public int OrderMaxQuantity { get; set; }
        
        public TimeSpan? PaymentExpireIn { get; set; }

        public string MediaResources { get; set; }
        
        public Guid? ProductDetailId { get; set; }
    }
}