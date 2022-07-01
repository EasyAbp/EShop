using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.BookingService.AssetCategories;
using EasyAbp.BookingService.AssetCategories.Dtos;
using EasyAbp.BookingService.AssetOccupancies;
using EasyAbp.BookingService.Assets;
using EasyAbp.BookingService.Assets.Dtos;
using EasyAbp.BookingService.AssetSchedules;
using EasyAbp.BookingService.Dtos;
using EasyAbp.BookingService.PeriodSchemes;
using EasyAbp.BookingService.PeriodSchemes.Dtos;
using EasyAbp.EShop.Orders.Booking;
using EasyAbp.EShop.Orders.Booking.Authorization;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Payments.Booking;
using EasyAbp.EShop.Payments.Booking.Authorization;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Booking;

[DependsOn(
    typeof(EShopOrdersBookingApplicationModule),
    typeof(EShopPaymentsBookingApplicationModule),
    typeof(EShopPluginsBookingApplicationModule),
    typeof(BookingDomainTestModule)
)]
public class BookingApplicationTestModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<BookingOrderCreationAuthorizationHandler>();
        context.Services.AddTransient<BookingPaymentCreationAuthorizationHandler>();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        
        var periodSchemeAppService = Substitute.For<IPeriodSchemeAppService>();
        services.AddTransient(_ => periodSchemeAppService);
        periodSchemeAppService.GetAsync(BookingTestConsts.PeriodScheme1Id).Returns(new PeriodSchemeDto
        {
            Id = BookingTestConsts.PeriodScheme1Id,
            Name = "PeriodScheme1",
            IsDefault = true,
            Periods = new List<PeriodDto>
            {
                new()
                {
                    Id = BookingTestConsts.Period1Id,
                    StartingTime = BookingTestConsts.Period1StartingTime,
                    Duration = BookingTestConsts.Period1Duration
                }
            }
        });

        var assetOccupancyAppService = Substitute.For<IAssetOccupancyAppService>();
        services.AddTransient(_ => assetOccupancyAppService);
        assetOccupancyAppService.CheckBulkCreateAsync(null).ReturnsForAnyArgs(Task.CompletedTask);

        var productAppService = Substitute.For<IProductAppService>();
        services.AddTransient(_ => productAppService);
        productAppService.GetAsync(BookingTestConsts.BookingProduct1Id).Returns(new ProductDto
        {
            Id = BookingTestConsts.BookingProduct1Id,
            StoreId = BookingTestConsts.Store1Id,
            ProductGroupName = BookingTestConsts.BookingProductGroupName,
            ProductSkus = new List<ProductSkuDto>
            {
                new()
                {
                    Id = BookingTestConsts.BookingProduct1Sku1Id,
                    AttributeOptionIds = new List<Guid>(),
                    Currency = "USD",
                    OrderMinQuantity = 1,
                    OrderMaxQuantity = 2
                }
            }
        });

        var assetAppService = Substitute.For<IAssetAppService>();
        services.AddTransient(_ => assetAppService);
        assetAppService.GetAsync(BookingTestConsts.Asset1Id).Returns(new AssetDto
        {
            Id = BookingTestConsts.Asset1Id,
            Name = "Camera1",
            AssetDefinitionName = "Camera",
            AssetCategoryId = BookingTestConsts.AssetCategory1Id,
            PeriodSchemeId = BookingTestConsts.PeriodScheme1Id,
            DefaultPeriodUsable = PeriodUsable.Accept,
            Volume = 1,
            TimeInAdvance = new TimeInAdvanceDto
            {
                MaxDaysInAdvance = 7,
                MinDaysInAdvance = 1
            },
        });

        var assetCategoryAppService = Substitute.For<IAssetCategoryAppService>();
        services.AddTransient(_ => assetCategoryAppService);
        assetCategoryAppService.GetAsync(BookingTestConsts.AssetCategory1Id).Returns(new AssetCategoryDto
        {
            Id = BookingTestConsts.AssetCategory1Id,
            DisplayName = "Cameras"
        });
        
        var productDetailAppService = Substitute.For<IProductDetailAppService>();
        services.AddTransient(_ => productDetailAppService);
        
        var orderRepository = Substitute.For<IOrderRepository>();
        services.AddTransient(_ => orderRepository);
        orderRepository.InsertAsync(null).ReturnsNullForAnyArgs();
    }
}
