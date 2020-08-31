using System;

namespace EasyAbp.EShop.Payments.Refunds.Dtos
{
    [Serializable]
    public class RefundItemOrderLineDto
    {
        public Guid OrderLineId { get; set; }
        
        public int RefundedQuantity { get; set; }
        
        public decimal RefundAmount { get; set; }
    }
}