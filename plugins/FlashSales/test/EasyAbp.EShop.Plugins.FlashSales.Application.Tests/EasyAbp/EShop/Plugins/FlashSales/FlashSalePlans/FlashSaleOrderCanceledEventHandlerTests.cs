using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.Eshop.Products.Products;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class FlashSaleOrderCanceledEventHandlerTests : FlashSalesApplicationTestBase
{
    protected FlashSaleOrderCanceledEventHandler ResultMarkAsFailedOrderCanceledEventHandler { get; }

    protected IFlashSaleCurrentResultCache FlashSaleCurrentResultCache { get; }

    protected IFlashSaleInventoryManager FlashSaleInventoryManager { get; }

    private ProductDto Product1 { get; set; }

    public FlashSaleOrderCanceledEventHandlerTests()
    {
        ResultMarkAsFailedOrderCanceledEventHandler = GetRequiredService<FlashSaleOrderCanceledEventHandler>();
        FlashSaleCurrentResultCache = GetRequiredService<IFlashSaleCurrentResultCache>();
        FlashSaleInventoryManager = GetRequiredService<IFlashSaleInventoryManager>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        Product1 = CreateMockProductDto();

        var productAppService = Substitute.For<IProductAppService>();
        productAppService.GetAsync(FlashSalesTestData.Product1Id).Returns(Task.FromResult(Product1));
        services.Replace(ServiceDescriptor.Singleton(productAppService));

        var flashSaleInventoryManager = Substitute.For<IFlashSaleInventoryManager>();
        services.Replace(ServiceDescriptor.Singleton(flashSaleInventoryManager));

        var flashSaleCurrentResultCache = Substitute.For<IFlashSaleCurrentResultCache>();
        services.Replace(ServiceDescriptor.Singleton(flashSaleCurrentResultCache));
    }

    [Fact]
    public async Task HandleEventAsync()
    {
        var flashSaleResult = await CreateFlashSaleResultAsync();
        var orderCanceledEto = new OrderCanceledEto(new OrderEto()
        {
            Id = Guid.NewGuid(),
            StoreId = flashSaleResult.StoreId,
            OrderLines = new List<OrderLineEto>()
            {
                new OrderLineEto()
                {
                    ProductId = FlashSalesTestData.Product1Id,
                    ProductSkuId = FlashSalesTestData.ProductSku1Id
                }
            }
        });
        flashSaleResult = await MarkAsSuccessfulAsync(flashSaleResult.Id, orderCanceledEto.Order.Id);
        FlashSaleInventoryManager
            .TryRollBackInventoryAsync(flashSaleResult.TenantId,
                Product1.InventoryProviderName, flashSaleResult.StoreId,
                FlashSalesTestData.Product1Id, FlashSalesTestData.ProductSku1Id)
        .Returns(Task.FromResult(true));

        await ResultMarkAsFailedOrderCanceledEventHandler.HandleEventAsync(orderCanceledEto);

        var existFlashSaleResult = await FlashSaleResultRepository.GetAsync(flashSaleResult.Id);
        existFlashSaleResult.Status.ShouldBe(FlashSaleResultStatus.Failed);
        existFlashSaleResult.Reason.ShouldBe(FlashSaleResultFailedReason.OrderCanceled);

        await FlashSaleInventoryManager.Received()
            .TryRollBackInventoryAsync(flashSaleResult.TenantId,
                Product1.InventoryProviderName, flashSaleResult.StoreId,
                FlashSalesTestData.Product1Id, FlashSalesTestData.ProductSku1Id);
        await FlashSaleCurrentResultCache.Received()
            .RemoveAsync(flashSaleResult.PlanId, flashSaleResult.UserId);
    }

    [Fact]
    public async Task HandleEventAsync_Should_Do_Not_RemoveResultCache_When_TryRollBackInventory_Failed()
    {
        var flashSaleResult = await CreateFlashSaleResultAsync();
        var orderCanceledEto = new OrderCanceledEto(new OrderEto()
        {
            Id = Guid.NewGuid(),
            StoreId = flashSaleResult.StoreId,
            OrderLines = new List<OrderLineEto>()
            {
                new OrderLineEto()
                {
                    ProductId = FlashSalesTestData.Product1Id,
                    ProductSkuId = FlashSalesTestData.ProductSku1Id
                }
            }
        });
        flashSaleResult = await MarkAsSuccessfulAsync(flashSaleResult.Id, orderCanceledEto.Order.Id);
        FlashSaleInventoryManager
            .TryRollBackInventoryAsync(flashSaleResult.TenantId,
                Product1.InventoryProviderName, flashSaleResult.StoreId,
                FlashSalesTestData.Product1Id, FlashSalesTestData.ProductSku1Id)
        .Returns(Task.FromResult(false));

        await ResultMarkAsFailedOrderCanceledEventHandler.HandleEventAsync(orderCanceledEto);

        var existFlashSaleResult = await FlashSaleResultRepository.GetAsync(flashSaleResult.Id);
        existFlashSaleResult.Status.ShouldBe(FlashSaleResultStatus.Failed);
        existFlashSaleResult.Reason.ShouldBe(FlashSaleResultFailedReason.OrderCanceled);

        await FlashSaleInventoryManager.Received()
            .TryRollBackInventoryAsync(flashSaleResult.TenantId,
                Product1.InventoryProviderName, flashSaleResult.StoreId,
                FlashSalesTestData.Product1Id, FlashSalesTestData.ProductSku1Id);
        await FlashSaleCurrentResultCache.DidNotReceive()
            .RemoveAsync(flashSaleResult.PlanId, flashSaleResult.UserId);
    }

    [Fact]
    public async Task HandleEventAsync_Should_Do_Not_Something_When_Result_Not_Exists()
    {
        var flashSaleResult = await CreateFlashSaleResultAsync();
        var orderCanceledEto = new OrderCanceledEto(new OrderEto()
        {
            Id = Guid.NewGuid(),
            StoreId = flashSaleResult.StoreId,
            OrderLines = new List<OrderLineEto>()
            {
                new OrderLineEto()
                {
                    ProductId = FlashSalesTestData.Product1Id,
                    ProductSkuId = FlashSalesTestData.ProductSku1Id
                }
            }
        });
        flashSaleResult = await MarkAsSuccessfulAsync(flashSaleResult.Id, orderCanceledEto.Order.Id);
        flashSaleResult = await MarkAsFailedAsync(flashSaleResult.Id, "UT");
        FlashSaleInventoryManager
            .TryRollBackInventoryAsync(flashSaleResult.TenantId,
                Product1.InventoryProviderName, flashSaleResult.StoreId,
                FlashSalesTestData.Product1Id, FlashSalesTestData.ProductSku1Id)
        .Returns(Task.FromResult(true));

        await ResultMarkAsFailedOrderCanceledEventHandler.HandleEventAsync(orderCanceledEto);

        var existFlashSaleResult = await FlashSaleResultRepository.GetAsync(flashSaleResult.Id);
        existFlashSaleResult.Status.ShouldBe(flashSaleResult.Status);
        existFlashSaleResult.Reason.ShouldBe(flashSaleResult.Reason);

        await FlashSaleInventoryManager.DidNotReceive()
            .TryRollBackInventoryAsync(flashSaleResult.TenantId,
                Product1.InventoryProviderName, flashSaleResult.StoreId,
                FlashSalesTestData.Product1Id, FlashSalesTestData.ProductSku1Id);
        await FlashSaleCurrentResultCache.DidNotReceive()
            .RemoveAsync(flashSaleResult.PlanId, flashSaleResult.UserId);
    }

    protected async Task<FlashSaleResult> MarkAsSuccessfulAsync(Guid resultId, Guid orderId)
    {
        return await WithUnitOfWorkAsync(async () =>
        {
            var flashSaleResult = await FlashSaleResultRepository.GetAsync(resultId);

            flashSaleResult.MarkAsSuccessful(orderId);
            await FlashSaleResultRepository.UpdateAsync(flashSaleResult, autoSave: true);

            return flashSaleResult;
        });
    }

    protected async Task<FlashSaleResult> MarkAsFailedAsync(Guid resultId, string reason)
    {
        return await WithUnitOfWorkAsync(async () =>
        {
            var flashSaleResult = await FlashSaleResultRepository.GetAsync(resultId);

            flashSaleResult.MarkAsFailed(reason);
            await FlashSaleResultRepository.UpdateAsync(flashSaleResult, autoSave: true);

            return flashSaleResult;
        });
    }

    protected async Task<FlashSaleResult> CreateFlashSaleResultAsync()
    {
        return await WithUnitOfWorkAsync(async () =>
        {
            var flashSaleResult = new FlashSaleResult(
                GuidGenerator.Create(),
                null,
                FlashSalesTestData.Store1Id,
                FlashSalesTestData.Plan1Id,
                CurrentUser.GetId(),
                DateTime.Now);
            await FlashSaleResultRepository.InsertAsync(flashSaleResult, autoSave: true);

            return flashSaleResult;
        });
    }
}
