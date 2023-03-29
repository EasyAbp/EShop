using System;
using System.Linq;

namespace EasyAbp.EShop.Products.Products;

public static class ProductExtensions
{
    public static IProductSku GetSkuById(this IProduct product, Guid skuId)
    {
        return product.ProductSkus.Single(x => x.Id == skuId);
    }

    public static IProductSku FindSkuById(this IProduct product, Guid skuId)
    {
        return product.ProductSkus.FirstOrDefault(x => x.Id == skuId);
    }

    public static TimeSpan? GetSkuPaymentExpireIn(this IProduct product, Guid skuId)
    {
        var sku = product.GetSkuById(skuId);

        return sku.PaymentExpireIn ?? product.PaymentExpireIn;
    }
}