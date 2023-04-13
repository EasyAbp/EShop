using System;
using System.Collections.Generic;

namespace EasyAbp.EShop.Products.Products;

public class ProductPriceModel : IHasFullDiscountsForProduct
{
    public decimal PriceWithoutDiscount { get; }

    public List<ProductDiscountInfoModel> ProductDiscounts { get; } = new();

    public List<OrderDiscountPreviewInfoModel> OrderDiscountPreviews { get; } = new();

    public ProductPriceModel(decimal priceWithoutDiscount)
    {
        if (PriceWithoutDiscount < decimal.Zero)
        {
            throw new OverflowException();
        }

        PriceWithoutDiscount = priceWithoutDiscount;
    }
}