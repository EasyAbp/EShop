using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyAbp.EShop.Products.Products;

public class CandidateProductDiscounts : ICloneable
{
    public Dictionary<int, CandidateProductDiscountInfoModel> Items { get; }

    public CandidateProductDiscounts(List<CandidateProductDiscountInfoModel> candidates)
    {
        Items = new Dictionary<int, CandidateProductDiscountInfoModel>();

        foreach (var candidate in candidates)
        {
            Items.Add(Items.Count, candidate);
        }
    }

    private CandidateProductDiscounts(Dictionary<int, CandidateProductDiscountInfoModel> items)
    {
        Items = items ?? new Dictionary<int, CandidateProductDiscountInfoModel>();
    }

    public object Clone()
    {
        return new CandidateProductDiscounts(Items.ToDictionary(x => x.Key, x => x.Value));
    }
}