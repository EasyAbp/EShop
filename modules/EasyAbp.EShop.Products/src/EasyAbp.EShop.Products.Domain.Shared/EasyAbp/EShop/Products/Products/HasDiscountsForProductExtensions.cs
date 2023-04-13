using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Products.Products;

public static class HasDiscountsForProductExtensions
{
    public static void FillEffectState(this IHasDiscountsForProduct hasDiscountsForProduct, DateTime now)
    {
        var effectGroupsBestModel = new Dictionary<string, ProductDiscountInfoModel>();

        foreach (var model in hasDiscountsForProduct.ProductDiscounts.Where(model =>
                     (!model.FromTime.HasValue || model.FromTime <= now) &&
                     (!model.ToTime.HasValue || model.ToTime >= now)))
        {
            if (model.EffectGroup.IsNullOrEmpty())
            {
                model.InEffect = true;
            }
            else
            {
                effectGroupsBestModel.TryGetValue(model.EffectGroup!, out var existing);

                if (existing is null)
                {
                    model.InEffect = true;
                    effectGroupsBestModel[model.EffectGroup] = model;
                }
                else if (effectGroupsBestModel[model.EffectGroup].DiscountedAmount < model.DiscountedAmount)
                {
                    effectGroupsBestModel[model.EffectGroup].InEffect = false;
                    model.InEffect = true;
                    effectGroupsBestModel[model.EffectGroup] = model;
                }
            }
        }
    }

    /// <summary>
    /// It returns a sum of the amount of the product discounts currently in effect.
    /// </summary>
    public static decimal GetDiscountedAmount(this IHasDiscountsForProduct hasDiscountsForProduct)
    {
        return hasDiscountsForProduct.ProductDiscounts.Where(x => x.InEffect == true).Sum(x => x.DiscountedAmount);
    }

    /// <summary>
    /// It returns the price minus the product discounts currently in effect.
    /// </summary>
    public static decimal GetDiscountedPrice(this IHasFullDiscountsForProduct hasFullDiscountsForProduct)
    {
        return hasFullDiscountsForProduct.PriceWithoutDiscount - hasFullDiscountsForProduct.GetDiscountedAmount();
    }

    public static ProductDiscountInfoModel FindProductDiscount(this IHasDiscountsForProduct hasDiscountsForProduct,
        [NotNull] string name, [CanBeNull] string key)
    {
        return hasDiscountsForProduct.ProductDiscounts.Find(x => x.Name == name && x.Key == key);
    }

    public static void AddOrUpdateProductDiscount(this IHasDiscountsForProduct hasDiscountsForProduct,
        ProductDiscountInfoModel model)
    {
        var found = hasDiscountsForProduct.FindProductDiscount(model.Name, model.Key);

        if (found is null)
        {
            hasDiscountsForProduct.ProductDiscounts.Add(model);
        }
        else
        {
            hasDiscountsForProduct.ProductDiscounts.ReplaceOne(found, model);
        }

        hasDiscountsForProduct.CheckDiscountedAmount();
    }

    public static bool TryRemoveProductDiscount(this IHasDiscountsForProduct hasDiscountsForProduct,
        [NotNull] string name,
        [CanBeNull] string key)
    {
        var found = hasDiscountsForProduct.FindProductDiscount(name, key);

        if (found is null)
        {
            return false;
        }

        hasDiscountsForProduct.ProductDiscounts.Remove(found);

        hasDiscountsForProduct.CheckDiscountedAmount();

        return true;
    }

    public static OrderDiscountPreviewInfoModel FindOrderDiscountPreview(
        this IHasDiscountsForProduct hasDiscountsForProduct, [NotNull] string name, [CanBeNull] string key)
    {
        return hasDiscountsForProduct.OrderDiscountPreviews.Find(x => x.Name == name && x.Key == key);
    }

    public static void AddOrUpdateOrderDiscountPreview(this IHasDiscountsForProduct hasDiscountsForProduct,
        OrderDiscountPreviewInfoModel model)
    {
        var found = hasDiscountsForProduct.FindOrderDiscountPreview(model.Name, model.Key);

        if (found is null)
        {
            hasDiscountsForProduct.OrderDiscountPreviews.Add(model);
        }
        else
        {
            hasDiscountsForProduct.OrderDiscountPreviews.ReplaceOne(found, model);
        }

        hasDiscountsForProduct.CheckDiscountedAmount();
    }

    public static bool TryRemoveOrderDiscountPreview(this IHasDiscountsForProduct hasDiscountsForProduct,
        [NotNull] string name,
        [CanBeNull] string key)
    {
        var found = hasDiscountsForProduct.FindOrderDiscountPreview(name, key);

        if (found is null)
        {
            return false;
        }

        hasDiscountsForProduct.OrderDiscountPreviews.Remove(found);

        hasDiscountsForProduct.CheckDiscountedAmount();

        return true;
    }

    private static void CheckDiscountedAmount(this IHasDiscountsForProduct hasDiscountsForProduct)
    {
        if (hasDiscountsForProduct.ProductDiscounts.Any(x => x.DiscountedAmount < decimal.Zero))
        {
            throw new DiscountAmountOverflowException();
        }
    }
}