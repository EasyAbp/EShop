using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    [Serializable]
    public class ProductDto : FullAuditedEntityDto<Guid>
    {
        public Guid ProductTypeId { get; set; }
        
        public string ProductTypeName { get; set; }

        public Guid ProductDetailId { get; set; }

        public string Code { get; set; }
        
        public string DisplayName { get; set; }
        
        public InventoryStrategy InventoryStrategy { get; set; }
        
        public string MediaResources { get; set; }
        
        public string SpecifiedInventoryProviderName { get; set; }
        
        public int DisplayOrder { get; set; }

        public bool IsPublished { get; set; }
        
        public bool IsStatic { get; set; }
        
        public bool IsHidden { get; set; }
        
        public long Sold { get; set; }
        
        public decimal? MinimumPrice { get; set; }
        
        public decimal? MaximumPrice { get; set; }

        public ICollection<ProductAttributeDto> ProductAttributes { get; set; }
        
        public ICollection<ProductSkuDto> ProductSkus { get; set; }

        public ProductSkuDto GetSkuById(Guid skuId)
        {
            return ProductSkus.Single(x => x.Id == skuId);
        }
        
        public ProductSkuDto FindSkuById(Guid skuId)
        {
            return ProductSkus.FirstOrDefault(x => x.Id == skuId);
        }
    }
}