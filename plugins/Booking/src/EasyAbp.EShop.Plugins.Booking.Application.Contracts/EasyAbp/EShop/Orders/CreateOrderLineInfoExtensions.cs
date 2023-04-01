using System;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using Volo.Abp;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Orders;

public static class CreateOrderLineInfoExtensions
{
    public static Guid? FindBookingAssetId(this ICreateOrderLineInfo orderLine)
    {
        return orderLine.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingAssetId);
    }

    public static Guid GetBookingAssetId(this ICreateOrderLineInfo orderLine)
    {
        return Check.NotNull(FindBookingAssetId(orderLine),
            BookingOrderProperties.OrderLineBookingAssetId)!.Value;
    }

    public static Guid? FindBookingAssetCategoryId(this ICreateOrderLineInfo orderLine)
    {
        return orderLine.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingAssetCategoryId);
    }

    public static Guid GetBookingAssetCategoryId(this ICreateOrderLineInfo orderLine)
    {
        return Check.NotNull(FindBookingAssetCategoryId(orderLine),
            BookingOrderProperties.OrderLineBookingAssetCategoryId)!.Value;
    }

    public static Guid? FindBookingPeriodSchemeId(this ICreateOrderLineInfo orderLine)
    {
        return orderLine.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingPeriodSchemeId);
    }

    public static Guid GetBookingPeriodSchemeId(this ICreateOrderLineInfo orderLine)
    {
        return Check.NotNull(FindBookingPeriodSchemeId(orderLine),
            BookingOrderProperties.OrderLineBookingPeriodSchemeId)!.Value;
    }

    public static Guid? FindBookingPeriodId(this ICreateOrderLineInfo orderLine)
    {
        return orderLine.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingPeriodId);
    }

    public static Guid GetBookingPeriodId(this ICreateOrderLineInfo orderLine)
    {
        return Check.NotNull(FindBookingPeriodId(orderLine),
            BookingOrderProperties.OrderLineBookingPeriodId)!.Value;
    }

    public static int? FindBookingVolume(this ICreateOrderLineInfo orderLine)
    {
        return orderLine.Quantity;
    }

    public static int GetBookingVolume(this ICreateOrderLineInfo orderLine)
    {
        return FindBookingVolume(orderLine)!.Value;
    }

    public static DateTime? FindBookingDate(this ICreateOrderLineInfo orderLine)
    {
        return orderLine.FindDateTimeProperty(BookingOrderProperties.OrderLineBookingDate);
    }

    public static DateTime GetBookingDate(this ICreateOrderLineInfo orderLine)
    {
        return Check.NotNull(FindBookingDate(orderLine),
            BookingOrderProperties.OrderLineBookingDate)!.Value;
    }

    public static TimeSpan? FindBookingStartingTime(this ICreateOrderLineInfo orderLine)
    {
        return orderLine.FindTimeSpanProperty(BookingOrderProperties.OrderLineBookingStartingTime);
    }

    public static TimeSpan GetBookingStartingTime(this ICreateOrderLineInfo orderLine)
    {
        return Check.NotNull(FindBookingStartingTime(orderLine),
            BookingOrderProperties.OrderLineBookingStartingTime)!.Value;
    }

    public static TimeSpan? FindBookingDuration(this ICreateOrderLineInfo orderLine)
    {
        return orderLine.FindTimeSpanProperty(BookingOrderProperties.OrderLineBookingDuration);
    }

    public static TimeSpan GetBookingDuration(this ICreateOrderLineInfo orderLine)
    {
        return Check.NotNull(FindBookingDuration(orderLine),
            BookingOrderProperties.OrderLineBookingDuration)!.Value;
    }
}