using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    public class CreateUpdateProductDto
    {
        [DisplayName("ProductStoreId")]
        public Guid? StoreId { get; set; }

        [Required]
        [DisplayName("ProductProductTypeId")]
        public Guid ProductTypeId { get; set; }

        [DisplayName("ProductCategory")]
        public IEnumerable<Guid> CategoryIds { get; set; }
        
        [Required]
        [DisplayName("ProductDisplayName")]
        public string DisplayName { get; set; }
        
        public CreateUpdateProductDetailDto ProductDetail { get; set; }

        [DisplayName("ProductInventoryStrategy")]
        public InventoryStrategy InventoryStrategy { get; set; }

        [DisplayName("ProductMediaResources")]
        public string MediaResources { get; set; }
        
        [DisplayName("ProductIsPublished")]
        public bool IsPublished { get; set; }
    }
}