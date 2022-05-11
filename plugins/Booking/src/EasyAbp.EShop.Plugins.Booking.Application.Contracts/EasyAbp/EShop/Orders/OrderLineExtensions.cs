using System;
using EasyAbp.EShop.Orders.Orders;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Orders;

public static class OrderLineExtensions
{
    public static Guid? FindBookingAssetId(this IOrderLine orderLine)
    {
        return orderLine.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingAssetId);
    }
    
    public static Guid? FindBookingAssetCategoryId(this IOrderLine orderLine)
    {
        return orderLine.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingAssetCategoryId);
    }
    
    public static DateTime? FindBookingDate(this IOrderLine orderLine)
    {
        return orderLine.GetProperty<DateTime?>(BookingOrderProperties.OrderLineBookingDate);
    }
    
    public static TimeSpan? FindBookingStartingTime(this IOrderLine orderLine)
    {
        return orderLine.GetProperty<TimeSpan?>(BookingOrderProperties.OrderLineBookingStartingTime);
    }
    
    public static TimeSpan? FindBookingDuration(this IOrderLine orderLine)
    {
        return orderLine.GetProperty<TimeSpan?>(BookingOrderProperties.OrderLineBookingDuration);
    }
}