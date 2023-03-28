using System;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderExtraFee
    {
        Guid OrderId { get; }
        
        [NotNull]
        string Name { get; }
        
        [CanBeNull]
        string Key { get; }
        
        decimal Fee { get; }
        
        decimal RefundAmount { get; }
    }
}