using System;
using System.Collections.Generic;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Orders.Orders
{
    [Serializable]
    public class OrderEto : ExtensibleObject, IOrder, IFullAuditedObject, IMultiTenant
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
        
        public decimal ActualTotalPrice { get; set; }

        public decimal RefundAmount { get; set; }

        public string CustomerRemark { get; set; }

        public string StaffRemark { get; set; }
        
        public Guid? PaymentId { get; set; }

        public DateTime? PaidTime { get; set; }

        public DateTime? CompletionTime { get; set; }

        public DateTime? CanceledTime { get; set; }
        
        public string CancellationReason { get; set; }

        public DateTime? ReducedInventoryAfterPlacingTime { get; set; }
        
        public DateTime? ReducedInventoryAfterPaymentTime { get; set; }
        
        public DateTime? PaymentExpiration { get; set; }

        IEnumerable<IOrderLine> IOrder.OrderLines => OrderLines;
        public List<OrderLineEto> OrderLines { get; set; }

        IEnumerable<IOrderDiscount> IOrder.OrderDiscounts => OrderDiscounts;
        public List<OrderDiscountEto> OrderDiscounts { get; set; }

        IEnumerable<IOrderExtraFee> IOrder.OrderExtraFees => OrderExtraFees;
        public List<OrderExtraFeeEto> OrderExtraFees { get; set; }

        public DateTime CreationTime { get; set; }
        
        public Guid? CreatorId { get; set; }
        
        public DateTime? LastModificationTime { get; set; }
        
        public Guid? LastModifierId { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public DateTime? DeletionTime { get; set; }
        
        public Guid? DeleterId { get; set; }
    }
}