using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
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

        public virtual decimal? PaymentAmount { get; protected set; }

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

        internal Order(
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

        internal void SetOrderNumber([NotNull] string orderNumber)
        {
            OrderNumber = orderNumber;
        }

        internal void SetOrderLines(List<OrderLine> orderLines)
        {
            OrderLines = orderLines;
        }

        internal void SetReducedInventoryAfterPlacingTime(DateTime time)
        {
            if (ReducedInventoryAfterPlacingTime.HasValue)
            {
                throw new OrderIsInWrongStageException(Id);
            }

            ReducedInventoryAfterPlacingTime = time;
        }

        internal void SetReducedInventoryAfterPaymentTime(DateTime time)
        {
            if (ReducedInventoryAfterPaymentTime.HasValue)
            {
                throw new OrderIsInWrongStageException(Id);
            }

            ReducedInventoryAfterPaymentTime = time;
        }

        internal async Task StartPaymentAsync(Guid paymentId, decimal? paymentAmount, IMoneyDistributor distributor)
        {
            PaymentId = paymentId;
            PaymentAmount = paymentAmount;

            if (paymentAmount is null)
            {
                // PaymentAmount is always null before EShop v5
                return;
            }

            var currentAmounts = OrderLines.ToDictionary(x => (object)x, x => x.ActualTotalPrice)
                .Union(OrderExtraFees.ToDictionary(x => (object)x, x => x.Fee))
                .ToDictionary(x => x.Key, x => x.Value);

            var distributionResult = await distributor.DistributeAsync(
                Currency,
                currentAmounts,
                paymentAmount.Value);

            foreach (var (key, amount) in distributionResult.Distributions)
            {
                switch (key)
                {
                    case OrderLine orderLine:
                        orderLine.SetPaymentAmount(amount);
                        break;
                    case OrderExtraFee orderExtraFee:
                        orderExtraFee.SetPaymentAmount(amount);
                        break;
                }
            }
        }

        internal void CancelPayment()
        {
            PaymentId = null;
            PaymentAmount = null;

            foreach (var orderLine in OrderLines)
            {
                orderLine.SetPaymentAmount(null);
            }
        }

        internal void SetPaid(DateTime paidTime)
        {
            PaidTime = paidTime;
            OrderStatus = OrderStatus.Processing;
        }

        internal void Complete(DateTime completionTime)
        {
            CompletionTime = completionTime;
            OrderStatus = OrderStatus.Completed;
        }

        internal void SetCanceled(DateTime canceledTime, [CanBeNull] string cancellationReason, bool forceCancel)
        {
            CanceledTime = canceledTime;
            CancellationReason = cancellationReason;
            OrderStatus = OrderStatus.Canceled;
        }

        public bool IsCanceled()
        {
            return CanceledTime.HasValue;
        }

        public bool IsPaid()
        {
            return PaidTime.HasValue;
        }

        internal void RefundOrderLine(Guid orderLineId, int quantity, decimal amount)
        {
            if (!IsPaid())
            {
                throw new OrderIsInWrongStageException(Id);
            }

            // PaymentAmount is always null before EShop v5
            var paymentAmount = PaymentAmount ?? ActualTotalPrice;
            if (amount <= decimal.Zero || RefundAmount + amount > paymentAmount)
            {
                throw new InvalidRefundAmountException(amount);
            }

            var orderLine = OrderLines.Single(x => x.Id == orderLineId);

            orderLine.Refund(quantity, amount);

            RefundAmount += amount;
        }

        internal void RefundOrderExtraFee([NotNull] string extraFeeName, [CanBeNull] string extraFeeKey, decimal amount)
        {
            if (!IsPaid())
            {
                throw new OrderIsInWrongStageException(Id);
            }

            // PaymentAmount is always null before EShop v5
            var paymentAmount = PaymentAmount ?? ActualTotalPrice;
            if (amount <= decimal.Zero || RefundAmount + amount > paymentAmount)
            {
                throw new InvalidRefundAmountException(amount);
            }

            var extraFee = OrderExtraFees.Single(x => x.Name == extraFeeName && x.Key == extraFeeKey);

            extraFee.Refund(amount);

            RefundAmount += amount;
        }

        public bool IsInPayment()
        {
            return PaymentId.HasValue && !PaidTime.HasValue;
        }

        internal void AddDiscounts(OrderDiscountDistributionModel model)
        {
            if (OrderStatus != OrderStatus.Pending)
            {
                throw new OrderIsInWrongStageException(Id);
            }

            foreach (var (o, discountAmount) in model.Distributions)
            {
                var orderLine = OrderLines.Single(x => x.Id == o.Id);

                orderLine.AddDiscount(discountAmount);

                TotalDiscount += discountAmount;
                ActualTotalPrice -= discountAmount;

                if (ActualTotalPrice < decimal.Zero)
                {
                    throw new DiscountAmountOverflowException();
                }

                if (OrderDiscounts.Any(x =>
                        x.OrderLineId == orderLine.Id && x.Name == model.DiscountInfoModel.Name &&
                        x.Key == model.DiscountInfoModel.Key))
                {
                    throw new DuplicateOrderDiscountException(orderLine.Id, model.DiscountInfoModel.Name,
                        model.DiscountInfoModel.Key);
                }

                var orderDiscount = new OrderDiscount(Id, orderLine.Id, model.DiscountInfoModel.EffectGroup,
                    model.DiscountInfoModel.Name, model.DiscountInfoModel.Key, model.DiscountInfoModel.DisplayName,
                    discountAmount);

                OrderDiscounts.Add(orderDiscount);
            }
        }

        internal void AddOrderExtraFee(decimal extraFee, [NotNull] string extraFeeName, [CanBeNull] string extraFeeKey,
            [CanBeNull] string extraFeeDisplayName)
        {
            if (OrderStatus != OrderStatus.Pending)
            {
                throw new OrderIsInWrongStageException(Id);
            }

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