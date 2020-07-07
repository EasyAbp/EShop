using System;
using EasyAbp.EShop.Stores.Stores;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrder : IMultiStore
    {
        string OrderNumber { get; }
        
        Guid CustomerUserId { get; }
        
        OrderStatus OrderStatus { get; }

        string Currency { get; }
        
        decimal ProductTotalPrice { get; }
        
        decimal TotalDiscount { get; }
        
        decimal TotalPrice { get; }
        
        decimal RefundedAmount { get; }
        
        string CustomerRemark { get; }
        
        string StaffRemark { get; }
        
        Guid? PaymentId { get; }
        
        DateTime? PaidTime { get; }
        
        DateTime? CompletionTime { get; }
        
        DateTime? CanceledTime { get; }
        
        DateTime? ReducedInventoryAfterPlacingTime { get; }
        
        DateTime? ReducedInventoryAfterPaymentTime { get; }
    }
}