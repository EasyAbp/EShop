using System.Collections.Generic;

namespace EasyAbp.EShop.Products.Products;

public class DiscountsInfoModel : IHasDiscountsInfo
{
    public List<ProductDiscountInfoModel> ProductDiscounts { get; set; }

    public List<OrderDiscountPreviewInfoModel> OrderDiscountPreviews { get; set; }

    public DiscountsInfoModel()
    {
        ProductDiscounts = new List<ProductDiscountInfoModel>();
        OrderDiscountPreviews = new List<OrderDiscountPreviewInfoModel>();
    }

    public DiscountsInfoModel(List<ProductDiscountInfoModel> productDiscounts,
        List<OrderDiscountPreviewInfoModel> orderDiscountPreviews)
    {
        ProductDiscounts = productDiscounts ?? new List<ProductDiscountInfoModel>();
        OrderDiscountPreviews = orderDiscountPreviews ?? new List<OrderDiscountPreviewInfoModel>();
    }
}