using System;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.Refunds;
using EasyAbp.PaymentService.Payments;
using EasyAbp.PaymentService.Refunds;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace EasyAbp.EShop.Payments.ObjectExtending
{
    public static class EShopPaymentsObjectExtensions
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
                 * table fields in the database, then configure them in the DenturePlusEfCoreEntityExtensionMappings
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
                    .AddOrUpdate(new []
                    {
                        typeof(PaymentItem),
                        typeof(EShopPaymentItemEto),
                        typeof(PaymentItemEto),
                        typeof(CreatePaymentItemEto)
                    }, options =>
                    {
                        options.AddOrUpdateProperty<Guid>(nameof(PaymentItem.StoreId));
                    });

                ObjectExtensionManager.Instance
                    .AddOrUpdate(new []
                    {
                        typeof(RefundItem),
                        typeof(EShopRefundItemEto),
                        typeof(RefundItemEto),
                        typeof(CreateRefundItemInput)
                    }, options =>
                    {
                        options.AddOrUpdateProperty<Guid>(nameof(RefundItem.StoreId));
                        options.AddOrUpdateProperty<Guid>(nameof(RefundItem.OrderId));
                        options.AddOrUpdateProperty<string>(nameof(RefundItem.OrderLines));
                        options.AddOrUpdateProperty<string>(nameof(RefundItem.OrderExtraFees));
                    });
            });
        }
    }
}