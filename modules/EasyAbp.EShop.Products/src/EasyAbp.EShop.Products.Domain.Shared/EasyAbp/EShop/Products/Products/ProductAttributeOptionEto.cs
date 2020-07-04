using System;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductAttributeOptionEto : IProductAttributeOption
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }
        
        public string Description { get; set; }
        
        public int DisplayOrder { get; set; }
    }
}