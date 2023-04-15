using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyAbp.EShop.Orders.Orders;

public class CandidateOrderDiscounts : ICloneable
{
    public Dictionary<int, OrderDiscountInfoModel> Items { get; }

    public CandidateOrderDiscounts(List<OrderDiscountInfoModel> candidates)
    {
        Items = new Dictionary<int, OrderDiscountInfoModel>();

        foreach (var candidate in candidates)
        {
            Items.Add(Items.Count, candidate);
        }
    }

    private CandidateOrderDiscounts(Dictionary<int, OrderDiscountInfoModel> items)
    {
        Items = items ?? new Dictionary<int, OrderDiscountInfoModel>();
    }

    public object Clone()
    {
        return new CandidateOrderDiscounts(Items.ToDictionary(x => x.Key, x => x.Value));
    }
}