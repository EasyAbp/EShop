using Volo.Abp.Timing;

namespace EasyAbp.EShop.Products.Products;

public class ProductDiscountContext
{
    public Product Product { get; }

    public ProductSku ProductSku { get; }

    public PriceDataModel PriceDataModel { get; }

    public ProductDiscountContext(Product product, ProductSku productSku, decimal priceFromPriceProvider, IClock clock)
    {
        Product = product;
        ProductSku = productSku;
        PriceDataModel = new PriceDataModel(priceFromPriceProvider, clock);
    }
}