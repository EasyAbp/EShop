using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Orders.Orders;

public class OrderDiscount : Entity
{
    public virtual Guid OrderId { get; protected set; }

    public virtual Guid OrderLineId { get; protected set; }

    [NotNull]
    public virtual string Name { get; protected set; }

    [CanBeNull]
    public virtual string Key { get; protected set; }

    [CanBeNull]
    public virtual string DisplayName { get; protected set; }

    public virtual decimal DiscountedAmount { get; protected set; }

    protected OrderDiscount()
    {
    }

    public OrderDiscount(
        Guid orderId,
        Guid orderLineId,
        [NotNull] string name,
        [CanBeNull] string key,
        [CanBeNull] string displayName,
        decimal discountedAmount)
    {
        OrderId = orderId;
        OrderLineId = orderLineId;
        Name = name;
        Key = key;
        DisplayName = displayName;
        DiscountedAmount = discountedAmount;
    }

    public override object[] GetKeys()
    {
        return new object[] { OrderId, OrderLineId, Name, Key };
    }
}