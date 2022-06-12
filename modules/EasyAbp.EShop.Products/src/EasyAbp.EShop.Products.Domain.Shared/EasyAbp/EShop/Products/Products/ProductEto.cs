using System;
using System.Collections.Generic;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductEto : ExtensibleObject, IProduct, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid Id { get; set; }

        public Guid StoreId { get; set; }

        public string ProductGroupName { get; set; }

        public Guid? ProductDetailId { get; set; }

        public string UniqueName { get; set; }

        public string DisplayName { get; set; }

        public InventoryStrategy InventoryStrategy { get; set; }

        public string InventoryProviderName { get; set; }

        public string MediaResources { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsPublished { get; set; }

        public bool IsStatic { get; set; }

        public bool IsHidden { get; set; }

        public List<ProductAttributeEto> ProductAttributes { get; set; }

        public List<ProductSkuEto> ProductSkus { get; set; }
    }
}