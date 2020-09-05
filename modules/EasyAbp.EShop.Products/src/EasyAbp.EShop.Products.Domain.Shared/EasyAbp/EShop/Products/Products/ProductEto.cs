using System;
using System.Collections.Generic;

namespace EasyAbp.EShop.Products.Products
{
    [Serializable]
    public class ProductEto : IProduct
    {
        public Guid Id { get; set; }
        
        public string ProductGroupName { get; set; }
        
        public Guid ProductDetailId { get; set; }

        public string UniqueName { get; set; }

        public string DisplayName { get; set; }

        public InventoryStrategy InventoryStrategy { get; set; }

        public string MediaResources { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsPublished { get; set; }

        public bool IsStatic { get; set; }

        public bool IsHidden { get; set; }
        
        public Dictionary<string, object> ExtraProperties { get; set; }
        
        public ICollection<ProductAttributeEto> ProductAttributes { get; set; }
        
        public ICollection<ProductSkuEto> ProductSkus { get; set; }
    }
}