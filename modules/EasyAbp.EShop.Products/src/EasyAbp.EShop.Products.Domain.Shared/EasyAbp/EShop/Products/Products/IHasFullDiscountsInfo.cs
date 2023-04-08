namespace EasyAbp.EShop.Products.Products;

public interface IHasFullDiscountsInfo : IHasDiscountsInfo
{
    /// <summary>
    /// The realtime price without subtracting the discount amount.
    /// </summary>
    decimal PriceWithoutDiscount { get; }
}