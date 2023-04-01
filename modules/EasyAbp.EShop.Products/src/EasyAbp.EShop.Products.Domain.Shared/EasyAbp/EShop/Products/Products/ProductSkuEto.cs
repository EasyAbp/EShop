using System;
using System.Collections.Generic;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductSkuEto : ExtensibleObject, IProductSku
    {
        public Guid Id { get; set; }

        public List<Guid> AttributeOptionIds { get; set; }

        public string Name { get; set; }

        public string Currency { get; set; }

        public decimal? OriginalPrice { get; set; }

        public decimal Price { get; set; }

        public int OrderMinQuantity { get; set; }

        public int OrderMaxQuantity { get; set; }

        public TimeSpan? PaymentExpireIn { get; }

        public string MediaResources { get; set; }

        public Guid? ProductDetailId { get; set; }
    }
}