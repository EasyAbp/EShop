using System;
using EasyAbp.PaymentService.Payments;
using EasyAbp.PaymentService.Refunds;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace EasyAbp.EShop.Payments.EntityFrameworkCore
{
    public static class EShopPaymentsEfCoreEntityExtensionMappings
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
                ObjectExtensionManager.Instance
                    .MapEfCoreProperty<Payment, Guid?>(
                        "StoreId"
                    );
                
                ObjectExtensionManager.Instance
                    .MapEfCoreProperty<Refund, Guid?>(
                        "StoreId"
                    );
            });
        }
    }
}
