namespace EasyAbp.EShop.Products.Products;

public interface IProductView : IProductBase, IHasDiscountsInfo, IHasProductGroupDisplayName
{
    decimal? MinimumPrice { get; }

    decimal? MaximumPrice { get; }

    decimal? MinimumPriceWithoutDiscount { get; }

    decimal? MaximumPriceWithoutDiscount { get; }

    long Sold { get; }
}