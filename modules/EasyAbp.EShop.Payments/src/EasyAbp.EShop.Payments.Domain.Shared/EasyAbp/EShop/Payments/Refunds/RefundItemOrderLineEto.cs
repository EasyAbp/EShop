using System;

namespace EasyAbp.EShop.Payments.Refunds
{
    [Serializable]
    public class RefundItemOrderLineEto
    {
        public Guid OrderLineId { get; set; }
        
        public int RefundedQuantity { get; set; }

        public decimal RefundAmount { get; set; }
    }
}