using System;
using System.Collections.Generic;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products;

[Serializable]
public class ProductDiscountContext
{
    public DateTime Now { get; }

    public decimal PriceFromPriceProvider { get; }

    public IProduct Product { get; }

    public IProductSku ProductSku { get; }

    public List<CandidateProductDiscountInfoModel> CandidateProductDiscounts { get; }

    public List<OrderDiscountPreviewInfoModel> OrderDiscountPreviews { get; }

    public ProductDiscountContext(DateTime now, IProduct product, IProductSku productSku,
        decimal priceFromPriceProvider, List<CandidateProductDiscountInfoModel> candidateProductDiscounts = null,
        List<OrderDiscountPreviewInfoModel> orderDiscountPreviews = null)
    {
        Now = now;
        Product = Check.NotNull(product, nameof(product));
        ProductSku = Check.NotNull(productSku, nameof(productSku));
        PriceFromPriceProvider = priceFromPriceProvider;

        CandidateProductDiscounts = candidateProductDiscounts ?? new List<CandidateProductDiscountInfoModel>();
        OrderDiscountPreviews = orderDiscountPreviews ?? new List<OrderDiscountPreviewInfoModel>();
    }
}