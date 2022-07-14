using JetBrains.Annotations;

namespace EasyAbp.EShop.Plugins.Booking.Options;

public class BookingProductGroupDefinition
{
    [NotNull]
    public string ProductGroupName { get; set; }

    public BookingProductGroupDefinition([NotNull] string productGroupName)
    {
        ProductGroupName = productGroupName;
    }
}