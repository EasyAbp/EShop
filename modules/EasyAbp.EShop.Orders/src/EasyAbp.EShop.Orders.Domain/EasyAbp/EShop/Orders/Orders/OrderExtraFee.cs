using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderExtraFee : Entity
    {
        public virtual Guid OrderId { get; protected set; }
        
        [NotNull]
        public virtual string Name { get; protected set; }
        
        [CanBeNull]
        public virtual string Key { get; protected set; }
        
        public virtual decimal Fee { get; protected set; }

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
        
        public override object[] GetKeys()
        {
            return new object[] {OrderId, Name, Key};
        }
    }
}