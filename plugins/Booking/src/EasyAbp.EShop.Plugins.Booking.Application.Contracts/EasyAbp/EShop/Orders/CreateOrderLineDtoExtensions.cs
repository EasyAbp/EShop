using System;
using EasyAbp.EShop.Orders.Orders.Dtos;
using Volo.Abp;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Orders;

public static class CreateOrderLineDtoExtensions
{
    public static Guid? FindBookingAssetId(this CreateOrderLineDto orderLine)
    {
        return orderLine.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingAssetId);
    }

    public static Guid GetBookingAssetId(this CreateOrderLineDto orderLine)
    {
        return Check.NotNull(FindBookingAssetId(orderLine),
            BookingOrderProperties.OrderLineBookingAssetId)!.Value;
    }

    public static Guid? FindBookingAssetCategoryId(this CreateOrderLineDto orderLine)
    {
        return orderLine.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingAssetCategoryId);
    }

    public static Guid GetBookingAssetCategoryId(this CreateOrderLineDto orderLine)
    {
        return Check.NotNull(FindBookingAssetCategoryId(orderLine),
            BookingOrderProperties.OrderLineBookingAssetCategoryId)!.Value;
    }

    public static Guid? FindBookingPeriodSchemeId(this CreateOrderLineDto orderLine)
    {
        return orderLine.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingPeriodSchemeId);
    }

    public static Guid GetBookingPeriodSchemeId(this CreateOrderLineDto orderLine)
    {
        return Check.NotNull(FindBookingPeriodSchemeId(orderLine),
            BookingOrderProperties.OrderLineBookingPeriodSchemeId)!.Value;
    }

    public static Guid? FindBookingPeriodId(this CreateOrderLineDto orderLine)
    {
        return orderLine.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingPeriodId);
    }

    public static Guid GetBookingPeriodId(this CreateOrderLineDto orderLine)
    {
        return Check.NotNull(FindBookingPeriodId(orderLine),
            BookingOrderProperties.OrderLineBookingPeriodId)!.Value;
    }

    public static int? FindBookingVolume(this CreateOrderLineDto orderLine)
    {
        return orderLine.Quantity;
    }

    public static int GetBookingVolume(this CreateOrderLineDto orderLine)
    {
        return Check.NotNull(FindBookingVolume(orderLine),
            BookingOrderProperties.OrderLineBookingVolume)!.Value;
    }

    public static DateTime? FindBookingDate(this CreateOrderLineDto orderLine)
    {
        return orderLine.GetProperty<DateTime?>(BookingOrderProperties.OrderLineBookingDate);
    }

    public static DateTime GetBookingDate(this CreateOrderLineDto orderLine)
    {
        return Check.NotNull(FindBookingDate(orderLine),
            BookingOrderProperties.OrderLineBookingDate)!.Value;
    }

    public static TimeSpan? FindBookingStartingTime(this CreateOrderLineDto orderLine)
    {
        return orderLine.GetProperty<TimeSpan?>(BookingOrderProperties.OrderLineBookingStartingTime);
    }

    public static TimeSpan GetBookingStartingTime(this CreateOrderLineDto orderLine)
    {
        return Check.NotNull(FindBookingStartingTime(orderLine),
            BookingOrderProperties.OrderLineBookingStartingTime)!.Value;
    }

    public static TimeSpan? FindBookingDuration(this CreateOrderLineDto orderLine)
    {
        return orderLine.GetProperty<TimeSpan?>(BookingOrderProperties.OrderLineBookingDuration);
    }

    public static TimeSpan GetBookingDuration(this CreateOrderLineDto orderLine)
    {
        return Check.NotNull(FindBookingDuration(orderLine),
            BookingOrderProperties.OrderLineBookingDuration)!.Value;
    }
}
