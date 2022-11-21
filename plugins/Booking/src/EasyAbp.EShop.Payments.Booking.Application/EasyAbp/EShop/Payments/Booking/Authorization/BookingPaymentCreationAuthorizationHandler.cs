using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using EasyAbp.BookingService.AssetOccupancies;
using EasyAbp.BookingService.AssetOccupancies.Dtos;
using EasyAbp.BookingService.AssetOccupancyProviders;
using EasyAbp.BookingService.PeriodSchemes;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Payments.Payments;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;

namespace EasyAbp.EShop.Payments.Booking.Authorization
{
    public class BookingPaymentCreationAuthorizationHandler : PaymentCreationAuthorizationHandler
    {
        private readonly IPeriodSchemeAppService _periodSchemeAppService;
        private readonly IAssetOccupancyAppService _assetOccupancyAppService;

        public BookingPaymentCreationAuthorizationHandler(
            IPeriodSchemeAppService periodSchemeAppService,
            IAssetOccupancyAppService assetOccupancyAppService)
        {
            _periodSchemeAppService = periodSchemeAppService;
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
                    models.Add(CreateOccupyAssetInfoModel(assetId.Value, orderLine));
                }
                else if (assetCategoryId is not null)
                {
                    byCategoryModels.Add(CreateOccupyAssetByCategoryInfoModel(assetCategoryId.Value, orderLine));
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
        
        protected virtual OccupyAssetInfoModel CreateOccupyAssetInfoModel(Guid assetId, OrderLineDto orderLine)
        {
            return new OccupyAssetInfoModel(
                assetId,
                orderLine.GetBookingVolume(),
                orderLine.GetBookingDate(),
                orderLine.GetBookingStartingTime(),
                orderLine.GetBookingDuration()
            );
        }

        protected virtual OccupyAssetByCategoryInfoModel CreateOccupyAssetByCategoryInfoModel(Guid assetCategoryId,
            OrderLineDto orderLine)
        {
            return new OccupyAssetByCategoryInfoModel(
                assetCategoryId,
                orderLine.FindBookingPeriodSchemeId(),
                orderLine.GetBookingVolume(),
                orderLine.GetBookingDate(),
                orderLine.GetBookingStartingTime(),
                orderLine.GetBookingDuration()
            );
        }

        protected virtual async Task<bool> IsPeriodInfoValidAsync(OrderLineDto orderLine)
        {
            var periodSchemeId = orderLine.GetBookingPeriodSchemeId();
            var periodId = orderLine.GetBookingPeriodId();

            var periodScheme = await _periodSchemeAppService.GetAsync(periodSchemeId);
            var period = periodScheme.Periods.Find(x => x.Id == periodId);

            return period is not null;
        }
    }
}
