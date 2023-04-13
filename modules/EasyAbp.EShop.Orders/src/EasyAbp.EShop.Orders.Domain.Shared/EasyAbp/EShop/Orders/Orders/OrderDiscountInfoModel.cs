using System;
using System.Collections.Generic;
using EasyAbp.EShop.Products.Products;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Orders.Orders;

public class OrderDiscountInfoModel : IDiscountInfo
{
    public string EffectGroup { get; set; }

    public string Name { get; set; }

    public string Key { get; set; }

    public string DisplayName { get; set; }

    public List<Guid> AffectedOrderLineIds { get; set; } = new();

    public decimal DiscountedAmount { get; set; }

    public OrderDiscountInfoModel()
    {
    }

    public OrderDiscountInfoModel(List<Guid> affectedOrderLineIds, [CanBeNull] string effectGroup,
        [NotNull] string name, [CanBeNull] string key, [CanBeNull] string displayName, decimal discountedAmount)
    {
        if (discountedAmount < decimal.Zero)
        {
            throw new DiscountAmountOverflowException();
        }

        AffectedOrderLineIds = affectedOrderLineIds ?? new List<Guid>();
        EffectGroup = effectGroup;
        Name = name;
        Key = key;
        DisplayName = displayName;
        DiscountedAmount = discountedAmount;
    }
}