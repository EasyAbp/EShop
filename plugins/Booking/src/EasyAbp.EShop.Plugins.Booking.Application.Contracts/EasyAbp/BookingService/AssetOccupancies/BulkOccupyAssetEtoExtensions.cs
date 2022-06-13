using System;
using EasyAbp.BookingService.AssetOccupancyProviders;
using Volo.Abp.Data;

namespace EasyAbp.BookingService.AssetOccupancies;

public static class BulkOccupyAssetEtoExtensions
{
    public static Guid? FindBookingOrderId(this BulkOccupyAssetEto eto)
    {
        return eto.GetProperty<Guid?>(BulkOccupyAssetEtoProperties.BookingOrderId);
    }

    public static void SetBookingOrderId(this BulkOccupyAssetEto eto, Guid? value)
    {
        eto.SetProperty(BulkOccupyAssetEtoProperties.BookingOrderId, value);
    }
}