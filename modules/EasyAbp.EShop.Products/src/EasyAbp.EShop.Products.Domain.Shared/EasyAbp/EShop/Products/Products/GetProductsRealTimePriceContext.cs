using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products;

[Serializable]
public class GetProductsRealTimePriceContext
{
    public DateTime Now { get; }

    /// <summary>
    /// ProductId to IProduct mapping.
    /// </summary>
    public Dictionary<Guid, IProduct> Products { get; }

    /// <summary>
    /// ProductSkuId to ProductRealTimePriceInfoModel mapping.
    /// </summary>
    public Dictionary<Guid, ProductRealTimePriceInfoModel> Models { get; }

    public GetProductsRealTimePriceContext(DateTime now, IEnumerable<IProduct> products,
        IEnumerable<ProductRealTimePriceInfoModel> models)
    {
        Now = now;
        Products = Check.NotNull(products, nameof(products)).ToDictionary(x => x.Id);
        Models = Check.NotNull(models, nameof(models)).ToDictionary(x => x.ProductSkuId);
    }

    /// <summary>
    /// Ctor for serializers.
    /// </summary>
    public GetProductsRealTimePriceContext(DateTime now, Dictionary<Guid, IProduct> products,
        Dictionary<Guid, ProductRealTimePriceInfoModel> models)
    {
        Now = now;
        Products = products;
        Models = models;
    }

    public ProductRealTimePriceInfoModel GetRealTimePrice(IProductSku productSku)
    {
        return Models[productSku.Id];
    }
}