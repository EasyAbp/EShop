using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using EasyAbp.BookingService.AssetOccupancies;
using EasyAbp.BookingService.AssetOccupancies.Dtos;
using EasyAbp.BookingService.PeriodSchemes;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;
using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;

namespace EasyAbp.EShop.Payments.Booking.Authorization
{
    public class BookingPaymentCreationAuthorizationHandler : PaymentCreationAuthorizationHandler
    {
        private readonly IPeriodSchemeAppService _periodSchemeAppService;
        private readonly IProductAssetAppService _productAssetAppService;
        private readonly IProductAssetCategoryAppService _productAssetCategoryAppService;
        private readonly IAssetOccupancyAppService _assetOccupancyAppService;

        public BookingPaymentCreationAuthorizationHandler(
            IPeriodSchemeAppService periodSchemeAppService,
            IProductAssetAppService productAssetAppService,
            IProductAssetCategoryAppService productAssetCategoryAppService,
            IAssetOccupancyAppService assetOccupancyAppService)
        {
            _periodSchemeAppService = periodSchemeAppService;
            _productAssetAppService = productAssetAppService;
            _productAssetCategoryAppService = productAssetCategoryAppService;
            _assetOccupancyAppService = assetOccupancyAppService;
        }

        protected override async Task HandlePaymentCreationAsync(AuthorizationHandlerContext context,
            PaymentOperationAuthorizationRequirement requirement, PaymentCreationResource resource)
        {
            foreach (var order in resource.Orders)
            {
                if (await IsBookingOrderValidAsync(context, order))
                {
                    continue;
                }

                context.Fail();
                return;
            }
        }

        protected virtual async Task<bool> IsBookingOrderValidAsync(AuthorizationHandlerContext context, OrderDto order)
        {
            var bookingOrderLines = order.OrderLines.Where(x => x.FindBookingAssetId() is not null).ToList();

            if (!bookingOrderLines.Any())
            {
                return true;
            }

            var models = new List<OccupyAssetInfoModel>();
            var byCategoryModels = new List<OccupyAssetByCategoryInfoModel>();

            foreach (var orderLine in bookingOrderLines)
            {
                if (!await IsPeriodInfoValidAsync(orderLine))
                {
                    return false;
                }

                var assetId = orderLine.FindBookingAssetId();
                var assetCategoryId = orderLine.FindBookingAssetCategoryId();

                if (assetId is not null)
                {
                    if (!await IsAssetInfoValidAsync(order, orderLine))
                    {
                        return false;
                    }

                    var bookingDate =
                        Check.NotNull(orderLine.FindBookingDate(), BookingOrderProperties.OrderLineBookingDate)!.Value;

                    var bookingStartingTime = Check.NotNull(orderLine.FindBookingStartingTime(),
                        BookingOrderProperties.OrderLineBookingStartingTime)!.Value;

                    var bookingDuration = Check.NotNull(orderLine.FindBookingDuration(),
                        BookingOrderProperties.OrderLineBookingDuration)!.Value;

                    models.Add(new OccupyAssetInfoModel(
                        assetId.Value, bookingDate, bookingStartingTime, bookingDuration));
                }
                else if (assetCategoryId is not null)
                {
                    if (!await IsAssetCategoryInfoValidAsync(order, orderLine))
                    {
                        return false;
                    }

                    var bookingDate =
                        Check.NotNull(orderLine.FindBookingDate(), BookingOrderProperties.OrderLineBookingDate)!.Value;

                    var bookingStartingTime = Check.NotNull(orderLine.FindBookingStartingTime(),
                        BookingOrderProperties.OrderLineBookingStartingTime)!.Value;

                    var bookingDuration = Check.NotNull(orderLine.FindBookingDuration(),
                        BookingOrderProperties.OrderLineBookingDuration)!.Value;

                    byCategoryModels.Add(new OccupyAssetByCategoryInfoModel(
                        assetCategoryId.Value, bookingDate, bookingStartingTime, bookingDuration));
                }
                else
                {
                    return false;
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
                return false;
            }

            return true;
        }

        protected virtual async Task<bool> IsAssetInfoValidAsync(OrderDto order, OrderLineDto orderLine)
        {
            var productAsset = (await _productAssetAppService.GetListAsync(
                new GetProductAssetDto
                {
                    MaxResultCount = 1,
                    StoreId = order.StoreId,
                    ProductId = orderLine.ProductId,
                    ProductSkuId = orderLine.ProductSkuId,
                    AssetId = orderLine.FindBookingAssetId(),
                    PeriodSchemeId = orderLine.FindBookingPeriodSchemeId()
                }
            )).Items.FirstOrDefault();

            return productAsset is not null;
        }

        protected virtual async Task<bool> IsAssetCategoryInfoValidAsync(OrderDto order, OrderLineDto orderLine)
        {
            var productAssetCategory = (await _productAssetCategoryAppService.GetListAsync(
                new GetProductAssetCategoryDto
                {
                    MaxResultCount = 1,
                    StoreId = order.StoreId,
                    ProductId = orderLine.ProductId,
                    ProductSkuId = orderLine.ProductSkuId,
                    AssetCategoryId = orderLine.FindBookingAssetCategoryId(),
                    PeriodSchemeId = orderLine.FindBookingPeriodSchemeId()
                }
            )).Items.FirstOrDefault();

            return productAssetCategory is not null;
        }

        protected virtual async Task<bool> IsPeriodInfoValidAsync(OrderLineDto orderLine)
        {
            var periodSchemeId = orderLine.FindBookingPeriodSchemeId();
            var periodId = orderLine.FindBookingPeriodId();

            if (periodSchemeId is null || periodId is null)
            {
                return false;
            }

            var periodScheme = await _periodSchemeAppService.GetAsync(periodSchemeId.Value);
            var period = periodScheme.Periods.Find(x => x.Id == periodId);

            return period is not null;
        }
    }
}
