using System.Collections.Generic;

namespace EasyAbp.EShop.Products.Products;

public class DiscountForProductModels : IHasDiscountsForProduct
{
    public List<ProductDiscountInfoModel> ProductDiscounts { get; set; }

    public List<OrderDiscountPreviewInfoModel> OrderDiscountPreviews { get; set; }

    public DiscountForProductModels()
    {
        ProductDiscounts = new List<ProductDiscountInfoModel>();
        OrderDiscountPreviews = new List<OrderDiscountPreviewInfoModel>();
    }

    public DiscountForProductModels(List<ProductDiscountInfoModel> productDiscounts,
        List<OrderDiscountPreviewInfoModel> orderDiscountPreviews)
    {
        ProductDiscounts = productDiscounts ?? new List<ProductDiscountInfoModel>();
        OrderDiscountPreviews = orderDiscountPreviews ?? new List<OrderDiscountPreviewInfoModel>();
    }
}