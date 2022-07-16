using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Plugins.Booking.BookingProductGroupDefinitions;

public class BookingProductGroupDefinitionTests : BookingApplicationTestBase
{
    [Fact]
    public async Task Should_Match_Booking_Product()
    {
        var definitionAppService = ServiceProvider.GetRequiredService<IBookingProductGroupDefinitionAppService>();

        var productGroupNames = (await definitionAppService.GetListAsync()).Items.Select(x => x.ProductGroupName);

        productGroupNames.ShouldContain(BookingTestConsts.BookingProductGroupName);
    }
}