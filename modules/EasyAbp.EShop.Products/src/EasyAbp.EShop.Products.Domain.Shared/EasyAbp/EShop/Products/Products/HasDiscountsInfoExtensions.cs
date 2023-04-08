using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Products.Products;

public static class HasDiscountsInfoExtensions
{
    public static decimal GetProductDiscountsDiscountedAmount(this IHasDiscountsInfo hasDiscountsInfo, DateTime now)
    {
        return hasDiscountsInfo.ProductDiscounts
            .Where(x => !x.FromTime.HasValue || x.FromTime <= now)
            .Where(x => !x.ToTime.HasValue || now <= x.ToTime)
            .Sum(x => x.DiscountedAmount);
    }

    public static void AddOrUpdateProductDiscount(this IHasDiscountsInfo hasDiscountsInfo,
        ProductDiscountInfoModel model)
    {
        var found = hasDiscountsInfo.FindProductDiscount(model.Name, model.Key);

        if (found is null)
        {
            hasDiscountsInfo.ProductDiscounts.Add(model);
        }
        else
        {
            hasDiscountsInfo.ProductDiscounts.ReplaceOne(found, model);
        }

        hasDiscountsInfo.CheckDiscountedAmount();
    }

    public static ProductDiscountInfoModel FindProductDiscount(this IHasDiscountsInfo hasDiscountsInfo,
        [NotNull] string name, [CanBeNull] string key)
    {
        return hasDiscountsInfo.ProductDiscounts.Find(x => x.Name == name && x.Key == key);
    }

    public static bool TryRemoveProductDiscount(this IHasDiscountsInfo hasDiscountsInfo, [NotNull] string name,
        [CanBeNull] string key)
    {
        var found = hasDiscountsInfo.FindProductDiscount(name, key);

        if (found is null)
        {
            return false;
        }

        hasDiscountsInfo.ProductDiscounts.Remove(found);

        hasDiscountsInfo.CheckDiscountedAmount();

        return true;
    }

    public static void AddOrUpdateOrderDiscountPreview(this IHasDiscountsInfo hasDiscountsInfo,
        OrderDiscountPreviewInfoModel model)
    {
        var found = hasDiscountsInfo.FindOrderDiscount(model.Name, model.Key);

        if (found is null)
        {
            hasDiscountsInfo.OrderDiscountPreviews.Add(model);
        }
        else
        {
            hasDiscountsInfo.OrderDiscountPreviews.ReplaceOne(found, model);
        }

        hasDiscountsInfo.CheckDiscountedAmount();
    }

    public static OrderDiscountPreviewInfoModel FindOrderDiscount(this IHasDiscountsInfo hasDiscountsInfo,
        [NotNull] string name, [CanBeNull] string key)
    {
        return hasDiscountsInfo.OrderDiscountPreviews.Find(x => x.Name == name && x.Key == key);
    }

    public static bool TryRemoveOrderDiscountPreview(this IHasDiscountsInfo hasDiscountsInfo, [NotNull] string name,
        [CanBeNull] string key)
    {
        var found = hasDiscountsInfo.FindOrderDiscount(name, key);

        if (found is null)
        {
            return false;
        }

        hasDiscountsInfo.OrderDiscountPreviews.Remove(found);

        hasDiscountsInfo.CheckDiscountedAmount();

        return true;
    }

    private static void CheckDiscountedAmount(this IHasDiscountsInfo hasDiscountsInfo)
    {
        if (hasDiscountsInfo.ProductDiscounts.Any(x => x.DiscountedAmount < decimal.Zero) ||
            hasDiscountsInfo.OrderDiscountPreviews.Any(x =>
                x.MinDiscountedAmount < decimal.Zero || x.MaxDiscountedAmount < decimal.Zero ||
                x.MinDiscountedAmount > x.MaxDiscountedAmount))
        {
            throw new DiscountAmountOverflowException();
        }
    }
}