using System;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrder : IMultiStore, IHasExtraProperties
    {
        string OrderNumber { get; }
        
        Guid CustomerUserId { get; }
        
        OrderStatus OrderStatus { get; }

        string Currency { get; }
        
        decimal ProductTotalPrice { get; }
        
        decimal TotalDiscount { get; }
        
        decimal TotalPrice { get; }
        
        /// <summary>
        /// ActualTotalPrice = TotalPrice - TotalDiscount
        /// </summary>
        decimal ActualTotalPrice { get; }

        decimal RefundAmount { get; }
        
        string CustomerRemark { get; }
        
        string StaffRemark { get; }
        
        Guid? PaymentId { get; }
        
        DateTime? PaidTime { get; }
        
        DateTime? CompletionTime { get; }
        
        DateTime? CanceledTime { get; }
        
        string CancellationReason { get; }
        
        DateTime? ReducedInventoryAfterPlacingTime { get; }
        
        DateTime? ReducedInventoryAfterPaymentTime { get; }
    }
}