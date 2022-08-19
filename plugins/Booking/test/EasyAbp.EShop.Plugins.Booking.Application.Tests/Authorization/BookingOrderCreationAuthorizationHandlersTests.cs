using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Orders.Booking.Authorization;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Volo.Abp.Data;
using Volo.Abp.Security.Claims;
using Xunit;

namespace EasyAbp.EShop.Plugins.Booking.Authorization;

public class BookingOrderCreationAuthorizationHandlersTests : BookingApplicationTestBase
{
    [Fact]
    public async Task Should_Not_Has_Failed_If_Valid()
    {
        var handler = ServiceProvider.GetRequiredService<BookingOrderCreationAuthorizationHandler>();

        var context = await CreateAuthorizationHandlerContextAsync();

        await handler.HandleAsync(context);

        context.HasFailed.ShouldBeFalse();
    }

    [Fact]
    public async Task Should_Failed_If_ProductAsset_Mapping_Not_Exists()
    {
        var productAssetRepository = ServiceProvider.GetRequiredService<IProductAssetRepository>();

        var productAsset = await productAssetRepository.GetAsync(x =>
            x.AssetId == BookingTestConsts.Asset1Id && x.ProductId == BookingTestConsts.BookingProduct1Id);

        await productAssetRepository.DeleteAsync(productAsset, true);

        var handler = ServiceProvider.GetRequiredService<BookingOrderCreationAuthorizationHandler>();

        var context = await CreateAuthorizationHandlerContextAsync();

        await handler.HandleAsync(context);

        context.HasFailed.ShouldBeTrue();

        await productAssetRepository.InsertAsync(productAsset, true);
    }

    [Fact]
    public async Task Should_Failed_If_ProductAssetCategory_Mapping_Not_Exists()
    {
        var productAssetCategoryRepository = ServiceProvider.GetRequiredService<IProductAssetCategoryRepository>();

        var productAsset = await productAssetCategoryRepository.GetAsync(x =>
            x.AssetCategoryId == BookingTestConsts.AssetCategory1Id &&
            x.ProductId == BookingTestConsts.BookingProduct1Id);

        await productAssetCategoryRepository.DeleteAsync(productAsset, true);

        var handler = ServiceProvider.GetRequiredService<BookingOrderCreationAuthorizationHandler>();

        var context = await CreateAuthorizationHandlerContextAsync();

        await handler.HandleAsync(context);

        context.HasFailed.ShouldBeTrue();

        await productAssetCategoryRepository.InsertAsync(productAsset, true);
    }

    private Task<AuthorizationHandlerContext> CreateAuthorizationHandlerContextAsync()
    {
        var orderLine1 = new CreateOrderLineDto
        {
            ProductId = BookingTestConsts.BookingProduct1Id,
            ProductSkuId = BookingTestConsts.BookingProduct1Sku1Id,
            Quantity = BookingTestConsts.Volume
        };

        orderLine1.SetProperty(BookingOrderProperties.OrderLineBookingPeriodSchemeId,
            BookingTestConsts.PeriodScheme1Id);
        orderLine1.SetProperty(BookingOrderProperties.OrderLineBookingPeriodId, BookingTestConsts.Period1Id);
        orderLine1.SetProperty(BookingOrderProperties.OrderLineBookingAssetId, BookingTestConsts.Asset1Id);
        orderLine1.SetProperty(BookingOrderProperties.OrderLineBookingDate, BookingTestConsts.BookingDate);
        orderLine1.SetProperty(BookingOrderProperties.OrderLineBookingStartingTime,
            BookingTestConsts.Period1StartingTime);
        orderLine1.SetProperty(BookingOrderProperties.OrderLineBookingDuration, BookingTestConsts.Period1Duration);

        var orderLine2 = new CreateOrderLineDto
        {
            ProductId = BookingTestConsts.BookingProduct1Id,
            ProductSkuId = BookingTestConsts.BookingProduct1Sku1Id,
            Quantity = BookingTestConsts.Volume
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

        var currentPrincipalAccessor = ServiceProvider.GetRequiredService<ICurrentPrincipalAccessor>();

        return Task.FromResult(new AuthorizationHandlerContext(
            new[] { new OrderOperationAuthorizationRequirement(OrderOperation.Creation) },
            currentPrincipalAccessor.Principal,
            new OrderCreationResource
            {
                Input = new CreateOrderDto
                {
                    StoreId = BookingTestConsts.Store1Id,
                    OrderLines = new List<CreateOrderLineDto>
                    {
                        orderLine1, orderLine2
                    }
                },
                ProductDictionary = new Dictionary<Guid, ProductDto>
                {
                    {
                        BookingTestConsts.BookingProduct1Id,
                        new ProductDto
                        {
                            Id = BookingTestConsts.BookingProduct1Id,
                            StoreId = BookingTestConsts.Store1Id,
                            ProductGroupName = BookingTestConsts.BookingProductGroupName
                        }
                    }
                }
            }));
    }
}