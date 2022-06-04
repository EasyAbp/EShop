using System;
using EasyAbp.EShop.Orders.Orders.Dtos;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Orders;

public static class CreateOrderLineDtoExtensions
{
    public static Guid? FindBookingAssetId(this CreateOrderLineDto orderLine)
    {
        return orderLine.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingAssetId);
    }
    
    public static Guid? FindBookingAssetCategoryId(this CreateOrderLineDto orderLine)
    {
        return orderLine.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingAssetCategoryId);
    }
    
    public static Guid? FindBookingPeriodSchemeId(this CreateOrderLineDto orderLine)
    {
        return orderLine.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingPeriodSchemeId);
    }
    
    public static Guid? FindBookingPeriodId(this CreateOrderLineDto orderLine)
    {
        return orderLine.GetProperty<Guid?>(BookingOrderProperties.OrderLineBookingPeriodId);
    }
    
    public static DateTime? FindBookingDate(this CreateOrderLineDto orderLine)
    {
        return orderLine.GetProperty<DateTime?>(BookingOrderProperties.OrderLineBookingDate);
    }
    
    public static TimeSpan? FindBookingStartingTime(this CreateOrderLineDto orderLine)
    {
        return orderLine.GetProperty<TimeSpan?>(BookingOrderProperties.OrderLineBookingStartingTime);
    }
    
    public static TimeSpan? FindBookingDuration(this CreateOrderLineDto orderLine)
    {
        return orderLine.GetProperty<TimeSpan?>(BookingOrderProperties.OrderLineBookingDuration);
    }
}