using System.Collections.Generic;

namespace EasyAbp.EShop.Plugins.Booking.Options;

public class EShopBookingOptions
{
    /// <summary>
    /// Which product groups are for booking business.
    /// </summary>
    public List<BookingProductGroupDefinition> BookingProductGroups { get; } = new();
}