using System.Collections.Generic;
using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems
{
    public class ProductDataModel : IProductData
    {
        public string MediaResources { get; set; }

        public string ProductUniqueName { get; set; }

        public string ProductDisplayName { get; set; }

        public string SkuName { get; set; }

        public string SkuDescription { get; set; }

        public string Currency { get; set; }

        public decimal PriceWithoutDiscount { get; set; }

        public List<ProductDiscountInfoModel> ProductDiscounts { get; set; } = new();

        public List<OrderDiscountPreviewInfoModel> OrderDiscountPreviews { get; set; } = new();

        public int Inventory { get; set; }
    }
}