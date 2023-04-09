using System;

namespace EasyAbp.EShop.Products.Products;

public class ProductDiscountContext
{
    public Product Product { get; }

    public ProductSku ProductSku { get; }

    public PriceDataModel PriceDataModel { get; }

    public ProductDiscountContext(Product product, ProductSku productSku, decimal priceFromPriceProvider, DateTime now)
    {
        Product = product;
        ProductSku = productSku;
        PriceDataModel = new PriceDataModel(priceFromPriceProvider, now);
    }
}