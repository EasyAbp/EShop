using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.BookingService.AssetOccupancies;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Orders.Orders;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace EasyAbp.EShop.Plugins.Booking.EventHandlers;

public class BookingOrderPaidEventHandler : IDistributedEventHandler<OrderPaidEto>, ITransientDependency
{
    private readonly IDistributedEventBus _distributedEventBus;

    public BookingOrderPaidEventHandler(IDistributedEventBus distributedEventBus)
    {
        _distributedEventBus = distributedEventBus;
    }

    public virtual async Task HandleEventAsync(OrderPaidEto eventData)
    {
        var occupyModels = new List<OccupyAssetInfoModel>();
        var occupyByCategoryModels = new List<OccupyAssetByCategoryInfoModel>();

        foreach (var orderLine in eventData.Order.OrderLines)
        {
            var assetId = orderLine.FindBookingAssetId();
            var assetCategoryId = orderLine.FindBookingAssetCategoryId();
            var volume = orderLine.FindBookingVolume();
            var date = orderLine.FindBookingDate();
            var startingTime = orderLine.FindBookingStartingTime();
            var duration = orderLine.FindBookingDuration();

            if (volume is null || date is null || startingTime is null || duration is null)
            {
                continue;
            }

            if (assetId.HasValue)
            {
                occupyModels.Add(new OccupyAssetInfoModel(
                    assetId: assetId.Value,
                    date: date.Value,
                    startingTime: startingTime.Value,
                    duration: duration.Value
                ));
            }
            else if (assetCategoryId.HasValue)
            {
                occupyByCategoryModels.Add(new OccupyAssetByCategoryInfoModel(
                    assetCategoryId: assetCategoryId.Value,
                    date: date.Value,
                    startingTime: startingTime.Value,
                    duration: duration.Value
                ));
            }
        }

        var eto = new BulkOccupyAssetEto(eventData.TenantId, eventData.Order.Id, eventData.Order.CustomerUserId,
            occupyModels, occupyByCategoryModels);

        eto.SetBookingOrderId(eventData.Order.Id);

        await _distributedEventBus.PublishAsync(eto);
    }
}