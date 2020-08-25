using System;
using System.Collections.Generic;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Orders.Orders
{
    [Serializable]
    public class OrderEto : IOrder, IMultiTenant
    {
        public Guid Id { get; set; }
        
        public Guid? TenantId { get; set; }

        public Guid StoreId { get; set; }

        public string OrderNumber { get; set; }
        
        public Guid CustomerUserId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string Currency { get; set; }

        public decimal ProductTotalPrice { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal RefundedAmount { get; set; }

        public string CustomerRemark { get; set; }

        public string StaffRemark { get; set; }
        
        public Guid? PaymentId { get; set; }

        public DateTime? PaidTime { get; set; }

        public DateTime? CompletionTime { get; set; }

        public DateTime? CanceledTime { get; set; }
        
        public string CancellationReason { get; set; }

        public DateTime? ReducedInventoryAfterPlacingTime { get; set; }
        
        public DateTime? ReducedInventoryAfterPaymentTime { get; set; }

        public List<OrderLineEto> OrderLines { get; set; }
    }
}