using System.Linq;

namespace EasyAbp.EShop.Products.Products;

public class RealTimePriceInfoModel
{
    public decimal PriceWithoutDiscount { get; }

    public DiscountForProductModels Discounts { get; }

    public decimal TotalDiscountAmount =>
        Discounts.ProductDiscounts.Where(x => x.InEffect).Sum(x => x.DiscountedAmount);

    public decimal TotalDiscountedPrice => PriceWithoutDiscount - TotalDiscountAmount;

    public RealTimePriceInfoModel(decimal priceWithoutDiscount, DiscountForProductModels discounts)
    {
        PriceWithoutDiscount = priceWithoutDiscount;
        Discounts = discounts;
    }
}