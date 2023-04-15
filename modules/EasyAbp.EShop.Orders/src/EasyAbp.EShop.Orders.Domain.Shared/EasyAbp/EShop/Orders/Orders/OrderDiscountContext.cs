using System;
using System.Collections.Generic;
using EasyAbp.EShop.Products.Products;
using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders;

[Serializable]
public class OrderDiscountContext
{
    public DateTime Now { get; }

    public IOrder Order { get; }

    public Dictionary<Guid, IProduct> ProductDict { get; }

    public List<OrderDiscountInfoModel> CandidateDiscounts { get; }

    public OrderDiscountContext(DateTime now, IOrder order, Dictionary<Guid, IProduct> productDict,
        List<OrderDiscountInfoModel> candidateDiscounts = null)
    {
        Now = now;
        Order = Check.NotNull(order, nameof(order));
        ProductDict = productDict ?? new Dictionary<Guid, IProduct>();
        CandidateDiscounts = candidateDiscounts ?? new List<OrderDiscountInfoModel>();
    }

    public OrderDiscountInfoModel FindCandidateDiscount([NotNull] string name, [CanBeNull] string key)
    {
        return CandidateDiscounts.Find(x => x.Name == name && x.Key == key);
    }
}