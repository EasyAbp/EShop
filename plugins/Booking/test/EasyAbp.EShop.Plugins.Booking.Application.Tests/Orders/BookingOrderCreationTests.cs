using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Volo.Abp.Data;
using Xunit;

namespace EasyAbp.EShop.Plugins.Booking.Orders;

public class BookingOrderCreationTests : BookingApplicationTestBase
{
    [Fact]
    public async Task Should_Override_Booking_Price()
    {
        var orderAppService = ServiceProvider.GetRequiredService<IOrderAppService>();
        
        var orderLine1 = new CreateOrderLineDto
        {
            ProductId = BookingTestConsts.BookingProduct1Id,
            ProductSkuId = BookingTestConsts.BookingProduct1Sku1Id,
            Quantity = 1
        };

        orderLine1.SetProperty(BookingOrderProperties.OrderLineBookingPeriodSchemeId,
            BookingTestConsts.PeriodScheme1Id);
        orderLine1.SetProperty(BookingOrderProperties.OrderLineBookingPeriodId, BookingTestConsts.Period1Id);
        orderLine1.SetProperty(BookingOrderProperties.OrderLineBookingAssetId, BookingTestConsts.Asset1Id);
        orderLine1.SetProperty(BookingOrderProperties.OrderLineBookingDate, BookingTestConsts.BookingDate);
        orderLine1.SetProperty(BookingOrderProperties.OrderLineBookingStartingTime,
            BookingTestConsts.Period1StartingTime);
        orderLine1.SetProperty(BookingOrderProperties.OrderLineBookingDuration, BookingTestConsts.Period1Duration);
        orderLine1.SetProperty(BookingOrderProperties.OrderLineBookingVolume, BookingTestConsts.Volume);

        var orderLine2 = new CreateOrderLineDto
        {
            ProductId = BookingTestConsts.BookingProduct1Id,
            ProductSkuId = BookingTestConsts.BookingProduct1Sku1Id,
            Quantity = 1
        };

        orderLine2.SetProperty(BookingOrderProperties.OrderLineBookingPeriodSchemeId,
            BookingTestConsts.PeriodScheme1Id);
        orderLine2.SetProperty(BookingOrderProperties.OrderLineBookingPeriodId, BookingTestConsts.Period1Id);
        orderLine2.SetProperty(BookingOrderProperties.OrderLineBookingAssetCategoryId,
            BookingTestConsts.AssetCategory1Id);
        orderLine2.SetProperty(BookingOrderProperties.OrderLineBookingDate, BookingTestConsts.BookingDate);
        orderLine2.SetProperty(BookingOrderProperties.OrderLineBookingStartingTime,
            BookingTestConsts.Period1StartingTime);
        orderLine2.SetProperty(BookingOrderProperties.OrderLineBookingDuration, BookingTestConsts.Period1Duration);
        orderLine2.SetProperty(BookingOrderProperties.OrderLineBookingVolume, BookingTestConsts.Volume);

        var order = await orderAppService.CreateAsync(new CreateOrderDto
        {
            StoreId = BookingTestConsts.Store1Id,
            OrderLines = new List<CreateOrderLineDto>
            {
                orderLine1, orderLine2
            }
        });
        
        order.ActualTotalPrice.ShouldBe(15m);
    }
}