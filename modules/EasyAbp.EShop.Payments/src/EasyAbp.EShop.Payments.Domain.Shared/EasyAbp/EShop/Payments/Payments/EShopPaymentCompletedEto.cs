using System;

namespace EasyAbp.EShop.Payments.Payments
{
    [Serializable]
    public class EShopPaymentCompletedEto
    {
        public EShopPaymentEto Payment { get; set; }
    }
}