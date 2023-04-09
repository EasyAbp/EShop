using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EasyAbp.EShop.Products.EntityFrameworkCore.ValueMappings;

public class AttributeOptionIdsValueComparer : ValueComparer<List<Guid>>
{
    public AttributeOptionIdsValueComparer()
        : base(
            (d1, d2) => d1.SequenceEqual(d2),
            d => d.Aggregate(0, (k, v) => HashCode.Combine(k, v.GetHashCode())),
            d => new List<Guid>(d))
    {
    }
}