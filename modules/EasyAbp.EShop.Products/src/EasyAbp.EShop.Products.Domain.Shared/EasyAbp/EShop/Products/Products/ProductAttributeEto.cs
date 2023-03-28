using System;
using System.Collections.Generic;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductAttributeEto : ExtensibleObject, IProductAttribute
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        IEnumerable<IProductAttributeOption> IProductAttribute.ProductAttributeOptions => ProductAttributeOptions;
        public List<ProductAttributeOptionEto> ProductAttributeOptions { get; set; }
    }
}