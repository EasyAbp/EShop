using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    public class UpdateProductSkuDto
    {
        [DisplayName("ProductSkuCode")]
        public string Code { get; set; }
        
        [Required]
        [DisplayName("ProductSkuCurrency")]
        public string Currency { get; set; }
        
        [DisplayName("ProductSkuOriginalPrice")]
        public decimal? OriginalPrice { get; set; }

        [DisplayName("ProductSkuPrice")]
        public decimal Price { get; set; }
        
        [DefaultValue(1)]
        [DisplayName("ProductSkuOrderMinQuantity")]
        public int OrderMinQuantity { get; set; }
        
        [DefaultValue(99)]
        [DisplayName("ProductSkuOrderMaxQuantity")]
        public int OrderMaxQuantity { get; set; }
        
        [DisplayName("ProductSkuProductDetailId")]
        public Guid? ProductDetailId { get; set; }
    }
    
    public class CreateProductSkuDto : UpdateProductSkuDto
    {
        [Required]
        [DisplayName("ProductSkuAttributeOptionIds")]
        public List<Guid> AttributeOptionIds { get; set; }
    }
}