namespace EasyAbp.EShop.Products.Products;

public interface IHasFullDiscountsForProduct : IHasDiscountsForProduct
{
    /// <summary>
    /// The realtime price without subtracting the discount amount.
    /// </summary>
    decimal PriceWithoutDiscount { get; }
}