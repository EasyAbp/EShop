using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Payments.Booking.Authorization;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.Payments.Dtos;
using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Volo.Abp.Data;
using Volo.Abp.Security.Claims;
using Xunit;

namespace EasyAbp.EShop.Plugins.Booking.Authorization;

public class BookingPaymentCreationAuthorizationHandlersTests : BookingApplicationTestBase
{
    [Fact]
    public async Task Should_Not_Has_Failed_If_Valid()
    {
        var handler = ServiceProvider.GetRequiredService<BookingPaymentCreationAuthorizationHandler>();

        var context = await CreateAuthorizationHandlerContextAsync();
        
        await handler.HandleAsync(context);

        context.HasFailed.ShouldBeFalse();
    }

    private Task<AuthorizationHandlerContext> CreateAuthorizationHandlerContextAsync()
    {
        var orderLine1 = new OrderLineDto
        {
            Id = BookingTestConsts.OrderLine1Id,
            ProductId = BookingTestConsts.BookingProduct1Id,
            ProductSkuId = BookingTestConsts.BookingProduct1Sku1Id,
            Quantity = 1,
            ExtraProperties = new ExtraPropertyDictionary()
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

        var orderLine2 = new OrderLineDto
        {
            Id = BookingTestConsts.OrderLine2Id,
            ProductId = BookingTestConsts.BookingProduct1Id,
            ProductSkuId = BookingTestConsts.BookingProduct1Sku1Id,
            Quantity = 1,
            ExtraProperties = new ExtraPropertyDictionary()
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

        var currentPrincipalAccessor = ServiceProvider.GetRequiredService<ICurrentPrincipalAccessor>();

        return Task.FromResult(new AuthorizationHandlerContext(
            new[] { new PaymentOperationAuthorizationRequirement(PaymentOperation.Creation) },
            currentPrincipalAccessor.Principal,
            new PaymentCreationResource
            {
                Input = new CreatePaymentDto
                {
                    PaymentMethod = "Free",
                    OrderIds = new List<Guid>
                    {
                        BookingTestConsts.Order1Id
                    }
                },
                Orders = new List<OrderDto>
                {
                    new()
                    {
                        Id = BookingTestConsts.Order1Id,
                        OrderLines = new List<OrderLineDto>
                        {
                            orderLine1, orderLine2
                        }
                    }
                }
            }));
    }
}