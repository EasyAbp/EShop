using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Plugins.FlashSales;
using EasyAbp.EShop.Plugins.FlashSales;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using Xunit;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using Volo.Abp.Users;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

namespace EasyAbp.EShop.Orders.Orders;

public class CreateFlashSaleOrderEventHandlerTests : OrdersPluginsFlashSalesApplicationTestBase
{
    protected CreateFlashSaleOrderEventHandler EventHandler { get; }

    protected ICurrentUser CurrentUser { get; }

    protected ICurrentTenant CurrentTenant { get; }

    protected IDistributedEventBus DistributedEventBus { get; }

    protected IFlashSalePlanHasher FlashSalePlanHasher { get; }

    protected IOrderRepository OrderRepository { get; }

    protected ProductDto Product1 { get; set; }

    public CreateFlashSaleOrderEventHandlerTests()
    {
        EventHandler = GetRequiredService<CreateFlashSaleOrderEventHandler>();
        CurrentUser = GetRequiredService<ICurrentUser>();
        CurrentTenant = GetRequiredService<ICurrentTenant>();
        DistributedEventBus = GetRequiredService<IDistributedEventBus>();
        FlashSalePlanHasher = GetRequiredService<IFlashSalePlanHasher>();
        OrderRepository = GetRequiredService<IOrderRepository>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        Product1 = CreateMockProductDto();

        var productAppService = Substitute.For<IProductAppService>();
        productAppService.GetAsync(FlashSalesTestData.Product1Id).Returns(Task.FromResult(Product1));
        services.Replace(ServiceDescriptor.Singleton(productAppService));

        var productDetailAppService = Substitute.For<IProductDetailAppService>();
        services.Replace(ServiceDescriptor.Singleton(productDetailAppService));
        productDetailAppService.GetAsync(FlashSalesTestData.ProductDetail1Id).Returns(Task.FromResult(
            new ProductDetailDto
            {
                Id = FlashSalesTestData.ProductDetail1Id,
                CreationTime = FlashSalesTestData.ProductDetailLastModificationTime,
                LastModificationTime = FlashSalesTestData.ProductDetailLastModificationTime,
                StoreId = FlashSalesTestData.Store1Id,
                Description = "My Details 1"
            }));
        productDetailAppService.GetAsync(FlashSalesTestData.ProductDetail2Id).Returns(Task.FromResult(
            new ProductDetailDto
            {
                Id = FlashSalesTestData.ProductDetail2Id,
                StoreId = FlashSalesTestData.Store1Id,
                Description = "My Details 2"
            }));

        var distributedEventBus = Substitute.For<IDistributedEventBus>();
        services.Replace(ServiceDescriptor.Singleton(distributedEventBus));

        var flashSalePlanHasher = Substitute.For<IFlashSalePlanHasher>();
        services.Replace(ServiceDescriptor.Singleton(flashSalePlanHasher));

        var orderRepository = Substitute.For<IOrderRepository>();
        services.Replace(ServiceDescriptor.Singleton(orderRepository));

        base.AfterAddApplication(services);
    }

    [Fact]
    public async Task HandleEventAsync()
    {
        FlashSalePlanHasher.HashAsync(default, default, default)
            .ReturnsForAnyArgs("My Hash Token");
        OrderRepository.InsertAsync(default, default, default)
            .ReturnsForAnyArgs(callInfo => callInfo.Arg<Order>());

        var createFlashSaleOrderEto = new CreateFlashSaleOrderEto
        {
            TenantId = CurrentTenant.Id,
            UserId = CurrentUser.GetId(),
            ResultId = FlashSalesTestData.Result1Id,
            CustomerRemark = "My Remark",
            HashToken = "My Hash Token",
            Plan = new FlashSalePlanEto
            {
                Id = FlashSalesTestData.Plan1Id,
                TenantId = CurrentTenant.Id,
                StoreId = FlashSalesTestData.Store1Id,
                BeginTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(30),
                ProductId = FlashSalesTestData.Product1Id,
                ProductSkuId = FlashSalesTestData.ProductSku1Id,
                IsPublished = true
            }
        };

        await EventHandler.HandleEventAsync(createFlashSaleOrderEto);

        await DistributedEventBus.Received()
            .PublishAsync(Arg.Is<FlashSaleOrderCreationResultEto>(eto =>
                eto.TenantId == CurrentTenant.Id &&
                eto.ResultId == FlashSalesTestData.Result1Id &&
                eto.Success &&
                eto.StoreId == FlashSalesTestData.Store1Id &&
                eto.PlanId == FlashSalesTestData.Plan1Id &&
                eto.UserId == CurrentUser.GetId() &&
                eto.OrderId != null &&
                eto.Reason == null
            ));
    }

    [Fact]
    public async Task HandleEventAsync_Should_Publish_False_When_ValidateHashToken_Failed()
    {
        FlashSalePlanHasher.HashAsync(default, default, default)
    .ReturnsForAnyArgs("My Hash Token");

        var createFlashSaleOrderEto = new CreateFlashSaleOrderEto
        {
            TenantId = CurrentTenant.Id,
            UserId = CurrentUser.GetId(),
            ResultId = FlashSalesTestData.Result1Id,
            CustomerRemark = "My Remark",
            HashToken = "My Hash Token Failed",
            Plan = new FlashSalePlanEto
            {
                Id = FlashSalesTestData.Plan1Id,
                TenantId = CurrentTenant.Id,
                StoreId = FlashSalesTestData.Store1Id,
                BeginTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(30),
                ProductId = FlashSalesTestData.Product1Id,
                ProductSkuId = FlashSalesTestData.ProductSku1Id,
                IsPublished = true
            }
        };

        await EventHandler.HandleEventAsync(createFlashSaleOrderEto);

        await DistributedEventBus.Received()
            .PublishAsync(Arg.Is<FlashSaleOrderCreationResultEto>(eto =>
                eto.TenantId == CurrentTenant.Id &&
                eto.ResultId == FlashSalesTestData.Result1Id &&
                !eto.Success &&
                eto.StoreId == FlashSalesTestData.Store1Id &&
                eto.PlanId == FlashSalesTestData.Plan1Id &&
                eto.UserId == CurrentUser.GetId() &&
                eto.OrderId == null &&
                eto.Reason == FlashSaleResultFailedReason.InvalidHashToken
            ));
    }
}
