using System;

namespace EasyAbp.EShop.Orders.Orders
{
    [Serializable]
    public class OrderPaidEto
    {
        public OrderEto Order { get; set; }
        
        public Guid PaymentId { get; set; }
        
        public Guid PaymentItemId { get; set; }
    }
}