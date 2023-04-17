using System.Collections.Generic;
using System.Linq;

namespace EasyAbp.EShop.Products.Products;

public class ProductAndSkuDataModel
{
    public Product Product { get; }

    public ProductSku ProductSku { get; }

    public ProductAndSkuDataModel(Product product, ProductSku productSku)
    {
        Product = product;
        ProductSku = productSku;
    }

    public static IEnumerable<ProductAndSkuDataModel> CreateByProduct(Product product)
    {
        return product.ProductSkus.Select(sku => new ProductAndSkuDataModel(product, sku));
    }

    public static IEnumerable<ProductAndSkuDataModel> CreateByProducts(IEnumerable<Product> products)
    {
        return from product in products
            from productSku in product.ProductSkus
            select new ProductAndSkuDataModel(product, productSku);
    }
}