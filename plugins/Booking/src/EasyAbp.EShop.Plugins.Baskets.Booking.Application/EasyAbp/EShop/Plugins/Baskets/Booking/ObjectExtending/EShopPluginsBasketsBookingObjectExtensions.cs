using System;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Plugins.Baskets.BasketItems;
using EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace EasyAbp.EShop.Plugins.Baskets.Booking.ObjectExtending;

public static class EShopPluginsBasketsBookingObjectExtensions
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            /* You can configure extension properties to entities or other object types
             * defined in the depended modules.
             * 
             * If you are using EF Core and want to map the entity extension properties to new
             * table fields in the database, then configure them in the EShopSampleEfCoreEntityExtensionMappings
             *
             * Example:
             *
             * ObjectExtensionManager.Instance
             *    .AddOrUpdateProperty<IdentityRole, string>("Title");
             *
             * See the documentation for more:
             * https://docs.abp.io/en/abp/latest/Object-Extensions
             */

            ObjectExtensionManager.Instance
                .AddOrUpdate(
                    new[]
                    {
                        typeof(BasketItem),
                        typeof(BasketItemDto),
                        typeof(CreateBasketItemDto),
                        typeof(UpdateBasketItemDto)
                    },
                    config =>
                    {
                        config.AddOrUpdateProperty<Guid?>(BookingBasketItemProperties.BasketItemBookingAssetId);
                        config.AddOrUpdateProperty<Guid?>(BookingBasketItemProperties.BasketItemBookingAssetCategoryId);
                        config.AddOrUpdateProperty<Guid?>(BookingBasketItemProperties.BasketItemBookingPeriodSchemeId);
                        config.AddOrUpdateProperty<Guid?>(BookingBasketItemProperties.BasketItemBookingPeriodId);
                        config.AddOrUpdateProperty<DateTime?>(BookingBasketItemProperties.BasketItemBookingDate);
                        config.AddOrUpdateProperty<TimeSpan?>(BookingBasketItemProperties.BasketItemBookingStartingTime);
                        config.AddOrUpdateProperty<TimeSpan?>(BookingBasketItemProperties.BasketItemBookingDuration);
                    }
                );
        });
    }
}