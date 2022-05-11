using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Plugins.Booking.BookingProductGroupDefinitions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Orders.Plugins.Booking.Authorization
{
    public class BookingOrderCreationAuthorizationHandler : OrderCreationAuthorizationHandler
    {
        private readonly IBookingProductGroupDefinitionAppService _definitionAppService;

        public BookingOrderCreationAuthorizationHandler(
            IBookingProductGroupDefinitionAppService definitionAppService)
        {
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

            foreach (var orderLine in bookingOrderLines)
            {
                var assetId = orderLine.FindBookingAssetId();
                if (assetId is not null)
                {
                    // Todo: Invoke IProductAssetAppService to check the mapping.
                }
                else
                {
                    var assetCategoryId = orderLine.FindBookingAssetCategoryId();
                    if (assetCategoryId is null)
                    {
                        context.Fail();
                        return;
                    }
                    // Todo: Invoke IProductAssetCategoryAppService to check the mapping.
                }
                
                // Todo: Invoke IAssetOccupancyAppService to check the booking info.
            }
        }
    }
}