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
        
        public virtual decimal Fee { get; protected set; }
        
        public virtual decimal RefundAmount { get; protected set; }

        protected OrderExtraFee()
        {
        }

        public OrderExtraFee(
            Guid orderId,
            [NotNull] string name,
            [CanBeNull] string key,
            decimal fee)
        {
            OrderId = orderId;
            Name = name;
            Key = key;
            Fee = fee;
        }
        
        internal void Refund(decimal amount)
        {
            RefundAmount += amount;
        }
        
        public override object[] GetKeys()
        {
            return new object[] {OrderId, Name, Key};
        }
    }
}