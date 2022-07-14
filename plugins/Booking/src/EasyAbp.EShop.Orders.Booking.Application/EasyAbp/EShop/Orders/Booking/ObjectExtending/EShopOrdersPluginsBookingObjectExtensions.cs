using System;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace EasyAbp.EShop.Orders.Booking.ObjectExtending
{
    public static class EShopOrdersPluginsBookingObjectExtensions
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
                            typeof(OrderLine),
                            typeof(OrderLineDto),
                            typeof(OrderLineEto),
                            typeof(CreateOrderLineDto)
                        },
                        config =>
                        {
                            config.AddOrUpdateProperty<Guid?>(BookingOrderProperties.OrderLineBookingAssetId);
                            config.AddOrUpdateProperty<Guid?>(BookingOrderProperties.OrderLineBookingAssetCategoryId);
                            config.AddOrUpdateProperty<Guid?>(BookingOrderProperties.OrderLineBookingPeriodSchemeId);
                            config.AddOrUpdateProperty<Guid?>(BookingOrderProperties.OrderLineBookingPeriodId);
                            config.AddOrUpdateProperty<DateTime?>(BookingOrderProperties.OrderLineBookingDate);
                            config.AddOrUpdateProperty<TimeSpan?>(BookingOrderProperties.OrderLineBookingStartingTime);
                            config.AddOrUpdateProperty<TimeSpan?>(BookingOrderProperties.OrderLineBookingDuration);
                        }
                    )
                    .AddOrUpdate(
                        new[]
                        {
                            typeof(OrderLine),
                            typeof(OrderLineDto),
                            typeof(OrderLineEto)
                        },
                        config =>
                        {
                            config.AddOrUpdateProperty<Guid?>(BookingOrderProperties.OrderLineBookingAssetOccupancyId);
                        });
            });
        }
    }
}