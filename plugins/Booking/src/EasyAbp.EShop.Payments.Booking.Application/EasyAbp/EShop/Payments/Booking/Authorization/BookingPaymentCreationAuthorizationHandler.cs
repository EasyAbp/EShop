using System.Threading.Tasks;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Payments.Payments;
using Microsoft.AspNetCore.Authorization;

namespace EasyAbp.EShop.Payments.Booking.Authorization
{
    public class BookingPaymentCreationAuthorizationHandler : PaymentCreationAuthorizationHandler
    {
        public BookingPaymentCreationAuthorizationHandler()
        {
        }
        
        protected override async Task HandlePaymentCreationAsync(AuthorizationHandlerContext context,
            PaymentOperationAuthorizationRequirement requirement, PaymentCreationResource resource)
        {
            foreach (var order in resource.Orders)
            {
                foreach (var orderLine in order.OrderLines)
                {
                    var assetId = orderLine.FindBookingAssetId();
                    if (assetId is null)
                    {
                        var assetCategoryId = orderLine.FindBookingAssetCategoryId();
                        if (assetCategoryId is null)
                        {
                            continue;
                        }
                    }
                
                    // Todo: Invoke IAssetOccupancyAppService (in EasyAbp.EShop.Plugins.Booking, not EasyAbp.BookingService) to check the booking info.
                }
            }
        }
    }
}