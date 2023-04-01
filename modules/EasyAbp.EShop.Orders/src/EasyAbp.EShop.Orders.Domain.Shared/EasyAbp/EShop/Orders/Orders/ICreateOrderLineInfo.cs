using System;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Orders.Orders;

public interface ICreateOrderLineInfo : IHasExtraProperties
{
    Guid ProductId { get; }

    Guid ProductSkuId { get; }

    int Quantity { get; }
}