using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Products.Products;

public class ProductDiscountContext
{
    public DateTime Now { get; }

    public IProduct Product { get; }

    public IProductSku ProductSku { get; }

    public decimal PriceWithoutDiscount => PriceModel.PriceWithoutDiscount;

    public IReadOnlyList<ProductDiscountInfoModel> ProductDiscounts => PriceModel.ProductDiscounts;

    public IReadOnlyList<OrderDiscountPreviewInfoModel> OrderDiscountPreviews => PriceModel.OrderDiscountPreviews;

    private ProductPriceModel PriceModel { get; }

    public ProductDiscountContext(IProduct product, IProductSku productSku, decimal priceFromPriceProvider,
        DateTime now)
    {
        Product = product;
        ProductSku = productSku;
        PriceModel = new ProductPriceModel(priceFromPriceProvider);
        Now = now;
    }

    public ProductDiscountInfoModel FindProductDiscount([NotNull] string name, [CanBeNull] string key)
    {
        return PriceModel.FindProductDiscount(name, key);
    }

    public void AddOrUpdateProductDiscount(ProductDiscountInfoModel model)
    {
        PriceModel.AddOrUpdateProductDiscount(model);
        PriceModel.FillEffectState(Now);
    }

    public bool TryRemoveProductDiscount([NotNull] string name, [CanBeNull] string key)
    {
        var result = PriceModel.TryRemoveProductDiscount(name, key);

        PriceModel.FillEffectState(Now);

        return result;
    }

    public OrderDiscountPreviewInfoModel FindOrderDiscountPreview([NotNull] string name, [CanBeNull] string key)
    {
        return PriceModel.FindOrderDiscountPreview(name, key);
    }

    public void AddOrUpdateOrderDiscountPreview(OrderDiscountPreviewInfoModel model)
    {
        PriceModel.AddOrUpdateOrderDiscountPreview(model);
    }

    public bool TryRemoveOrderDiscountPreview([NotNull] string name, [CanBeNull] string key)
    {
        return PriceModel.TryRemoveOrderDiscountPreview(name, key);
    }

    /// <summary>
    /// It returns a sum of the amount of the product discounts currently in effect.
    /// </summary>
    public decimal GetDiscountedAmount(string excludingEffectGroup = null)
    {
        return PriceModel.ProductDiscounts
            .Where(x => x.InEffect == true)
            .WhereIf(excludingEffectGroup != null, x => x.EffectGroup != excludingEffectGroup)
            .Sum(x => x.DiscountedAmount);
    }

    /// <summary>
    /// It returns the price minus the product discounts currently in effect.
    /// </summary>
    public decimal GetDiscountedPrice(string excludingEffectGroup = null)
    {
        return PriceWithoutDiscount - GetDiscountedAmount(excludingEffectGroup);
    }

    public ProductPriceModel ToFinalProductPriceModel()
    {
        return PriceModel;
    }
}