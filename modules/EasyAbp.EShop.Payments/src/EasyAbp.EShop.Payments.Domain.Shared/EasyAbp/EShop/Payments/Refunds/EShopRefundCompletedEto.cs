using System;

namespace EasyAbp.EShop.Payments.Refunds
{
    [Serializable]
    public class EShopRefundCompletedEto
    {
        public EShopRefundEto Refund { get; set; }
    }
}