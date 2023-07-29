using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderExtraFee : Entity, IOrderExtraFee
    {
        public virtual Guid OrderId { get; protected set; }

        [NotNull]
        public virtual string Name { get; protected set; }

        [CanBeNull]
        public virtual string Key { get; protected set; }

        [CanBeNull]
        public virtual string DisplayName { get; protected set; }

        public virtual decimal Fee { get; protected set; }

        public virtual decimal RefundAmount { get; protected set; }

        public virtual decimal? PaymentAmount { get; protected set; }

        protected OrderExtraFee()
        {
        }

        public OrderExtraFee(
            Guid orderId,
            [NotNull] string name,
            [CanBeNull] string key,
            [CanBeNull] string displayName,
            decimal fee)
        {
            OrderId = orderId;
            Name = name;
            Key = key;
            DisplayName = displayName;
            Fee = fee;
        }

        internal void Refund(decimal amount)
        {
            // PaymentAmount is always null before EShop v5
            var paymentAmount = PaymentAmount ?? Fee;
            if (amount <= decimal.Zero || RefundAmount + amount > paymentAmount)
            {
                throw new InvalidRefundAmountException(amount);
            }

            RefundAmount += amount;
        }

        internal void SetPaymentAmount(decimal? paymentAmount)
        {
            PaymentAmount = paymentAmount;
        }

        public override object[] GetKeys()
        {
            return new object[] { OrderId, Name, Key };
        }
    }
}