using System;
using System.Collections.Generic;
using System.Linq;
using EasyAbp.EShop.Products.Products;
using JetBrains.Annotations;
using NodaMoney;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Orders.Orders
{
    public class Order : FullAuditedAggregateRoot<Guid>, IOrder, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid StoreId { get; protected set; }

        public virtual string OrderNumber { get; protected set; }

        public virtual Guid CustomerUserId { get; protected set; }

        public virtual OrderStatus OrderStatus { get; protected set; }

        public virtual string Currency { get; protected set; }

        public virtual decimal ProductTotalPrice { get; protected set; }

        public virtual decimal TotalDiscount { get; protected set; }

        public virtual decimal TotalPrice { get; protected set; }

        public virtual decimal ActualTotalPrice { get; protected set; }

        public virtual decimal RefundAmount { get; protected set; }

        public virtual string CustomerRemark { get; protected set; }

        public virtual string StaffRemark { get; protected set; }

        public virtual Guid? PaymentId { get; protected set; }

        public virtual DateTime? PaidTime { get; protected set; }

        public virtual DateTime? CompletionTime { get; protected set; }

        public virtual DateTime? CanceledTime { get; protected set; }

        public virtual string CancellationReason { get; protected set; }

        public virtual DateTime? ReducedInventoryAfterPlacingTime { get; protected set; }

        public virtual DateTime? ReducedInventoryAfterPaymentTime { get; protected set; }

        public virtual DateTime? PaymentExpiration { get; protected set; }

        IEnumerable<IOrderLine> IOrder.OrderLines => OrderLines;
        public virtual List<OrderLine> OrderLines { get; protected set; }

        IEnumerable<IOrderDiscount> IOrder.OrderDiscounts => OrderDiscounts;
        public virtual List<OrderDiscount> OrderDiscounts { get; protected set; }

        IEnumerable<IOrderExtraFee> IOrder.OrderExtraFees => OrderExtraFees;
        public virtual List<OrderExtraFee> OrderExtraFees { get; protected set; }

        protected Order()
        {
        }

        public Order(
            Guid id,
            Guid? tenantId,
            Guid storeId,
            Guid customerUserId,
            [NotNull] string currency,
            decimal productTotalPrice,
            decimal totalDiscount,
            decimal totalPrice,
            decimal actualTotalPrice,
            [CanBeNull] string customerRemark,
            DateTime? paymentExpiration
        ) : base(id)
        {
            TenantId = tenantId;
            StoreId = storeId;
            CustomerUserId = customerUserId;
            Currency = currency;
            ProductTotalPrice = productTotalPrice;
            TotalDiscount = totalDiscount;
            TotalPrice = totalPrice;
            ActualTotalPrice = actualTotalPrice;
            CustomerRemark = customerRemark;
            PaymentExpiration = paymentExpiration;

            RefundAmount = 0;

            OrderStatus = OrderStatus.Pending;
            OrderLines = new List<OrderLine>();
            OrderDiscounts = new List<OrderDiscount>();
            OrderExtraFees = new List<OrderExtraFee>();
        }

        public void SetOrderNumber([NotNull] string orderNumber)
        {
            OrderNumber = orderNumber;
        }

        public void SetOrderLines(List<OrderLine> orderLines)
        {
            OrderLines = orderLines;
        }

        public void SetReducedInventoryAfterPlacingTime(DateTime? time)
        {
            ReducedInventoryAfterPlacingTime = time;
        }

        public void SetReducedInventoryAfterPaymentTime(DateTime? time)
        {
            ReducedInventoryAfterPaymentTime = time;
        }

        public void SetPaymentExpiration(DateTime? paymentExpiration)
        {
            PaymentExpiration = paymentExpiration;
        }

        public void SetPaymentId(Guid? paymentId)
        {
            PaymentId = paymentId;
        }

        public void SetPaidTime(DateTime? paidTime)
        {
            PaidTime = paidTime;
        }

        public void SetOrderStatus(OrderStatus orderStatus)
        {
            OrderStatus = orderStatus;
        }

        public void SetCompletionTime(DateTime? completionTime)
        {
            CompletionTime = completionTime;
        }

        public void SetCanceled(DateTime canceledTime, [CanBeNull] string cancellationReason)
        {
            CanceledTime = canceledTime;
            CancellationReason = cancellationReason;
        }

        public bool IsPaid()
        {
            return PaidTime.HasValue;
        }

        public void RefundOrderLine(Guid orderLineId, int quantity, decimal amount)
        {
            if (amount <= decimal.Zero)
            {
                throw new InvalidRefundAmountException(amount);
            }

            var orderLine = OrderLines.Single(x => x.Id == orderLineId);

            orderLine.Refund(quantity, amount);

            RefundAmount += amount;
        }

        public void RefundOrderExtraFee([NotNull] string extraFeeName, [CanBeNull] string extraFeeKey, decimal amount)
        {
            if (amount <= decimal.Zero)
            {
                throw new InvalidRefundAmountException(amount);
            }

            var extraFee = OrderExtraFees.Single(x => x.Name == extraFeeName && x.Key == extraFeeKey);

            extraFee.Refund(amount);

            RefundAmount += amount;
        }

        public bool IsInPayment()
        {
            return !(!PaymentId.HasValue || PaidTime.HasValue);
        }

        public void AddDiscounts(OrderDiscountDistributionModel model)
        {
            foreach (var (orderLineId, discountAmount) in model.Distributions)
            {
                var orderLine = OrderLines.Single(x => x.Id == orderLineId);

                orderLine.AddDiscount(discountAmount);

                TotalDiscount += discountAmount;
                ActualTotalPrice -= discountAmount;

                if (ActualTotalPrice < decimal.Zero)
                {
                    throw new DiscountAmountOverflowException();
                }

                if (OrderDiscounts.Any(x =>
                        x.OrderLineId == orderLineId && x.Name == model.DiscountInfoModel.Name &&
                        x.Key == model.DiscountInfoModel.Key))
                {
                    throw new DuplicateOrderDiscountException(orderLineId, model.DiscountInfoModel.Name,
                        model.DiscountInfoModel.Key);
                }

                var orderDiscount = new OrderDiscount(Id, orderLineId, model.DiscountInfoModel.EffectGroup,
                    model.DiscountInfoModel.Name, model.DiscountInfoModel.Key, model.DiscountInfoModel.DisplayName,
                    discountAmount);

                OrderDiscounts.Add(orderDiscount);
            }
        }

        public void AddOrderExtraFee(decimal extraFee, [NotNull] string extraFeeName, [CanBeNull] string extraFeeKey,
            [CanBeNull] string extraFeeDisplayName)
        {
            if (extraFee <= decimal.Zero)
            {
                throw new InvalidOrderExtraFeeException(extraFee);
            }

            if (OrderExtraFees.Any(x => x.Name == extraFeeName && x.Key == extraFeeKey))
            {
                throw new DuplicateOrderExtraFeeException(extraFeeName, extraFeeKey);
            }

            var orderExtraFee = new OrderExtraFee(Id, extraFeeName, extraFeeKey, extraFeeDisplayName, extraFee);

            OrderExtraFees.Add(orderExtraFee);

            TotalPrice += extraFee;
            ActualTotalPrice += extraFee;
        }

        public void SetStaffRemark(string staffRemark)
        {
            StaffRemark = staffRemark;
        }

        public bool IsInInventoryDeductionStage()
        {
            return !ReducedInventoryAfterPlacingTime.HasValue ||
                   PaidTime.HasValue && !ReducedInventoryAfterPaymentTime.HasValue;
        }
    }
}