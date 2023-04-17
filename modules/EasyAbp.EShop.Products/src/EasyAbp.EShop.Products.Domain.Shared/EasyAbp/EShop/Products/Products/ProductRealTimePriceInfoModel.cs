using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyAbp.EShop.Products.Products;

public class ProductRealTimePriceInfoModel : IHasDiscountsForSku
{
    public Guid ProductId { get; }

    public Guid ProductSkuId { get; }

    public decimal PriceWithoutDiscount { get; }

    public List<CandidateProductDiscountInfoModel> CandidateProductDiscounts { get; } = new();

    public List<ProductDiscountInfoModel> ProductDiscounts { get; } = new();

    public List<OrderDiscountPreviewInfoModel> OrderDiscountPreviews { get; } = new();

    public decimal TotalDiscountAmount => ProductDiscounts.Where(x => x.InEffect).Sum(x => x.DiscountedAmount);

    public decimal TotalDiscountedPrice => PriceWithoutDiscount - TotalDiscountAmount;

    public ProductRealTimePriceInfoModel(Guid productId, Guid productSkuId, decimal priceWithoutDiscount)
    {
        ProductId = productId;
        ProductSkuId = productSkuId;
        PriceWithoutDiscount = priceWithoutDiscount;
    }
}