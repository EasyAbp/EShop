using System;
using System.Collections.Generic;
using System.Linq;
using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Orders.Orders;

public class OrderDiscountContext
{
    public IOrder Order { get; }

    public Dictionary<Guid, IProduct> ProductDict { get; }

    public List<OrderDiscountInfoModel> CandidateDiscounts { get; }

    public OrderDiscountContext(IOrder order, Dictionary<Guid, IProduct> productDict)
    {
        Order = order;
        ProductDict = productDict ?? new Dictionary<Guid, IProduct>();
    }

    public List<OrderDiscountInfoModel> GetEffectDiscounts()
    {
        var effectDiscounts = new List<OrderDiscountInfoModel>();

        foreach (var discount in CandidateDiscounts.Where(x => x.EffectGroup.IsNullOrEmpty()))
        {
            effectDiscounts.Add(discount);
        }

        // Make sure that each OrderLine can only be affected by one discount with the same EffectGroup.
        var affectedOrderLineIdsInEffectGroup = new Dictionary<string, List<Guid>>();

        foreach (var grouping in CandidateDiscounts.Where(x => !x.EffectGroup.IsNullOrEmpty())
                     .GroupBy(x => x.EffectGroup))
        {
            var effectGroup = grouping.Key;
            affectedOrderLineIdsInEffectGroup[effectGroup] = new List<Guid>();

            // todo: can be improved to find the best discount combo.
            foreach (var discount in grouping)
            {
                if (discount.AffectedOrderLineIds.Any(x => affectedOrderLineIdsInEffectGroup[effectGroup].Contains(x)))
                {
                    continue;
                }

                affectedOrderLineIdsInEffectGroup[effectGroup].AddRange(discount.AffectedOrderLineIds);

                effectDiscounts.Add(discount);
            }
        }

        return effectDiscounts;
    }
}