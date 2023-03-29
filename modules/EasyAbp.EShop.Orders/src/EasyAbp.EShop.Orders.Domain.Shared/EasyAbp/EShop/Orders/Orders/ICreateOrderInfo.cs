using System.Collections.Generic;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Orders.Orders;

public interface ICreateOrderInfo : IMultiStore, IHasExtraProperties
{
    string CustomerRemark { get; }

    IEnumerable<ICreateOrderLineInfo> OrderLines { get; }
}