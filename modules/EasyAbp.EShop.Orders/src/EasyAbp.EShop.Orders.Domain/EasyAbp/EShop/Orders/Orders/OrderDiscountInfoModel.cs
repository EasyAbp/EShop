using System;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Orders.Orders;

public class OrderDiscountInfoModel
{
    public Guid OrderLineId { get; set; }

    [NotNull]
    public string Name { get; set; }

    [CanBeNull]
    public string Key { get; set; }

    [CanBeNull]
    public string DisplayName { get; set; }

    public decimal DiscountedAmount { get; set; }

    public OrderDiscountInfoModel(
        Guid orderLineId,
        [NotNull] string name,
        [CanBeNull] string key,
        [CanBeNull] string displayName,
        decimal discountedAmount)
    {
        OrderLineId = orderLineId;
        Name = name;
        Key = key ?? string.Empty;
        DisplayName = displayName;
        DiscountedAmount = discountedAmount;
    }
}