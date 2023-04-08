using System;
using System.Collections.Generic;
using System.Linq;
using EasyAbp.EShop.Products.Products;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EasyAbp.EShop.Plugins.Baskets.EntityFrameworkCore.ValueMappings;

public class ProductDiscountsInfoValueComparer : ValueComparer<List<ProductDiscountInfoModel>>
{
    public ProductDiscountsInfoValueComparer()
        : base(
            (d1, d2) => d1.SequenceEqual(d2),
            d => d.Aggregate(0, (k, v) => HashCode.Combine(k, v.GetHashCode())),
            d => d.Select(x => (ProductDiscountInfoModel)x.Clone()).ToList())
    {
    }
}

public class OrderDiscountPreviewsInfoValueComparer : ValueComparer<List<OrderDiscountPreviewInfoModel>>
{
    public OrderDiscountPreviewsInfoValueComparer()
        : base(
            (d1, d2) => d1.SequenceEqual(d2),
            d => d.Aggregate(0, (k, v) => HashCode.Combine(k, v.GetHashCode())),
            d => new List<OrderDiscountPreviewInfoModel>(d))
    {
    }
}