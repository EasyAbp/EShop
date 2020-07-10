using System;

namespace EasyAbp.EShop.Products.Products
{
    public class  ProductSkuEto : IProductSku
    {
        public Guid Id { get; set; }

        public string SerializedAttributeOptionIds { get; set; }

        public string Name { get; set; }

        public string Currency { get; set; }

        public decimal? OriginalPrice { get; set; }

        public decimal Price { get; set; }

        public int OrderMinQuantity { get; set; }

        public int OrderMaxQuantity { get; set; }

        public string MediaResources { get; set; }

        public Guid? ProductDetailId { get; set; }
    }
}