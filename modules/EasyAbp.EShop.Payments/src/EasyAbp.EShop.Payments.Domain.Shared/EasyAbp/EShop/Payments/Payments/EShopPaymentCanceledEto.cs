using System;

namespace EasyAbp.EShop.Payments.Payments
{
    [Serializable]
    public class EShopPaymentCanceledEto
    {
        public EShopPaymentEto Payment { get; set; }
    }
}