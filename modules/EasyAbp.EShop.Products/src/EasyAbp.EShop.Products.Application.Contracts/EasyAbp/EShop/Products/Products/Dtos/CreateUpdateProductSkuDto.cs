using System;
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
        
        [DisplayName("ProductSkuInventory")]
        public int Inventory { get; set; }
        
        [DisplayName("ProductSkuOrderMinQuantity")]
        public int OrderMinQuantity { get; set; }
        
        [DisplayName("ProductSkuProductDetailId")]
        public Guid? ProductDetailId { get; set; }
    }
    
    public class CreateProductSkuDto : UpdateProductSkuDto
    {
        [Required]
        [DisplayName("ProductSkuSerializedAttributeOptionIds")]
        public string SerializedAttributeOptionIds { get; set; }
    }
}