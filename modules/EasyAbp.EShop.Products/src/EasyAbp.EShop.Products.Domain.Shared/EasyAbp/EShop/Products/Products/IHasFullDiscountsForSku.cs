namespace EasyAbp.EShop.Products.Products;

public interface IHasFullDiscountsForSku : IHasDiscountsForSku
{
    /// <summary>
    /// The realtime price without subtracting the discount amount.
    /// </summary>
    decimal PriceWithoutDiscount { get; }
}