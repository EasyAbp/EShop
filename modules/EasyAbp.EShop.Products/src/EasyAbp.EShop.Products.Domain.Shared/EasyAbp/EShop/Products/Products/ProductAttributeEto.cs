using System;
using System.Collections.Generic;

namespace EasyAbp.EShop.Products.Products
{
    [Serializable]
    public class ProductAttributeEto : IProductAttribute
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }
        
        public string Description { get; set; }
        
        public int DisplayOrder { get; set; }
        
        public ICollection<ProductAttributeOptionEto> ProductAttributeOptions { get; set; }
        
        public Dictionary<string, object> ExtraProperties { get; set; }
    }
}