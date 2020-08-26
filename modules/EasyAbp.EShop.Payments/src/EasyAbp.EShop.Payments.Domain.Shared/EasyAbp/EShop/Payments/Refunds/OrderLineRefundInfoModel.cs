using System;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class OrderLineRefundInfoModel
    {
        public Guid OrderLineId { get; set; }
        
        public int Quantity { get; set; }
        
        public decimal TotalAmount { get; set; }
    }
}