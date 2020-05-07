using System;
using System.Collections.Generic;

namespace EasyAbp.EShop.Orders.Orders
{
    [Serializable]
    public class OrderEto
    {
        public Guid Id { get; set; }
        
        public Guid? TenantId { get; set; }

        public Guid StoreId { get; set; }

        public Guid CustomerUserId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string Currency { get; set; }

        public decimal ProductTotalPrice { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal RefundedAmount { get; set; }

        public string CustomerRemark { get; set; }

        public string StaffRemark { get; set; }

        public DateTime? PaidTime { get; set; }

        public DateTime? CompletionTime { get; set; }

        public DateTime? CancelledTime { get; set; }

        public List<OrderLineEto> OrderLines { get; set; }
    }
}