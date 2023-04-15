using System.Collections.Generic;

namespace EasyAbp.EShop.Products.Products;

public class DiscountForProductModels : IHasDiscountsForProduct
{
    public List<ProductDiscountInfoModel> ProductDiscounts { get; set; }

    public List<OrderDiscountPreviewInfoModel> OrderDiscountPreviews { get; set; }

    public DiscountForProductModels(List<ProductDiscountInfoModel> productDiscounts = null,
        List<OrderDiscountPreviewInfoModel> orderDiscountPreviews = null)
    {
        ProductDiscounts = productDiscounts ?? new List<ProductDiscountInfoModel>();
        OrderDiscountPreviews = orderDiscountPreviews ?? new List<OrderDiscountPreviewInfoModel>();
    }
}