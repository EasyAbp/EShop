using System;
using System.Collections.Generic;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Products.Products;

public class PriceDataModel : IHasFullDiscountsInfo
{
    public DateTime Now { get; }

    public decimal PriceWithoutDiscount { get; }

    /// <summary>
    /// It's a sum of the amount of product discounts which in effect at the current time (this.<see cref="Now"/>).
    /// </summary>
    public decimal DiscountedAmount => this.GetProductDiscountsDiscountedAmount(Now);

    /// <summary>
    /// It has been subtracted from the product discounts which in effect at the current time (this.<see cref="Now"/>).
    /// </summary>
    public decimal DiscountedPrice => PriceWithoutDiscount - DiscountedAmount;

    public List<ProductDiscountInfoModel> ProductDiscounts { get; } = new();

    public List<OrderDiscountPreviewInfoModel> OrderDiscountPreviews { get; } = new();

    public PriceDataModel(decimal priceWithoutDiscount, IClock clock)
    {
        if (PriceWithoutDiscount < decimal.Zero)
        {
            throw new OverflowException();
        }

        Now = clock.Now;
        PriceWithoutDiscount = priceWithoutDiscount;
    }
}