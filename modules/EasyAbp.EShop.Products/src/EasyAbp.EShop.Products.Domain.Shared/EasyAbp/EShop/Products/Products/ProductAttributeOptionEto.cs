using System;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Products.Products
{
    [Serializable]
    public class ProductAttributeOptionEto : ExtensibleObject, IProductAttributeOption
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }
        
        public string Description { get; set; }
        
        public int DisplayOrder { get; set; }
    }
}