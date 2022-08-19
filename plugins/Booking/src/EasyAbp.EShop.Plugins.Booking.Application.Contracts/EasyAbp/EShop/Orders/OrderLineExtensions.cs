using System;
using EasyAbp.EShop.Orders.Orders;
using Volo.Abp;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Orders;

public static class OrderLineExtensions
{
    public static Guid? FindBookingAssetId(this IOrderLine orderLine)
    {
        return orderLine.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingAssetId);
    }

    public static Guid GetBookingAssetId(this IOrderLine orderLine)
    {
        return Check.NotNull(FindBookingAssetId(orderLine),
            BookingOrderProperties.OrderLineBookingAssetId)!.Value;
    }

    public static Guid? FindBookingAssetCategoryId(this IOrderLine orderLine)
    {
        return orderLine.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingAssetCategoryId);
    }

    public static Guid GetBookingAssetCategoryId(this IOrderLine orderLine)
    {
        return Check.NotNull(FindBookingAssetCategoryId(orderLine),
            BookingOrderProperties.OrderLineBookingAssetCategoryId)!.Value;
    }

    public static Guid? FindBookingPeriodSchemeId(this IOrderLine orderLine)
    {
        return orderLine.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingPeriodSchemeId);
    }

    public static Guid GetBookingPeriodSchemeId(this IOrderLine orderLine)
    {
        return Check.NotNull(FindBookingPeriodSchemeId(orderLine),
            BookingOrderProperties.OrderLineBookingPeriodSchemeId)!.Value;
    }

    public static Guid? FindBookingPeriodId(this IOrderLine orderLine)
    {
        return orderLine.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingPeriodId);
    }

    public static Guid GetBookingPeriodId(this IOrderLine orderLine)
    {
        return Check.NotNull(FindBookingPeriodId(orderLine),
            BookingOrderProperties.OrderLineBookingPeriodId)!.Value;
    }

    public static int? FindBookingVolume(this IOrderLine orderLine)
    {
        return orderLine.Quantity;
    }

    public static int GetBookingVolume(this IOrderLine orderLine)
    {
        return FindBookingVolume(orderLine)!.Value;
    }

    public static DateTime? FindBookingDate(this IOrderLine orderLine)
    {
        return orderLine.FindDateTimeProperty(BookingOrderProperties.OrderLineBookingDate);
    }

    public static DateTime GetBookingDate(this IOrderLine orderLine)
    {
        return Check.NotNull(FindBookingDate(orderLine),
            BookingOrderProperties.OrderLineBookingDate)!.Value;
    }

    public static TimeSpan? FindBookingStartingTime(this IOrderLine orderLine)
    {
        return orderLine.FindTimeSpanProperty(BookingOrderProperties.OrderLineBookingStartingTime);
    }

    public static TimeSpan GetBookingStartingTime(this IOrderLine orderLine)
    {
        return Check.NotNull(FindBookingStartingTime(orderLine),
            BookingOrderProperties.OrderLineBookingStartingTime)!.Value;
    }

    public static TimeSpan? FindBookingDuration(this IOrderLine orderLine)
    {
        return orderLine.FindTimeSpanProperty(BookingOrderProperties.OrderLineBookingDuration);
    }

    public static TimeSpan GetBookingDuration(this IOrderLine orderLine)
    {
        return Check.NotNull(FindBookingDuration(orderLine),
            BookingOrderProperties.OrderLineBookingDuration)!.Value;
    }
}