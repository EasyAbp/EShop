using System;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Plugins.Baskets.BasketItems;
using Volo.Abp;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Plugins.Baskets.Booking.ObjectExtending;

public static class OrderLineExtensions
{
    public static Guid? FindBookingAssetId(this IBasketItem basketItem)
    {
        return basketItem.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingAssetId);
    }

    public static Guid GetBookingAssetId(this IBasketItem basketItem)
    {
        return Check.NotNull(FindBookingAssetId(basketItem),
            BookingOrderProperties.OrderLineBookingAssetId)!.Value;
    }

    public static Guid? FindBookingAssetCategoryId(this IBasketItem basketItem)
    {
        return basketItem.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingAssetCategoryId);
    }

    public static Guid GetBookingAssetCategoryId(this IBasketItem basketItem)
    {
        return Check.NotNull(FindBookingAssetCategoryId(basketItem),
            BookingOrderProperties.OrderLineBookingAssetCategoryId)!.Value;
    }

    public static Guid? FindBookingPeriodSchemeId(this IBasketItem basketItem)
    {
        return basketItem.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingPeriodSchemeId);
    }

    public static Guid GetBookingPeriodSchemeId(this IBasketItem basketItem)
    {
        return Check.NotNull(FindBookingPeriodSchemeId(basketItem),
            BookingOrderProperties.OrderLineBookingPeriodSchemeId)!.Value;
    }

    public static Guid? FindBookingPeriodId(this IBasketItem basketItem)
    {
        return basketItem.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingPeriodId);
    }

    public static Guid GetBookingPeriodId(this IBasketItem basketItem)
    {
        return Check.NotNull(FindBookingPeriodId(basketItem),
            BookingOrderProperties.OrderLineBookingPeriodId)!.Value;
    }

    public static int? FindBookingVolume(this IBasketItem basketItem)
    {
        return basketItem.Quantity;
    }

    public static int GetBookingVolume(this IBasketItem basketItem)
    {
        return FindBookingVolume(basketItem)!.Value;
    }

    public static DateTime? FindBookingDate(this IBasketItem basketItem)
    {
        return basketItem.FindDateTimeProperty(BookingOrderProperties.OrderLineBookingDate);
    }

    public static DateTime GetBookingDate(this IBasketItem basketItem)
    {
        return Check.NotNull(FindBookingDate(basketItem),
            BookingOrderProperties.OrderLineBookingDate)!.Value;
    }

    public static TimeSpan? FindBookingStartingTime(this IBasketItem basketItem)
    {
        return basketItem.FindTimeSpanProperty(BookingOrderProperties.OrderLineBookingStartingTime);
    }

    public static TimeSpan GetBookingStartingTime(this IBasketItem basketItem)
    {
        return Check.NotNull(FindBookingStartingTime(basketItem),
            BookingOrderProperties.OrderLineBookingStartingTime)!.Value;
    }

    public static TimeSpan? FindBookingDuration(this IBasketItem basketItem)
    {
        return basketItem.FindTimeSpanProperty(BookingOrderProperties.OrderLineBookingDuration);
    }

    public static TimeSpan GetBookingDuration(this IBasketItem basketItem)
    {
        return Check.NotNull(FindBookingDuration(basketItem),
            BookingOrderProperties.OrderLineBookingDuration)!.Value;
    }
}