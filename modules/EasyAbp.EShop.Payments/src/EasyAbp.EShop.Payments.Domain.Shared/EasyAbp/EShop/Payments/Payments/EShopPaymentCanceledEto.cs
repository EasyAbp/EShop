using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Payments.Payments
{
    [Serializable]
    public class EShopPaymentCanceledEto : IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public EShopPaymentEto Payment { get; set; }

        public EShopPaymentCanceledEto(EShopPaymentEto payment)
        {
            TenantId = payment.TenantId;
            Payment = payment;
        }
    }
}