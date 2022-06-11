using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Payments.Refunds;

[Serializable]
public class CreateEShopRefundItemInfoModel : ExtensibleObject
{
    public Guid OrderId { get; set; }

    [CanBeNull]
    public string CustomerRemark { get; set; }

    [CanBeNull]
    public string StaffRemark { get; set; }

    public List<OrderLineRefundInfoModel> OrderLines { get; set; } = new();

    public List<OrderExtraFeeRefundInfoModel> OrderExtraFees { get; set; } = new();
}