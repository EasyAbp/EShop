using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems
{
    public interface IProductData : IHasFullDiscountsForProduct
    {
        string MediaResources { get; }

        string ProductUniqueName { get; }

        string ProductDisplayName { get; }

        string SkuName { get; }

        string SkuDescription { get; }

        string Currency { get; }

        int Inventory { get; }
    }
}