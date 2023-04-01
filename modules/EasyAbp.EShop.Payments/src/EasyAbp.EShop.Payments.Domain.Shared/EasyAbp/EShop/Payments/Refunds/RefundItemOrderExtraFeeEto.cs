using System;

namespace EasyAbp.EShop.Payments.Refunds
{
    [Serializable]
    public class RefundItemOrderExtraFeeEto
    {
        public string Name { get; set; }

        public string Key { get; set; }

        public string DisplayName { get; set; }

        public decimal RefundAmount { get; set; }
    }
}