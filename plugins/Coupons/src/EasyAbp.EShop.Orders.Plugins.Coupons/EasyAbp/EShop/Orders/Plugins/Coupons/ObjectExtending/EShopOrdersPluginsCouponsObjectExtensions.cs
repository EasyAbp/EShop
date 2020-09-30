﻿using System;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Plugins.Coupons;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace EasyAbp.EShop.Orders.Plugins.Coupons.ObjectExtending
{
    public static class EShopOrdersPluginsCouponsObjectExtensions
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
                            typeof(Order),
                            typeof(OrderDto),
                            typeof(OrderEto),
                            typeof(CreateOrderDto)
                        },
                        config =>
                        {
                            config.AddOrUpdateProperty<Guid?>(CouponsConsts.OrderCouponIdPropertyName);
                        }
                    );
                
                ObjectExtensionManager.Instance
                    .AddOrUpdate(
                        new[]
                        {
                            typeof(Order),
                            typeof(OrderDto),
                            typeof(OrderEto),
                        },
                        config =>
                        {
                            config.AddOrUpdateProperty<decimal>(CouponsConsts.OrderCouponDiscountAmountPropertyName);
                        }
                    );
            });
        }
    }
}