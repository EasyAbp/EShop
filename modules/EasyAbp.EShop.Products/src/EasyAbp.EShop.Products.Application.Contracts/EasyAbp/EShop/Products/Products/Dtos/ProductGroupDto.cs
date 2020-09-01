using System;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    [Serializable]
    public class ProductGroupDto
    {
        public string Name { get; set; }
        
        public string DisplayName { get; set; }
        
        public string Description { get; set; }
    }
}