using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using EasyAbp.BookingService.AssetOccupancies;
using EasyAbp.BookingService.AssetOccupancies.Dtos;
using EasyAbp.BookingService.PeriodSchemes;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Plugins.Booking.BookingProductGroupDefinitions;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;
using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;

namespace EasyAbp.EShop.Orders.Booking.Authorization
{
    public class BookingOrderCreationAuthorizationHandler : OrderCreationAuthorizationHandler
    {
        private readonly IPeriodSchemeAppService _periodSchemeAppService;
        private readonly IProductAssetAppService _productAssetAppService;
        private readonly IProductAssetCategoryAppService _productAssetCategoryAppService;
        private readonly IAssetOccupancyAppService _assetOccupancyAppService;
        private readonly IBookingProductGroupDefinitionAppService _definitionAppService;

        public BookingOrderCreationAuthorizationHandler(
            IPeriodSchemeAppService periodSchemeAppService,
            IProductAssetAppService productAssetAppService,
            IProductAssetCategoryAppService productAssetCategoryAppService,
            IAssetOccupancyAppService assetOccupancyAppService,
            IBookingProductGroupDefinitionAppService definitionAppService)
        {
            _periodSchemeAppService = periodSchemeAppService;
            _productAssetAppService = productAssetAppService;
            _productAssetCategoryAppService = productAssetCategoryAppService;
            _assetOccupancyAppService = assetOccupancyAppService;
            _definitionAppService = definitionAppService;
        }

        protected override async Task HandleOrderCreationAsync(AuthorizationHandlerContext context,
            OrderOperationAuthorizationRequirement requirement, OrderCreationResource resource)
        {
            var productGroupNames = (await _definitionAppService.GetListAsync()).Items.Select(x => x.ProductGroupName);

            var bookingOrderLines = resource.Input.OrderLines.Where(x =>
                productGroupNames.Contains(resource.ProductDictionary[x.ProductId].ProductGroupName)).ToList();

            if (!bookingOrderLines.Any())
            {
                return;
            }

            // Quantity of order lines for booking should be 1.
            if (bookingOrderLines.Any(x => x.Quantity != 1))
            {
                context.Fail();
                return;
            }

            var models = new List<OccupyAssetInfoModel>();
            var byCategoryModels = new List<OccupyAssetByCategoryInfoModel>();

            foreach (var orderLine in bookingOrderLines)
            {
                if (!await IsPeriodInfoValidAsync(orderLine))
                {
                    context.Fail();
                    return;
                }

                var assetId = orderLine.FindBookingAssetId();
                var assetCategoryId = orderLine.FindBookingAssetCategoryId();

                if (assetId is not null)
                {
                    if (!await IsAssetInfoValidAsync(orderLine, resource))
                    {
                        context.Fail();
                        return;
                    }

                    models.Add(CreateOccupyAssetInfoModel(assetId.Value, orderLine));
                } 
                else if (assetCategoryId is not null)
                {
                    if (!await IsAssetCategoryInfoValidAsync(orderLine, resource))
                    {
                        context.Fail();
                        return;
                    }
                    
                    byCategoryModels.Add(CreateOccupyAssetByCategoryInfoModel(assetCategoryId.Value, orderLine));
                }
                else
                {
                    context.Fail();
                    return;
                }
            }

            try
            {
                await _assetOccupancyAppService.CheckBulkCreateAsync(new BulkCreateAssetOccupancyDto
                {
                    OccupierUserId = Check.NotNull(context.User.FindUserId(), "CurrentUserId"),
                    Models = models,
                    ByCategoryModels = byCategoryModels
                });
            }
            catch
            {
                context.Fail();
                return;
            }
        }

        protected virtual OccupyAssetInfoModel CreateOccupyAssetInfoModel(Guid assetId, CreateOrderLineDto orderLine)
        {
            return new OccupyAssetInfoModel(
                assetId,
                orderLine.GetBookingDate(),
                orderLine.GetBookingStartingTime(),
                orderLine.GetBookingDuration()
            );
        }

        protected virtual OccupyAssetByCategoryInfoModel CreateOccupyAssetByCategoryInfoModel(Guid assetCategoryId,
            CreateOrderLineDto orderLine)
        {
            return new OccupyAssetByCategoryInfoModel(
                assetCategoryId,
                orderLine.GetBookingDate(),
                orderLine.GetBookingStartingTime(),
                orderLine.GetBookingDuration()
            );
        }
        
        protected virtual async Task<bool> IsAssetInfoValidAsync(CreateOrderLineDto orderLine,
            OrderCreationResource resource)
        {
            var productAsset = (await _productAssetAppService.GetListAsync(
                new GetProductAssetDto
                {
                    MaxResultCount = 1,
                    StoreId = resource.Input.StoreId,
                    ProductId = orderLine.ProductId,
                    ProductSkuId = orderLine.ProductSkuId,
                    AssetId = orderLine.GetBookingAssetId(),
                    PeriodSchemeId = orderLine.GetBookingPeriodSchemeId()
                }
            )).Items.FirstOrDefault();

            return productAsset is not null;
        }

        protected virtual async Task<bool> IsAssetCategoryInfoValidAsync(CreateOrderLineDto orderLine,
            OrderCreationResource resource)
        {
            var productAssetCategory = (await _productAssetCategoryAppService.GetListAsync(
                new GetProductAssetCategoryDto
                {
                    MaxResultCount = 1,
                    StoreId = resource.Input.StoreId,
                    ProductId = orderLine.ProductId,
                    ProductSkuId = orderLine.ProductSkuId,
                    AssetCategoryId = orderLine.GetBookingAssetCategoryId(),
                    PeriodSchemeId = orderLine.GetBookingPeriodSchemeId()
                }
            )).Items.FirstOrDefault();

            return productAssetCategory is not null;
        }
        
        protected virtual async Task<bool> IsPeriodInfoValidAsync(CreateOrderLineDto orderLine)
        {
            var periodSchemeId = orderLine.GetBookingPeriodSchemeId();
            var periodId = orderLine.GetBookingPeriodId();

            var periodScheme = await _periodSchemeAppService.GetAsync(periodSchemeId);
            var period = periodScheme.Periods.Find(x => x.Id == periodId);

            return period is not null;
        }
    }
}
