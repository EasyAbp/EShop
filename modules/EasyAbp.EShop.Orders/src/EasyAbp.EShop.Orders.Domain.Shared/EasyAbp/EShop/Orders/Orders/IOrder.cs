using System;
using System.Collections.Generic;
using EasyAbp.EShop.Stores.Stores;
using JetBrains.Annotations;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrder : IMultiStore, IHasExtraProperties
    {
        Guid Id { get; }

        [NotNull]
        string OrderNumber { get; }

        Guid CustomerUserId { get; }

        OrderStatus OrderStatus { get; }

        [NotNull]
        string Currency { get; }

        decimal ProductTotalPrice { get; }

        decimal TotalDiscount { get; }

        decimal TotalPrice { get; }

        /// <summary>
        /// ActualTotalPrice = TotalPrice - TotalDiscount
        /// </summary>
        decimal ActualTotalPrice { get; }

        decimal RefundAmount { get; }

        [CanBeNull]
        string CustomerRemark { get; }

        [CanBeNull]
        string StaffRemark { get; }

        Guid? PaymentId { get; }

        DateTime? PaidTime { get; }

        DateTime? CompletionTime { get; }

        DateTime? CanceledTime { get; }

        [CanBeNull]
        string CancellationReason { get; }

        DateTime? ReducedInventoryAfterPlacingTime { get; }

        DateTime? ReducedInventoryAfterPaymentTime { get; }

        DateTime? PaymentExpiration { get; }

        IEnumerable<IOrderLine> OrderLines { get; }

        IEnumerable<IOrderExtraFee> OrderExtraFees { get; }
    }
}