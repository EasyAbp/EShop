using System;
using System.Collections.Generic;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Orders.Orders;

[Serializable]
public class CreateOrderInfoModel : ExtensibleObject, ICreateOrderInfo
{
    public Guid StoreId { get; set; }

    public string CustomerRemark { get; set; }

    IEnumerable<ICreateOrderLineInfo> ICreateOrderInfo.OrderLines => OrderLines;
    public List<CreateOrderLineInfoModel> OrderLines { get; set; }

    public CreateOrderInfoModel()
    {
    }

    public CreateOrderInfoModel(Guid storeId, string customerRemark, List<CreateOrderLineInfoModel> orderLines)
    {
        StoreId = storeId;
        CustomerRemark = customerRemark;
        OrderLines = orderLines;
    }
}